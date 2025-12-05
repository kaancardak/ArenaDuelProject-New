using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Ayarlar")]
    public AudioMixer mainMixer;
    public string gameSceneName = "GameScene";

    [Header("UI Referansları")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (musicSlider != null) musicSlider.value = 1f;
        if (sfxSlider != null) sfxSlider.value = 1f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan Çıkıldı!");
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