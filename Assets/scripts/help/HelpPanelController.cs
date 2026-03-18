using UnityEngine;

public class HelpPanelController : MonoBehaviour
{
    public GameObject helpPanel;
    public KeyCode toggleKey = KeyCode.H;

    private bool isOpen = false;

    void Start()
    {
        helpPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleHelp();
        }

        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleHelp();
        }
    }

    void ToggleHelp()
    {
        isOpen = !isOpen;
        helpPanel.SetActive(isOpen);

        Time.timeScale = isOpen ? 0f : 1f;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }
}
