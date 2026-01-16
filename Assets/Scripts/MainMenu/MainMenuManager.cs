using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Sahne Ayarları")]
    public AudioMixer mainMixer;
    public string gameSceneName = "GameScene";

    [Header("UI Referansları")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public TMP_InputField fileNameInput;
    public TextMeshProUGUI statusText;
    public Button loadButton;

    private void Start()
    {
        if (musicSlider != null) musicSlider.value = 1f;
        if (sfxSlider != null) sfxSlider.value = 1f;

        PlayerPrefs.SetInt("AI_Active", 0);
        PlayerPrefs.SetString("AI_JsonData", "");
        PlayerPrefs.Save();

        if (statusText != null)
        {
            statusText.text = "AI: Not loaded.";
            statusText.color = Color.yellow;
        }
    }

    public void ProcessLoadedFile(string jsonContent)
    {
        if (string.IsNullOrEmpty(jsonContent) || !jsonContent.Trim().StartsWith("{"))
        {
            if (statusText != null)
            {
                statusText.text = "ERROR: Invalid File!";
                statusText.color = Color.red;
            }
            return;
        }

        PlayerPrefs.SetInt("AI_Active", 1);
        PlayerPrefs.SetString("AI_JsonData", jsonContent);
        PlayerPrefs.Save();

        if (statusText != null)
        {
            statusText.text = "Success: AI File Loaded.";
            statusText.color = Color.green;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMusicVolume(float sliderValue)
    {
        if (mainMixer != null)
            mainMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        if (mainMixer != null)
            mainMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }
}