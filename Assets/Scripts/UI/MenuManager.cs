using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.Cinemachine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private  CinemachineCamera _mainCamera;
    [SerializeField] private CinemachineCamera _settingsCamera;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;
    public float currentSensitivity;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown QualityDropdown;
    private Resolution[] resolutions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(OpenMenuAfterTime()); 
        _menuPanel.SetActive(false);
        PopulateResolutionDropdown();
    }

    public void Play()
    {
        SceneManager.LoadScene(1); 
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
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
    IEnumerator OpenSettingsAfterTime()
    {
        yield return new WaitForSeconds(2.0f);
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
      //  PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        
    }
    public void SeResolution(int resolitionIndex)
    {
        Resolution resolution = resolutions[resolitionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //QualitySettings.SetQualityLevel(qualityIndex);
        //  PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
    }
    private void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width== Screen.currentResolution.width&&
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        //  PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
    }
    public void SetFullscreen(bool isFullScreen) 
    {
        Screen.fullScreen = isFullScreen;
    }
}
