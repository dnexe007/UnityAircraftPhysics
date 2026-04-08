using UnityEngine;

public class AngularDrag : MonoBehaviour
{

    [SerializeField] private Vector3 DragForceAxesMults = new(1, 1, 0.5f);
    [SerializeField] public float DragForce = 5;


    public Common.QuadDragAnchor SpeedStabilityAnchor = new(50, 1);

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float speedMult = 1 + SpeedStabilityAnchor.GetDrag(rb.velocity.magnitude);

        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);
        Vector3 newLocalAngVel = localAngVel;

        float deccelerationX = DragForceAxesMults.x * Mathf.Abs(newLocalAngVel.x);
        newLocalAngVel.x = Mathf.MoveTowards(newLocalAngVel.x, 0, Time.fixedDeltaTime * deccelerationX * speedMult * DragForce);

        float deccelerationY = DragForceAxesMults.y * Mathf.Abs(newLocalAngVel.y);
        newLocalAngVel.y = Mathf.MoveTowards(newLocalAngVel.y, 0, Time.fixedDeltaTime * deccelerationY * speedMult * DragForce);

        float deccelerationZ = DragForceAxesMults.z * Mathf.Abs(newLocalAngVel.z);
        newLocalAngVel.z = Mathf.MoveTowards(newLocalAngVel.z, 0, Time.fixedDeltaTime * deccelerationZ * speedMult * DragForce);

        rb.AddRelativeTorque(newLocalAngVel - localAngVel, ForceMode.VelocityChange);
    }
}
