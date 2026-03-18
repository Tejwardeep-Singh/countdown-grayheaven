using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;

    public int storyStep = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AdvanceStory()
    {
        storyStep++;
        Debug.Log("Story advanced to step: " + storyStep);
    }
}