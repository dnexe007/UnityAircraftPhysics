using UnityEngine;
//using static Common;

public class VerticalResistance : MonoBehaviour
{
    private Rigidbody rb;
    private PlaneInfo info;

    public AnimationCurve ResistanceOverSpeed = new(
        new Keyframe(0, 0),
        new Keyframe(75, 9.81f),
        new Keyframe(200, 130)
    );

    public AnimationCurve PointOffsetOverAttackAngle = new(
        new Keyframe(15, 0),
        new Keyframe(20, -2),
        new Keyframe(30, -3)
    );

   /* public AnimationCurve ForwardSpeedMult = new(
        new Keyframe(0, 1),
        new Keyframe(120, 6),
        new Keyframe(400, 10)
    );*/

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponent<PlaneInfo>();
    }

    public Vector3 forcePoint;

    private void FixedUpdate()
    {
        float pointOffset = PointOffsetOverAttackAngle.Evaluate(info.attackAngle);
        forcePoint = rb.position + transform.TransformDirection(rb.centerOfMass + Vector3.forward * pointOffset);

        Vector3 localVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(forcePoint));
        Vector3 newLocalVelocity = localVelocity;

        float deccelerationForce = ResistanceOverSpeed.Evaluate(Mathf.Abs(localVelocity.y));

        newLocalVelocity.y = Mathf.MoveTowards(localVelocity.y, 0, deccelerationForce * Time.fixedDeltaTime);

        Vector3 velocityDelta = transform.TransformDirection(newLocalVelocity - localVelocity);

        rb.AddForceAtPosition(velocityDelta, forcePoint, ForceMode.VelocityChange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        if(Application.isPlaying)
            Gizmos.DrawWireSphere(forcePoint, 0.5f);
    }
}
