using UnityEngine;

public class PlaneInfo : MonoBehaviour
{
    
    private Rigidbody rb;

    public float attackAngle { get; private set; }
    public Vector3 localVelocity { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        print(rb.inertiaTensor);
    }

    private void FixedUpdate()
    {
        localVelocity = transform.InverseTransformDirection(rb.velocity);
        attackAngle = Mathf.Atan2(-localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;
    }
}
