using UnityEngine;

public class ForwardResistance : MonoBehaviour
{
    public AnimationCurve ResistanceOverSpeed = new(
        new Keyframe(0, 0),
        new Keyframe(200, 1.5f),
        new Keyframe(250, 12)
    );

    private Rigidbody rb;
    private PlaneInfo info;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponent<PlaneInfo>();
    }

    private void FixedUpdate()
    {
        Vector3 localVelocity = info.localVelocity;
        float decceleration = ResistanceOverSpeed.Evaluate(Mathf.Abs(localVelocity.z));
        localVelocity.z = Mathf.MoveTowards(localVelocity.z, 0, decceleration * Time.fixedDeltaTime);
        Vector3 velocityDelta = transform.TransformDirection(localVelocity - info.localVelocity);
        rb.AddForce(velocityDelta, ForceMode.VelocityChange);
    }
}
