using NUnit.Framework;
using System;
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
    [SerializeField] private  CinemachineCamera _mainCamera;
    [SerializeField] private CinemachineCamera _settingsCamera;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _auchPanel;
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Transform _movableNailTransform;
    public float currentSensitivity;
    private int _currentResolutionIndex;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button exitButton;
    private Resolution[] resolutions;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(OpenMenuAfterTime()); 
        _menuPanel.SetActive(false);
        _loadPanel.SetActive(false);
        _mainPanel.SetActive(false);
        PopulateResolutionDropdown();
        LoadSettings(_currentResolutionIndex);
        exitButton.onClick.AddListener(ExitGame);
    }
    private void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        _currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                _currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        //  PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
    }
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetVolume()
    {
        float desiredVolume = Mathf.InverseLerp(-0.136f, -1.841574f, _movableNailTransform.localPosition.x);
        if (desiredVolume <= 0.0001f)
            desiredVolume = 0.0001f;

        _audioMixer.SetFloat("MasterVolume", MathF.Log10(desiredVolume) * 20);
    }
    public void SetSensitivity(float _desiredSensetivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", _desiredSensetivity);
        currentSensitivity = _desiredSensetivity;
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettings", qualityDropdown.value);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(Screen.fullScreen));

        float volume;
        _audioMixer.GetFloat("MasterVolume", out volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettings"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettings");
        }
        else
            qualityDropdown.value = 3;

        SetQuality(qualityDropdown.value);
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        }
        else
            resolutionDropdown.value = currentResolutionIndex;

        SetResolution(resolutionDropdown.value);
        //SetResolution();
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        }
        else
            Screen.fullScreen = true;

        _toggle.isOn = Screen.fullScreen;
        if (PlayerPrefs.HasKey("Volume"))
        {
            _audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("Volume"));
            _movableNailTransform.localPosition = new Vector3(Mathf.Lerp(-0.136f, -1.841574f, Mathf.Pow(10, PlayerPrefs.GetFloat("Volume") / 20)), 0, 0);
        }
        else
        {
            _audioMixer.SetFloat("MasterVolume", 0);
            _movableNailTransform.localPosition = new Vector3(Mathf.Lerp(-0.136f, -1.841574f, 1), 0, 0);
        }
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
        _mainPanel.SetActive(true);
        _settingsPanel.SetActive(false);
        _auchPanel.SetActive(false);
        _settingsCamera.gameObject.SetActive(false);
        _mainCamera.gameObject.SetActive(true);
        _settingsCamera.Priority = 0;
        StartCoroutine(OpenMenuAfterTime());
    }
    IEnumerator OpenSettingsAfterTime()
    {
        yield return new WaitForSeconds(1.0f);
        _settingsPanel.SetActive(true);
    }
    IEnumerator OpenMenuAfterTime()
    {
        yield return new WaitForSeconds(6.0f);
        _menuPanel.SetActive(true);
       // _auchPanel?.SetActive(true);
        _mainPanel?.SetActive(true);
        _loadPanel?.SetActive(true);
    }
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
