using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject winPanel;
    public GameObject losePanel;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void TriggerWin()
    {
        if(winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0; 
    }

    public void TriggerLose()
    {
        if(losePanel != null) losePanel.SetActive(true);
        Time.timeScale = 0; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}