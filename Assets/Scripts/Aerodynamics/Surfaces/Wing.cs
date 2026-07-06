using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    private FlightData fd;
    protected override void Start()
    {
        base.Start();
        fd = GetComponentInParent<FlightData>();
    }
    protected override float CalculateLift()
    {
        return config.WingConfig.GetLift(VelocityMagnitude, VerticalAOA, fd.FlapsValue01);
    }
}
