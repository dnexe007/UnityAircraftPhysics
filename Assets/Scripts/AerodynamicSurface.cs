using System;
using UnityEngine;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    private AirSurfaceCFG surfaceParams => config.pitchParams;
    [SerializeField] private float additionalRotatingFactor = 0;

    protected override void ApplyForce()
    {

        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * surfaceParams.GetLift(data.speed, data.aoa);

        rb.AddForceAtPosition(
            liftVector
            ,
            transform.position -
            rb.transform.forward * additionalRotatingFactor
            ,
            ForceMode.Force
        );
    }
}
