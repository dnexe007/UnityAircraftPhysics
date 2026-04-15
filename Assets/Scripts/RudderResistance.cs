using UnityEngine;

public class RudderResistance : MonoBehaviour
{
    [SerializeField] private Common.QuadDragAnchor ResistanceAnchor = new(100, 9.81f);

    private Rigidbody rb;

    private void Start()
    {
         rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 localVelocity = rb.transform.InverseTransformDirection(
            rb.GetPointVelocity(transform.position)
        );

        float force = ResistanceAnchor.GetDrag(localVelocity.x) * Mathf.Sign(localVelocity.x);

        rb.AddForceAtPosition(
            - rb.transform.right * force,
            transform.position, 
            ForceMode.Acceleration
        );
    }
}
