using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 40f;

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}