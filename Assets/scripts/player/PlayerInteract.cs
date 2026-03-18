using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{

    public TextMeshProUGUI promptText;
    public float interactDistance = 4f;
    public GameObject interactPrompt;
    public Camera playerCamera;

    private IInteractable current;
    bool overridePrompt = false;
    float overrideTimer = 0f;

    void OnEnable()
    {
        Debug.Log("PlayerInteract ENABLED");
        if (interactPrompt)
            interactPrompt.SetActive(false);
    }
    
    

public void SetPromptText(string message)
{
    if (promptText != null)
    {
        promptText.text = message;
        overridePrompt = true;
        overrideTimer = 2f;
    }
}
    
void Update()
{

    if (overridePrompt)
    {
        overrideTimer -= Time.deltaTime;

        if (overrideTimer <= 0f)
        {
            overridePrompt = false; 

            if (promptText != null)
                promptText.text = "Press E to Inspect";
        }

        
    }
    Camera cam = playerCamera;
    if (!cam) return;

    Vector3 origin = cam.transform.position;
    Vector3 direction = cam.transform.forward;

    int interactMask = LayerMask.GetMask("Interactable");

    Debug.DrawRay(origin, direction * interactDistance, Color.green);

    bool foundInteractable = false;

    
    if (Physics.SphereCast(origin, 0.5f, direction, out RaycastHit hit, interactDistance, interactMask))
    {
        IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            current = interactable;
            foundInteractable = true;
        }
    }

    
    if (!foundInteractable)
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, 1.5f, interactMask);

        if (nearby.Length > 0)
        {
            IInteractable interactable = nearby[0].GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                current = interactable;
                foundInteractable = true;
            }
        }
    }

    
   if (foundInteractable)
    {
        if (interactPrompt && !interactPrompt.activeSelf)
            interactPrompt.SetActive(true);

        if (!overridePrompt && promptText != null)
            promptText.text = "Press E to Inspect";

        if (Input.GetKeyDown(KeyCode.E))
        {
            current.Interact();
        }

        return;
    }

   
    current = null;
    if (interactPrompt && interactPrompt.activeSelf)
        interactPrompt.SetActive(false);
}



}
