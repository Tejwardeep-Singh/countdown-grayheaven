using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    void Start()
    {
        int loadMode = PlayerPrefs.GetInt("LoadMode", 0); 

        if (loadMode == 1 && PlayerPrefs.GetInt("HasSave", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            transform.position = new Vector3(x, y, z);

            Debug.Log("Game Loaded at saved position");
        }
        else
        {
            Debug.Log("New Game started at default spawn");
        }
    }
}
