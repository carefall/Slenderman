using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] Bot bot;
    internal bool safe;
    private Vector2 direction, pointer;
    private Rigidbody rb;
    [SerializeField] private float speed, sense, jumpHeight;
    private Vector3 camRot;
    [SerializeField] private Light flashlight;
    [SerializeField] private TextMeshProUGUI pickText;
    [SerializeField] private Image point, key;
    [SerializeField] private Transform keys;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private string[] texts;
    private Ladder ladder;

    private bool onGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = (transform.forward * direction.y + transform.right * direction.x) * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
        transform.eulerAngles += new Vector3(0, pointer.x * sense, 0);
        camRot.x -= pointer.y * sense;
        camRot.x = Mathf.Clamp(camRot.x, -90, 90);
        Camera.main.transform.localEulerAngles = camRot;
        if (transform.position.y < 19) safe = true;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 3))
        {
            if (hit.collider.gameObject.CompareTag("key"))
            {
                pickText.enabled = true;
                key.enabled = true;
                pickText.text = texts[0];
                key.sprite = sprites[0];
                point.enabled = false;
            }
            else if (hit.collider.gameObject.CompareTag("door"))
            {
                pickText.enabled = true;
                key.enabled = true;
                pickText.text = texts[keys.childCount == 0 ? 1 : 2];
                key.sprite = sprites[keys.childCount == 0 ? 1 : 2];
                point.enabled = false;
            }
            else
            {
                pickText.enabled = false;
                key.enabled = false;
                point.enabled = true;
            }
        }
        else
        {
            if (ladder)
            {
                pickText.enabled = true;
                key.enabled = true;
                pickText.text = texts[3];
                key.sprite = sprites[3];
            }
            else
            {
                pickText.enabled = false;
                key.enabled = false;
                point.enabled = true;
            }
        }
        if (!safe)
        {
            bot.Chase(transform.position);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        pointer = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!onGround) return;
            onGround = false;
            rb.AddForce(Vector3.up * jumpHeight);
        }

    }

    public void Flashlight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }

    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (ladder)
            {
                transform.position = ladder.point.position;
            }
            else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 3))
            {
                if (hit.collider.gameObject.CompareTag("key"))
                {
                    Destroy(hit.collider.gameObject);
                    bot.AddSpeed();
                }
                else if (hit.collider.gameObject.CompareTag("door"))
                {
                    if (keys.childCount == 0)
                    {

                        Destroy(this);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("House"))
        {
            bot.Go();
            safe = true;
        }
        if (other.gameObject.CompareTag("Bot"))
        {
            bot.Stop(transform); 
            rb.linearVelocity = Vector3.zero;
            Destroy(this);
        }
        other.gameObject.TryGetComponent<Ladder>(out ladder);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("House"))
        {
            if (transform.position.y > 19)
            {
                safe = false;
            }
        }
        if (other.gameObject.TryGetComponent<Ladder>(out _))
        {
            ladder = null;
        }
    }

    public void Ground()
    {
        onGround = true;
    }
}
