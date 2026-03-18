using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseController : MonoBehaviour
{
    

    [Header("UI")]
    public GameObject pauseMenu;

    [Header("Player")]
    public Transform player;


    private bool isPaused = false;

    void Start()
    {
        
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   

            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

   
    public void TogglePause()
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isPaused = true;
        
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }

    

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void SaveGame()
    {
        Vector3 pos = player.position;

        PlayerPrefs.SetFloat("PlayerX", pos.x);
        PlayerPrefs.SetFloat("PlayerY", pos.y);
        PlayerPrefs.SetFloat("PlayerZ", pos.z);

        PlayerPrefs.SetInt("HasSave", 1);
        PlayerPrefs.Save();

        Debug.Log("Game Saved at " + pos);
    }
}
