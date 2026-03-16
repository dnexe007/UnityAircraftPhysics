using UnityEngine;
//using static Common;

public class VerticalResistance : MonoBehaviour
{
    private Rigidbody rb;
    private PlaneInfo info;


    /*public AnimationCurve PointOffsetOverAttackAngle = new(
        new Keyframe(15, 0),
        new Keyframe(20, -2),
        new Keyframe(30, -3)
    );*/

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponent<PlaneInfo>();
    }

    public Vector3 forcePoint;

    public float verticalSpeed;

    public float MaxFallSpeed = 50;
    float CalculateResistance(float speed)
    {
        float mult = 9.81f / Mathf.Pow(MaxFallSpeed, 2);
        return speed * speed * mult;
    }
    public float PointOffset;
    private void FixedUpdate()
    {
        //float pointOffset = PointOffsetOverAttackAngle.Evaluate(info.AttackAngle);
        float pointOffset = PointOffset;
        forcePoint = rb.position + transform.TransformDirection(rb.centerOfMass + Vector3.forward * pointOffset);

        Vector3 localVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(forcePoint));
        Vector3 newLocalVelocity = localVelocity;
        verticalSpeed = Mathf.Abs(localVelocity.y);
        float deccelerationForce = CalculateResistance(Mathf.Abs(localVelocity.y));

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
