using UnityEngine;

public class AmbientMusicController : MonoBehaviour
{
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void StartMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}