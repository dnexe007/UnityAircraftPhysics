using UnityEngine;

public class MovementDrag : MonoBehaviour
{
    private Rigidbody rb;
    private AircraftSetup setup;

    MovementDragConfig config => setup.config.MovementDragConfig;
    //Vector3 ForcePoint => rb.worldCenterOfMass + rb.transform.TransformDirection(config.forcePointOffset);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setup = GetComponentInParent<AircraftSetup>();
    }

    private void FixedUpdate()
    {

       // float angleMult = config.resistanceMultOverFlowAngle.Evaluate(Vector3.Angle(transform.forward, rb.velocity));
        //float basicForce = config.resistanceAnchor.GetDrag(rb.velocity.magnitude);

        //Vector3 forceVector = -rb.velocity.normalized * basicForce * angleMult;

        var drag = config.GetFuselageDrag(rb.velocity, transform.forward, rb.worldCenterOfMass);
        rb.AddForceAtPosition(drag.force, drag.position, ForceMode.Force);

       // rb.AddForceAtPosition(forceVector, ForcePoint, ForceMode.Acceleration);
    }
}
