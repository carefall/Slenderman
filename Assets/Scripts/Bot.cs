using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Bot : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
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
        agent.isStopped = true;
        Camera.main.transform.localEulerAngles = Vector3.zero;
        player.LookAt(transform);
        anim.Play("Hit");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
