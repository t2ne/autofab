using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioMixer audiomixer;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;

    public void Start()
    {
        LoadVolume();
        LoadSensitivity();
        //Play the music track "MainMenu" with a fade duration of 1 second
        MusicManager.Instance.PlayMusic("MainMenuJazz", 1f);
        Debug.Log("Hello!!!", gameObject);
    }

    public void PlayGame()
    {
        LevelManager.Instance.LoadScene("MainGame", "CrossFade");
        //also play the FactoryMusic along with the FactoryEngineSounds
        MusicManager.Instance.PlayMusic("FactoryMusic", 1.5f);
        MusicManager.Instance.PlayMusic("FactoryEngineSounds", 1.5f);
    }

    public void Options()
    {
        //Load the scene with the name "Options"
        SceneManager.LoadScene("OptionsMenu");
        Debug.Log("Opções");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    //Options Menu

    public void Voltar()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateMusicVolume(float volume)
    {
        audiomixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSFXVolume(float volume)
    {
        audiomixer.SetFloat("SFXVolume", volume);
    }

    public void SaveVolume()
    {
        audiomixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audiomixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        Debug.Log("Volume carregado");
    }

    public void UpdateSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    public void SaveSensitivity()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
    }

    public void LoadSensitivity()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }
}
