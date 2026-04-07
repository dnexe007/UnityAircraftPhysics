using UnityEngine;

public class RudderResistance : MonoBehaviour
{
    private Rigidbody rb;
    public float ResistanceMult;
    private void Start()
    {
         rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 localVelocity = rb.transform.InverseTransformDirection(
            rb.GetPointVelocity(transform.position)
        );

        float force = -localVelocity.x * Mathf.Abs(localVelocity.x) * ResistanceMult;

        rb.AddForceAtPosition(
            rb.transform.right * force,
            transform.position, 
            ForceMode.Acceleration
        );
    }
}
