using UnityEngine;

public class AngularDrag : MonoBehaviour
{
    private Rigidbody rb;
    private FlightData fd;

    private FuselageAngularDragCFG config => fd.aircraftParams.fuselageAngularDragParams;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponent<FlightData>();
    }

    private void FixedUpdate()
    {
        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);
        float speedMult = 1 + config.speedStabilityAnchor.GetDrag(rb.velocity.magnitude);

        var localDrag = new Vector3(
            -localAngVel.x * config.axesCoefs.x,
            -localAngVel.y * config.axesCoefs.y,
            -localAngVel.z * config.axesCoefs.z
        ) * speedMult * config.basicDrag;

        rb.AddRelativeTorque(
            localDrag,
            ForceMode.Acceleration
        );
    }
}
