using UnityEngine;
using UnityEngine.SceneManagement;
public class pause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject PausePanel;
 

    // Update is called once per frame
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Main()
    {
        SceneManager.LoadScene("Menu");
    }
}
