using UnityEngine;
using TMPro;
using System.Collections;

public class PromptMessage : MonoBehaviour
{
    public static PromptMessage instance;

    public TextMeshProUGUI messageText;

    void Awake()
    {
        instance = this;
        messageText.gameObject.SetActive(false);
    }

    public void ShowMessage(string msg, float duration = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(Show(msg, duration));
    }

    IEnumerator Show(string msg, float duration)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        messageText.gameObject.SetActive(false);
    }
}