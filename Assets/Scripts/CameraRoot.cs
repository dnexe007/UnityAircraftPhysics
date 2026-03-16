using UnityEngine;

public class CameraRoot : MonoBehaviour
{

    public float SlerpSpeed = 10;

    Rigidbody rb;

    Quaternion currentRt;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        rt = GetComponentInParent<RollAndPitch>();
        currentRt = transform.parent.rotation;
    }

    public float XAngleOffsetSensitivity;
    float currentXAngleOffset;
    public float XAngleOffsetSpeed;

    public float YPositionMult;
    RollAndPitch rt;
    private void Update()
    {
        // currentFd = Vector3.Slerp(currentFd, transform.parent.forward, SlerpSpeed * Time.deltaTime);
        //transform.forward = currentFd;

        Vector3 trUp = Vector3.Cross(transform.parent.forward, rt.rightHorizontalVector);

        Quaternion targetRt = Quaternion.LookRotation(transform.parent.forward, trUp);

        currentRt = Quaternion.Slerp(currentRt, targetRt, SlerpSpeed * Time.deltaTime);
        transform.rotation = currentRt;
        transform.localEulerAngles += currentXAngleOffset * Vector3.right;

        currentXAngleOffset = Mathf.Lerp(
            currentXAngleOffset,
            XAngleOffsetSensitivity * rb.transform.InverseTransformDirection(rb.velocity).y,
            Time.deltaTime * XAngleOffsetSpeed
        );

        transform.localPosition = Vector3.up * currentXAngleOffset * YPositionMult;
    }
}
