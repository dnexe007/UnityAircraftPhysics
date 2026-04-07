using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollHelper : MonoBehaviour
{
    FlightData fd;
    Rigidbody rb;

    public AnimationCurve HelperOverAngularVel = new(
        new(0.0f, 0.0f),
        new(0.03f, 0.3f)
    );

    public AnimationCurve HelperMultOverPitch = new(
        new(0, 0),
        new(30, 1)
    );

    private float angularVel;
    private void Start()
    {
        fd = GetComponentInParent<FlightData>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        angularVel = Mathf.Abs(rb.transform.InverseTransformDirection(rb.angularVelocity).y);
        rb.AddTorque(
            transform.forward *
            Mathf.Sign(fd.Roll) *
            HelperOverAngularVel.Evaluate(angularVel) *
            HelperMultOverPitch.Evaluate(fd.Pitch),
            ForceMode.Acceleration
        );
    }
}
