using UnityEngine;
using TMPro;
using System.Collections;

public class IntroController : MonoBehaviour
{
    [Header("UI")]
    public GameObject introPanel;
    public CanvasGroup blackFade;
    public GameObject pauseButton;
    public TextMeshProUGUI introText;
    public TextMeshProUGUI continueHint;

    [Header("Audio")]
    public AudioSource introAudio;

    [Header("PlayerInteract")]
    public PlayerInteract playerInteract;

    [Header("Typewriter")]
    public float typingSpeed = 0.04f;

    [Header("Intro Messages")]
    [TextArea(2, 5)]
    public string[] introMessages =
    {
        "Here I am…\nAlone in the quiet of a dead city.",

        "I’ve spent my life building a reputation\nas a fearless detective — Tim Crowe.",

        "Tonight,\nthat reputation is being tested.",

        "A terrorist has challenged me.\nA bomb is hidden somewhere in this city.",

        "Evacuation was the easy part.\nFinding it… won’t be.",

        "No team.\nNo backup.",

        "Just me, the city,\nand time slipping away.",

        "He said the answer\nis already in front of me.",

        "Everything begins here.\n",

        "The SNERAW Memorial. \n It isn’t a tribute.\nIt’s a message.",

        "Dawn is the limit.",

        "The countdown has begun…"
    };

    [Header("Fade Settings")]

    [Header("Tick Tock")]
    public AudioSource tickTockAudio;   
    public int tickStartIndex = 10; 

    public float fadeDuration = 1.5f;
    public float audioFadeDuration = 1.5f;

   
    private bool introActive;
    private bool isTyping;
    private bool canContinue;
    private int currentIndex;

    private Coroutine typingRoutine;
    private Coroutine blinkRoutine;

    void Start()
    {
        PrepareIntro();
    }

    void PrepareIntro()
    {
        introPanel.SetActive(false);
        continueHint.gameObject.SetActive(false);
        blackFade.alpha = 0f;

        StartIntro();
    }

    public void StartIntro()
    {
        StartCoroutine(BeginIntro());
    }

    void Update()
    {
        if (!introActive) return;

        if (Input.anyKeyDown || Input.touchCount > 0)
        {
        
            if (isTyping)
            {
                StopCoroutine(typingRoutine);
                introText.text = introMessages[currentIndex];
                isTyping = false;
                ShowContinue();
                return;
            }

           
            if (canContinue)
            {
                AdvanceMessage();
            }
        }
    }

    IEnumerator BeginIntro()
    {
        introActive = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        introPanel.SetActive(true);
        blackFade.alpha = 1f;

        introText.text = "";
        continueHint.gameObject.SetActive(false);

        currentIndex = 0;

        if (introAudio != null)
        {
            introAudio.volume = 1f;
            introAudio.Play();
        }

        StartTypingCurrent();
        yield break;
    }

    void StartTypingCurrent()
    {
        introText.text = "";
        canContinue = false;

        
        if (currentIndex == tickStartIndex && tickTockAudio != null)
        {
            tickTockAudio.volume = 0f;
            tickTockAudio.Play();
            StartCoroutine(FadeInTickTock());
        }

        typingRoutine = StartCoroutine(TypeText(introMessages[currentIndex]));
    }


    IEnumerator TypeText(string message)
    {
        isTyping = true;

        foreach (char c in message)
        {
            introText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
        ShowContinue();
    }

    void ShowContinue()
    {
        canContinue = true;
        continueHint.gameObject.SetActive(true);

        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        blinkRoutine = StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            continueHint.alpha = 1f;
            yield return new WaitForSecondsRealtime(0.6f);
            continueHint.alpha = 0f;
            yield return new WaitForSecondsRealtime(0.6f);
        }
    }

    void AdvanceMessage()
    {
        canContinue = false;
        continueHint.gameObject.SetActive(false);

        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        currentIndex++;

        if (currentIndex < introMessages.Length)
        {
            StartTypingCurrent();
        }
        else
        {
            StartCoroutine(EndIntro());
        }
    }

    IEnumerator EndIntro()
    {
        introActive = false;

        yield return StartCoroutine(FadeAudio());
        yield return StartCoroutine(FadeCanvas(1f, 0f));

        introPanel.SetActive(false);
        Time.timeScale = 1f;

        pauseButton.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        FindObjectOfType<AmbientMusicController>().StartMusic();
        PlayerPrefs.SetInt("LoadMode", 1);
        playerInteract.enabled = true;
        PlayerPrefs.Save();
    }

    IEnumerator FadeCanvas(float from, float to)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            blackFade.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }

        blackFade.alpha = to;
    }

    IEnumerator FadeAudio()
    {
        float t = 0f;

        float introStartVol = introAudio != null ? introAudio.volume : 0f;
        float tickStartVol = tickTockAudio != null ? tickTockAudio.volume : 0f;

        while (t < audioFadeDuration)
        {
            t += Time.unscaledDeltaTime;

            if (introAudio != null)
                introAudio.volume = Mathf.Lerp(introStartVol, 0f, t / audioFadeDuration);

            if (tickTockAudio != null)
                tickTockAudio.volume = Mathf.Lerp(tickStartVol, 0f, t / audioFadeDuration);

            yield return null;
        }

        if (introAudio != null)
        {
            introAudio.Stop();
            introAudio.volume = introStartVol;
        }

        if (tickTockAudio != null)
        {
            tickTockAudio.Stop();
            tickTockAudio.volume = tickStartVol;
        }
    }


    IEnumerator FadeInTickTock()
    {
        float t = 0f;
        float targetVolume = 1f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime;
            tickTockAudio.volume = Mathf.Lerp(0f, targetVolume, t);
            yield return null;
        }

        tickTockAudio.volume = targetVolume;
    }
}
