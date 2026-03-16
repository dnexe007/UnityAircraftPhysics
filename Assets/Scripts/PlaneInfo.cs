using UnityEngine;

public class PlaneInfo : MonoBehaviour
{
    private Rigidbody rb;

    public Vector3 LocalVelocity { get; private set; }
    public float AttackAngle { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        LocalVelocity = transform.InverseTransformDirection(rb.velocity);

        AttackAngle = Mathf.Atan2(-LocalVelocity.y, LocalVelocity.z) * Mathf.Rad2Deg;
    }
}
