using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Bot : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim,fade;
    [SerializeField] Transform head;

    private void Start()
    {
        transform.position = points[Random.Range(0, points.Length)].position;
    }

    public void Chase(Vector3 player)
    {
        agent.destination = player;
    }
    public void Go()
    {
        agent.destination = points[Random.Range(0, points.Length)].position;
    }
    public void AddSpeed()
    {
        agent.speed+=0.5f;
    }
    public void Stop(Transform player)
    {
        agent.destination = transform.position;
        agent.isStopped = true;
        Camera.main.transform.localEulerAngles = Vector3.zero;
        Camera.main.transform.LookAt(head);
        anim.Play("Roar");
        fade.Play("Fade");
        Invoke("Menu", 1.1f);
    }
    void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
