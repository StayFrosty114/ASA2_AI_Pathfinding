
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Overlord : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
