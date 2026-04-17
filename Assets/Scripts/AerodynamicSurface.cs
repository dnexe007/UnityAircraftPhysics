using UnityEngine;

public enum SurfaceType
{
    AileronR,
    AileronL,
    Pitch,
    Rudder
}

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    [SerializeField] private SurfaceType surfaceType;

    public SurfaceType GetSurfaceType() => surfaceType;
    private AirSurfaceCFG surfaceParams
    {
        get
        {
            switch (surfaceType)
            {
                case SurfaceType.AileronR:
                    return config.aileronParams;
                case SurfaceType.AileronL:
                    return config.aileronParams;
                case SurfaceType.Pitch:
                    return config.pitchParams;
                default:
                    return config.rudderParams;
            }
        }
    }

    protected override void ApplyForce()
    {
        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * surfaceParams.GetLift(data.speed, data.aoa);

        rb.AddForceAtPosition(
            liftVector
            ,
            transform.position
            ,
            ForceMode.Force
        );
    }
}
