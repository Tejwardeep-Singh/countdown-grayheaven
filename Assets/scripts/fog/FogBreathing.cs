using UnityEngine;

public class FogBreathing : MonoBehaviour
{
    public float baseDensity = 0.06f;
    public float variation = 0.002f;
    public float speed = 0.1f;

    void Update()
    {
        RenderSettings.fogDensity =
            baseDensity + Mathf.Sin(Time.time * speed) * variation;
    }
}
