using UnityEngine;

public class AngularDrag : MonoBehaviour
{
    [SerializeField] private Vector3 DragForces = new(5, 5, 3);
    [SerializeField] private AnimationCurve ForceMultOverSpeed = new(
        new(0, 0.25f),
        new(50, 1)
    );

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float forceMult = ForceMultOverSpeed.Evaluate(rb.velocity.magnitude);

        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);
        Vector3 newLocalAngVel = localAngVel;

        float deccelerationX = DragForces.x * Mathf.Abs(newLocalAngVel.x);
        newLocalAngVel.x = Mathf.MoveTowards(newLocalAngVel.x, 0, Time.fixedDeltaTime * deccelerationX * forceMult);

        float deccelerationY = DragForces.y * Mathf.Abs(newLocalAngVel.y);
        newLocalAngVel.y = Mathf.MoveTowards(newLocalAngVel.y, 0, Time.fixedDeltaTime * deccelerationY * forceMult);

        float deccelerationZ = DragForces.z * Mathf.Abs(newLocalAngVel.z);
        newLocalAngVel.z = Mathf.MoveTowards(newLocalAngVel.z, 0, Time.fixedDeltaTime * deccelerationZ * forceMult);

        rb.AddRelativeTorque(newLocalAngVel - localAngVel, ForceMode.VelocityChange);
    }
}
