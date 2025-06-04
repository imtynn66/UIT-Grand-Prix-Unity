using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceUIController : MonoBehaviour
{
    public GameObject endRacePanel;

    public void ShowEndPanel()
    {
        endRacePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Lobby");
    }
}