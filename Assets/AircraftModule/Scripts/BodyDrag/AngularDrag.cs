using UnityEngine;

public class AngularDrag : MonoBehaviour
{
    private Rigidbody rb;
    private Aircraft setup;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setup = GetComponentInParent<Aircraft>();
    }

    private void FixedUpdate()
    {
        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);
        rb.AddRelativeTorque(
            setup.Config.AngularDragConfig.GetAngularDrag(localAngVel, rb.velocity.magnitude),
            ForceMode.Force
        );
    }
}
