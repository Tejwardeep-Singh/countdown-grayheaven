using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class EndingController : MonoBehaviour
{
    public GameObject endingsPanel;

    public TextMeshProUGUI chapterText;
    public GameObject creditsText;

    public AudioSource creditsMusic;

    [TextArea]
    public string endingMessage =
    "The truth is revealed.\n\nNow comes the reckoning.\n\nCOUNTDOWN: GRAY HEAVEN\n\nChapter 2\nREVENGE";

    public float typingSpeed = 0.12f;
    public float waitAfterText = 5f;

    public float creditsDuration = 18f;

    public void StartEnding()
    {
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        
        endingsPanel.SetActive(true);

        chapterText.gameObject.SetActive(true);
        chapterText.text = "";
        yield return new WaitForSecondsRealtime(1.5f);
        
        if (creditsMusic != null)
            creditsMusic.Play();

       
        foreach (char c in endingMessage)
        {
            chapterText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        
        yield return new WaitForSecondsRealtime(waitAfterText);

        
        chapterText.gameObject.SetActive(false);

        
        creditsText.SetActive(true);

        
        yield return new WaitForSecondsRealtime(creditsDuration);

        
        if (creditsMusic != null)
            yield return StartCoroutine(FadeAudio());

        
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeAudio()
    {
        float startVolume = creditsMusic.volume;
        float t = 0f;

        while (t < 2f)
        {
            t += Time.unscaledDeltaTime;
            creditsMusic.volume = Mathf.Lerp(startVolume, 0f, t / 2f);
            yield return null;
        }

        creditsMusic.Stop();
        creditsMusic.volume = startVolume;
    }
}