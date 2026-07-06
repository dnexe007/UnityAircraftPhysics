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

    protected override float CalculateLift()
    {
        return surfaceConfig.GetLift(VelocityMagnitude, VerticalAOA);
    }
}
