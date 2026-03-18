using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class IntroSequence : MonoBehaviour
{
    public GameObject fog;

    public Image blackFade;
    public Image snerawLogo;
    public Image gameLogo;
    public AudioSource audioSource;
    public AudioClip boomClip;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        blackFade.color = new Color(0,0,0,1);
        snerawLogo.color = new Color(1,1,1,0);
        gameLogo.color = new Color(1,1,1,0);

        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        
        yield return Fade(blackFade, 1, 0, 1f);

        
        
        yield return LogoReveal(snerawLogo, 1.2f, 1.5f);
        yield return new WaitForSeconds(0.3f);

        
        audioSource.PlayOneShot(boomClip, 0.9f);
        yield return LogoReveal(gameLogo, 1.5f, 2f);

        
        yield return Fade(blackFade, 0, 1, 1f);
        if (fog) fog.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LogoReveal(Image logo, float revealTime, float holdTime)
    {
        logo.gameObject.SetActive(true);
        logo.transform.localScale = Vector3.one * 0.8f;

        

        
        for (float t = 0; t < revealTime; t += Time.deltaTime)
        {
            float p = t / revealTime;
            logo.color = new Color(1, 1, 1, p);
            logo.transform.localScale = Vector3.Lerp(Vector3.one * 0.8f, Vector3.one, p);
            yield return null;
        }

        logo.color = Color.white;
        logo.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(holdTime);

        
        for (float t = 0; t < 0.8f; t += Time.deltaTime)
        {
            float p = 1 - (t / 0.8f);
            logo.color = new Color(1, 1, 1, p);
            yield return null;
        }

        logo.color = new Color(1, 1, 1, 0);
        logo.gameObject.SetActive(false);
    }

    IEnumerator Fade(Image img, float from, float to, float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float a = Mathf.Lerp(from, to, t / time);
            img.color = new Color(0, 0, 0, a);
            yield return null;
        }
        img.color = new Color(0, 0, 0, to);
    }
}
