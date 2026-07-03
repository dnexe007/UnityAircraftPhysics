using UnityEngine;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    public string surfaceType;

    private AerodynamicSurfaceConfig surfaceConfig;

	protected override void Start()
	{
		base.Start();
		surfaceConfig = config.GetSurfaceConfigByName(surfaceType);
	}

    public string GetSurfaceType() => surfaceType;

    protected override void ApplyForce()
    {
        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * surfaceConfig.GetLift(data.speed, data.aoa);

        rb.AddForceAtPosition(
            liftVector
            ,
            transform.position
            ,
            ForceMode.Force
        );
    }
}
