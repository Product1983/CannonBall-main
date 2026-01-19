using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _settingsCamera;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        _mainCamera.SetActive(false); 
        _menuPanel.SetActive(false);
        _settingsCamera.SetActive(true);
        _settingsPanel.SetActive(true);

    }
}
