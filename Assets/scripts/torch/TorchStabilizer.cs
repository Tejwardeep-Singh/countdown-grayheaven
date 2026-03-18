using UnityEngine;

public class TorchStabilizer : MonoBehaviour
{
    Vector3 initialLocalPos;
    public float smooth = 12f;

    void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    void LateUpdate()   
    {
        Vector3 targetLocalPos = initialLocalPos;

        
        targetLocalPos.y = initialLocalPos.y;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetLocalPos,
            Time.deltaTime * smooth
        );
    }
}
