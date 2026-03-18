using UnityEngine;

public class PaperMapController : MonoBehaviour
{
    public GameObject mapPanel;
    public KeyCode mapKey = KeyCode.M;
    private bool isOpen;

    void Update()
    {
        if (Input.GetKeyDown(mapKey))
            ToggleMap();
    }

    void ToggleMap()
    {
        isOpen = !isOpen;
        mapPanel.SetActive(isOpen);

        Time.timeScale = isOpen ? 0f : 1f;
        Cursor.visible = isOpen;
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
