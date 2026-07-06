using UnityEngine;

public class AngularDrag : MonoBehaviour
{
    private Rigidbody rb;
    private AircraftSetup setup;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setup = GetComponentInParent<AircraftSetup>();
    }

    private void FixedUpdate()
    {
        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);
        rb.AddRelativeTorque(
            setup.config.AngularDragConfig.GetAngularDrag(localAngVel, rb.velocity.magnitude),
            ForceMode.Force
        );
    }
}
