using UnityEngine;
using System.Collections;

public class ClockTowerBroadcast : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip warningClip;

    [Header("Timing")]
    [SerializeField] private float delayAfterIntro = 1.5f;

    private bool hasPlayed = false;

    void Awake()
    {
        if (audioSource == null || warningClip == null)
        {
            Debug.LogError("ClockTowerBroadcast: Missing AudioSource or WarningClip");
            enabled = false;
            return;
        }

        audioSource.clip = warningClip;
    }

    public void PlayBroadcast()
    {
        if (hasPlayed) return;

        hasPlayed = true;
        StartCoroutine(PlayAfterDelay());
    }

    IEnumerator PlayAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayAfterIntro);
        audioSource.Play();
    }
    public void PauseBroadcast()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Pause();
    }

    public void ResumeBroadcast()
    {
        if (audioSource != null)
            audioSource.UnPause();
    }
}
