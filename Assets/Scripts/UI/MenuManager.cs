using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
   [SerializeField] private  CinemachineCamera _mainCamera;
    [SerializeField] private CinemachineCamera _settingsCamera;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;
    public float currentSensitivity;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Toggle _toggle;
    private Resolution[] resolutions;
    private int _currentResolutionIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(OpenMenuAfterTime()); 
        _menuPanel.SetActive(false);
        PopulateResolutionDropdown();
        LoadSettings(_currentResolutionIndex);
    }
    public void Play()
    {
        SceneManager.LoadScene(1); 
    }
    public void Settings()
    {
        _mainCamera.Priority = 0; 
        _menuPanel.SetActive(false);
        _settingsCamera.gameObject.SetActive(true);
        _mainCamera.gameObject.SetActive(false);
        _settingsCamera.Priority = 1;
        StartCoroutine(OpenSettingsAfterTime());
    }
    public void Back()
    {
        _mainCamera.Priority = 1;
        _menuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
        _settingsCamera.gameObject.SetActive(false);
        _mainCamera.gameObject.SetActive(true);
        _settingsCamera.Priority = 0;
        StartCoroutine(OpenMenuAfterTime());
    }
    IEnumerator OpenSettingsAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        _settingsPanel.SetActive(true);
    }
    IEnumerator OpenMenuAfterTime()
    {
        yield return new WaitForSeconds(6.0f);
        _menuPanel.SetActive(true);
    }
    public void SetSensitivity(float _desiredSensetivity) 
    {
        PlayerPrefs.SetFloat("Sensitivity", _desiredSensetivity);
      currentSensitivity = _desiredSensetivity;
    }
    public void SaveSettings() 
    {
       PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
        PlayerPrefs.SetInt("Resolution", _resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreen", System.Convert.ToInt32(Screen.fullScreen));

        float volume;
        _audioMixer.GetFloat("MasterVolume", out volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettings"))
        {
            _qualityDropdown.value = PlayerPrefs.GetInt("QualitySettings");
        }
        else
            _qualityDropdown.value = 3;

        SetQuality(_qualityDropdown.value);
        if (PlayerPrefs.HasKey("Resolution"))
        {
            _resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        }
        else
            _resolutionDropdown.value = currentResolutionIndex;

            SetResolution(_resolutionDropdown.value);
            if (PlayerPrefs.HasKey("FullScreen"))
            {
                Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen"));
            }
            else
                Screen.fullScreen = true;
        _toggle.isOn = Screen.fullScreen;

        if (PlayerPrefs.HasKey("Volume"))
        {
            _audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("Volume"));
        }
        else
            _audioMixer.SetFloat("MasterVolume", 0); ;

        SetResolution(_resolutionDropdown.value);
    }
    public void SetVolume(float desiredVolume) 
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(desiredVolume)*20);  
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        
    }
    public void SetResolution(int resolitionIndex)
    {
        Resolution resolution = resolutions[resolitionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //QualitySettings.SetQualityLevel(qualityIndex);
        //  PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
    }
    private void PopulateResolutionDropdown()
    {
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        _currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width== Screen.currentResolution.width&&
                resolutions[i].height == Screen.currentResolution.height)
                _currentResolutionIndex = i;
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.RefreshShownValue();
        //  PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
    }
    public void SetFullscreen(bool isFullScreen) 
    {
        Screen.fullScreen = isFullScreen;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
