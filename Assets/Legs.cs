using UnityEngine;

public class Legs : MonoBehaviour
{
    [SerializeField] PlayerContoller player;

    private void OnTriggerEnter(Collider other)
    {
        player.Ground();
    }
}
