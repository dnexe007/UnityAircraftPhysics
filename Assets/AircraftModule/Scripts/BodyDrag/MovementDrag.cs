using UnityEngine;

public class MovementDrag : MonoBehaviour
{
    private Rigidbody rb;
    private MovementDragConfig config;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        config = GetComponentInParent<Aircraft>().Config.MovementDragConfig;
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
