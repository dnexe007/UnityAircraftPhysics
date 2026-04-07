using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalSideSlip : MonoBehaviour
{
    public Vector3 ForcePointOffset = new(0, -3, 0);
    public AnimationCurve SlipForceOverSpeed = new(
        new(0, 0),
        new(200, 5)
    );
    public AnimationCurve SlipMultOverAngle = new(
        new(0, 0),
        new(30, 1)
    );

    Rigidbody rb;
    FlightData fd;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponent<FlightData>();
    }

    private void FixedUpdate()
    {

        //надо учесть случай вверх ногами
        rb.AddForceAtPosition(
            transform.right *
            Mathf.Sign(fd.Roll) *
            SlipForceOverSpeed.Evaluate(fd.LocalVelocity.z) *
            SlipMultOverAngle.Evaluate(Mathf.Abs(fd.Roll))
            ,
            transform.TransformPoint(rb.centerOfMass) +
            transform.TransformDirection(ForcePointOffset)
            ,
            ForceMode.Acceleration
        );
    }
}
