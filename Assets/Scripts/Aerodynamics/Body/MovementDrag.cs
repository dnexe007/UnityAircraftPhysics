using UnityEngine;

public class MovementDrag : MonoBehaviour
{
    private Rigidbody rb;
    private AircraftSetup setup;
    MovementDragConfig config => setup.config.MovementDragConfig;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setup = GetComponentInParent<AircraftSetup>();
    }

    private void FixedUpdate()
    {
        float angleOfAttack = Vector3.Angle(rb.velocity, transform.forward);

        Vector3 dragVector = config.GetDragVector(rb.velocity, angleOfAttack);
        float rotatingFactor = config.GetRotatingFactor(angleOfAttack);

        rb.AddForceAtPosition(
            dragVector,
            rb.worldCenterOfMass + rb.transform.forward * rotatingFactor,
            ForceMode.Force
        );
    }
}
