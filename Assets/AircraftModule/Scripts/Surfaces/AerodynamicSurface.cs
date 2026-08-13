using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    [SerializeField] private string profileName = "Default";

	private AerodynamicSurfaceConfig config;

	protected override void Start()
	{
		base.Start();
		config = Root.Config.GetSurfaceConfigByName(profileName);
	}

	protected override float GetLift(float velocityMagnitude, float verticalAOA)
	{
		return config.GetLift(velocityMagnitude, verticalAOA);
	}
}
