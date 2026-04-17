using UnityEngine;

public class FuselageResistance : MonoBehaviour
{
    private Rigidbody rb;
    private FlightData fd;

    FuselageDragCFG config => fd.config.fuselageDragParams;
    Vector3 ForcePoint => rb.worldCenterOfMass + rb.transform.TransformDirection(config.forcePointOffset);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponent<FlightData>();
    }

    private void FixedUpdate()
    {

        float angleMult = config.resistanceMultOverFlowAngle.Evaluate(Vector3.Angle(transform.forward, rb.velocity));
        float basicForce = config.resistanceAnchor.GetDrag(rb.velocity.magnitude);

        Vector3 forceVector = -rb.velocity.normalized * basicForce * angleMult;

        rb.AddForceAtPosition(forceVector, ForcePoint, ForceMode.Acceleration);
    }
}
