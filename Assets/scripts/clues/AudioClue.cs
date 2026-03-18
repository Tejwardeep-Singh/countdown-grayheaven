using UnityEngine;
using System.Collections;

public class AudioClue : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    static bool audioPlaying = false;
    public int clueStep;
    public AudioClip clueAudio;

    public bool isFinalClue = false;

    public void Interact()
        {
        if (audioPlaying)
            return;

        if (audioSource == null || clueAudio == null)
        {
            Debug.LogError("Missing audio on " + gameObject.name);
            return;
        }

        if (audioSource.isPlaying)
            return;

        if (StoryManager.instance.storyStep < clueStep)
        {
            PlayerInteract player = FindFirstObjectByType<PlayerInteract>();    
            if (player != null)
                PromptMessage.instance.ShowMessage("The Signal is Lost......");

            return;
        }

        Debug.Log("Playing clue: " + gameObject.name);

        audioPlaying = true;    
        audioSource.clip = clueAudio;
        audioSource.Play();

        StartCoroutine(UnlockAudio());

        if (StoryManager.instance.storyStep == clueStep)
        {
            StoryManager.instance.AdvanceStory();
        }

        if (isFinalClue)
        {
            StartCoroutine(TriggerEndingAfterAudio());
        }
        }

   IEnumerator TriggerEndingAfterAudio()
    {
        while (audioSource.time < audioSource.clip.length)
        {
            yield return null;
        }

        EndingController ending = FindFirstObjectByType<EndingController>();

        if (ending != null)
        {
            ending.StartEnding();
        }
    }
    IEnumerator UnlockAudio()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        audioPlaying = false;
    }
    IEnumerator ShowLockedPrompt(PlayerInteract player)
    {
        player.SetPromptText("The phone is locked.");

        yield return new WaitForSeconds(2f);

        player.SetPromptText("Press E to Inspect");
    }
}   