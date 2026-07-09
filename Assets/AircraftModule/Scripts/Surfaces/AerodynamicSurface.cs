using UnityEngine;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    [SerializeField] private string profileName = "Default";


    private AerodynamicSurfaceConfig config;

    public float MaxRotationAngle => config.GetMaxRotationAngle(VelocityMagnitude);
    public float RotationSpeed => config.RotationSpeed;


	protected override void Start()
	{
		base.Start();
		config = GetComponentInParent<Aircraft>().Config.GetSurfaceConfigByName(profileName);
	}

    protected override float CalculateLift()
    {
        return config.GetLift(VelocityMagnitude, VerticalAOA);
    }
}
