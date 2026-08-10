using UnityEngine;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    [SerializeField] private string profileName = "Default";
	[SerializeField] private SurfaceController surfaceController;

	private AerodynamicSurfaceConfig config;

	public float GetMaxRotationAngle()
	{
		Vector3 localVelocity = rb.GetLocalVelocity(transform, transform.position);

		localVelocity.x = 0;

		return config.GetMaxRotationAngle(localVelocity.magnitude);
	}

	protected override void Start()
	{
		base.Start();
		config = root.Config.GetSurfaceConfigByName(profileName);
	}

	protected override float GetLift(float velocityMagnitude, float verticalAOA)
	{
		return config.GetLift(velocityMagnitude, verticalAOA);
	}

	private void Update()
	{
		surfaceController.UpdateAngle(GetMaxRotationAngle(), config.RotationSpeed, root);
		CurrentRotationAngle = surfaceController.CurrentRotationAngle;
	}
}
