using UnityEngine;

public class AirResistance : MonoBehaviour
{
    public const float MsToKmh = 3.6f;
    public const float MsToKnots = 1.94384f;

    private Rigidbody rb;
    private PlaneInfo info;

    private Vector3 localVelocity;

    public Vector3 ResistancePointOffset = new Vector3(0, 0, -3);

    public AnimationCurve ForwardResistance = new AnimationCurve(
        new Keyframe(0, 0),
        new Keyframe(400, 3),
        new Keyframe(500, 23)
    );

    public AnimationCurve VerticalResistance = new AnimationCurve(
        new Keyframe(0, 0),
        new Keyframe(150, 9.81f),
        new Keyframe(400, 130)
    );

    public AnimationCurve SidewaysResistance = new AnimationCurve(
        new Keyframe(0, 0),
        new Keyframe(150, 8),
        new Keyframe(400, 120)
    );

    public AnimationCurve ForwardSpeedMultCurve = new AnimationCurve(
        new Keyframe(0, 1),
        new Keyframe(120, 6),
        new Keyframe(400, 10)
    );

    public AnimationCurve VerticalResistanceOffsetOverAttackAngle = new AnimationCurve(
        new Keyframe(15, 0),
        new Keyframe(20, -2),
        new Keyframe(30, -3)
    );
    
    void ApplyMovementResistance()
    {/*
        float deccelerationZ = ForwardResistance.Evaluate(Mathf.Abs(localVelocity.z) * MsToKnots) / MsToKnots;
        localVelocity.z = Mathf.MoveTowards(localVelocity.z, 0, deccelerationZ * Time.fixedDeltaTime);*/

        float forwardMult = ForwardSpeedMultCurve.Evaluate(Mathf.Abs(localVelocity.z) * MsToKnots);

        float deccelerationY =
            VerticalResistance.Evaluate(Mathf.Abs(localVelocity.y) * MsToKnots) * forwardMult;
        localVelocity.y = Mathf.MoveTowards(localVelocity.y, 0, deccelerationY * Time.fixedDeltaTime);

      /*  float deccelerationX =
            SidewaysResistance.Evaluate(Mathf.Abs(localVelocity.x) * MsToKnots) * forwardMult;
        localVelocity.x = Mathf.MoveTowards(localVelocity.x, 0, deccelerationX * Time.fixedDeltaTime);*/
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponent<PlaneInfo>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.GetPointVelocity(transform.TransformPoint(rb.centerOfMass + ResistancePointOffset));

        localVelocity = transform.InverseTransformDirection(velocity);
        ApplyMovementResistance();
        Vector3 newVelocity = transform.TransformDirection(localVelocity);

        Vector3 delta = newVelocity - velocity;
        rb.AddForceAtPosition(delta, transform.TransformPoint(rb.centerOfMass + ResistancePointOffset), ForceMode.VelocityChange);
    }

    
}
