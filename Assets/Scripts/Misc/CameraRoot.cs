using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private bool FollowRoll;
    [SerializeField] private float RotationSpeed = 3;

    //private Rigidbody rb;
    private Quaternion currentRt;
    private FlightData flightData;

    private void Start()
    {
        //rb = GetComponentInParent<Rigidbody>();
        flightData = GetComponentInParent<FlightData>();
        currentRt = transform.parent.rotation;
    }
    private void Update()
    {
        // currentFd = Vector3.Slerp(currentFd, transform.parent.forward, SlerpSpeed * Time.deltaTime);
        //transform.forward = currentFd;

        Vector3 noRollUp = Vector3.Cross(transform.parent.forward, flightData.RightHorizontalVector);
        Vector3 rollUp = transform.parent.up;


        Quaternion targetRt = Quaternion.LookRotation(
            transform.parent.forward, 
            FollowRoll? rollUp: noRollUp
        );

        currentRt = Quaternion.Slerp(currentRt, targetRt, RotationSpeed * Time.deltaTime);
        transform.rotation = currentRt;
        /*transform.localEulerAngles += currentXAngleOffset * Vector3.right;

        currentXAngleOffset = Mathf.Lerp(
            currentXAngleOffset,
            XAngleOffsetSensitivity * rb.transform.InverseTransformDirection(rb.velocity).y,
            Time.deltaTime * XAngleOffsetSpeed
        );

        transform.localPosition = Vector3.up * currentXAngleOffset * YPositionMult;*/
    }
}
