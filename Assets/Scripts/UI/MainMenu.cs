using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
 public GameObject auchPanel = null;
    public TextMeshProUGUI playerName = null;
    public TextMeshProUGUI EnterName = null;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            Globals.playerName = PlayerPrefs.GetString("Name");
            playerName.text = Globals.playerName;
            auchPanel.SetActive(false);
        }
       else
        {
            auchPanel.SetActive(true);
       }
    }
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
     
    }
    public void SaveName() 
    {
        PlayerPrefs.SetString("Name", EnterName.text);
        PlayerPrefs.SetInt("Save", 1);
        PlayerPrefs.Save();
        Start();
    }
}
