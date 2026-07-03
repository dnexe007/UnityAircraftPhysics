using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    private FlightData fd;
    protected override void Start()
    {
        base.Start();
        fd = GetComponentInParent<FlightData>();
        
    }

    protected override void ApplyForce()
    {
        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * config.wingConfig.GetLift(data.speed, data.aoa, fd.FlapsValue01);
        rb.AddForceAtPosition(
            liftVector, 
            transform.position, 
            ForceMode.Force
        );
    }
}
