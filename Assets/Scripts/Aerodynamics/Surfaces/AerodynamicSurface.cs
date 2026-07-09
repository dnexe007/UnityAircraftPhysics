using UnityEngine;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    [SerializeField] private string profileName = "Default";


    private AerodynamicSurfaceConfig surfaceConfig;

    public float MaxRotationAngle => surfaceConfig.GetMaxRotationAngle(VelocityMagnitude);
    public float RotationSpeed => surfaceConfig.RotationSpeed;


	protected override void Start()
	{
		base.Start();
		surfaceConfig = config.GetSurfaceConfigByName(profileName);
	}

    protected override float CalculateLift()
    {
        return surfaceConfig.GetLift(VelocityMagnitude, VerticalAOA);
    }
}
