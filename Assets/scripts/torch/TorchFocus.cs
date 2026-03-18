using UnityEngine;

public class TorchFocus : MonoBehaviour
{
    public Transform mainCamera;
    public float rotationSpeed = 15f;

    void LateUpdate()
    {
        if (!mainCamera) return;

        Quaternion targetRotation =
            Quaternion.LookRotation(mainCamera.forward, Vector3.up);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }
}
