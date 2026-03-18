using UnityEngine;

public class TorchController : MonoBehaviour
{
    [Header("Torch References")]
    [SerializeField] private Light torchLight;

    [Header("Intensity Settings")]
    [SerializeField] private float minIntensity = 5f;
    [SerializeField] private float maxIntensity = 50f;
    [SerializeField] private float defaultIntensity = 50f;
    [SerializeField] private float intensityStep = 5f;

    [Header("Input")]
    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    [SerializeField] private KeyCode increaseKey = KeyCode.KeypadPlus;
    [SerializeField] private KeyCode decreaseKey = KeyCode.KeypadMinus;

    private bool isOn = false;

    void Start()
    {
        if (torchLight == null)
        {
            Debug.LogError("Torch Light is not assigned!");
            enabled = false;
            return;
        }

       
        torchLight.intensity = defaultIntensity;
        torchLight.enabled = false;
    }

    void Update()
    {
        HandleToggle();
        HandleIntensity();
    }

    void HandleToggle()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            torchLight.enabled = isOn;

            
            if (isOn)
            {
                torchLight.intensity = Mathf.Clamp(
                    torchLight.intensity,
                    minIntensity,
                    maxIntensity
                );
            }
        }
    }

    void HandleIntensity()
    {
        if (!isOn) return;

        if (Input.GetKeyDown(increaseKey))
        {
            torchLight.intensity = Mathf.Clamp(
                torchLight.intensity + intensityStep,
                minIntensity,
                maxIntensity
            );
        }

        if (Input.GetKeyDown(decreaseKey))
        {
            torchLight.intensity = Mathf.Clamp(
                torchLight.intensity - intensityStep,
                minIntensity,
                maxIntensity
            );
        }
    }
}
