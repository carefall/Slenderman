using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] Animator anim;
    public void Play()
    {
        anim.Play("Fade");
        Invoke("Game", 1.1f);
    }
    private void Game()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Enter(BaseEventData e)
    {
        e.selectedObject.GetComponentInChildren<TextMeshProUGUI>().color=Color.red;
    }
    public void Exit(BaseEventData e)
    {
        e.selectedObject.GetComponentInChildren<TextMeshProUGUI>().color = color;
    }

}
