using UnityEngine;
using System.Collections;

public class PhoneClue : MonoBehaviour, IInteractable
{
    public int requiredStoryStep;

    public void Interact()
    {
        if (StoryManager.instance.storyStep < requiredStoryStep)
        {
            Debug.Log("The phone isn't responding yet...");
            return;
        }

        Debug.Log("Phone activated");
    }
}