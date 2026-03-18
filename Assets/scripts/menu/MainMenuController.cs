using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject mainMenu;
    public GameObject continueMainButton;

    void Start()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mainMenu.SetActive(true);

        SetupContinueButton();
    }

    void SetupContinueButton()
    {
        bool hasSave = PlayerPrefs.GetInt("HasSave", 0) == 1;
        continueMainButton.SetActive(hasSave);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("LoadMode", 0); 
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGame");
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetInt("LoadMode", 1); 
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
