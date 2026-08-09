using UnityEngine;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    [SerializeField] private string profileName = "Default";
	[SerializeField] private MultipointWing pointsGenerator;
	[SerializeField] private SurfaceController surfaceController;

	private AerodynamicSurfaceConfig config;

	public float CurrentRotationAngle => surfaceController.CurrentRotationAngle;
    public float MaxRotationAngle => config.GetMaxRotationAngle(VelocityMagnitude);
    public float RotationSpeed => config.RotationSpeed;


	private Aircraft root;
	protected override void Start()
	{
		base.Start();
		root = GetComponentInParent<Aircraft>();
		config = root.Config.GetSurfaceConfigByName(profileName);
	}


	private void Update()
	{
		surfaceController.UpdateAngle(MaxRotationAngle, RotationSpeed, root);
	}

	protected override void ApplyLift()
	{
		foreach (WingPoint point in pointsGenerator.GetPoints(transform, CurrentRotationAngle, rb))
		{
			float lift = config.GetLift(point.VelocityMagnitude, point.VerticalAOA);
			rb.AddForceAtPosition(lift * point.Normal * point.TotalForceMult, point.Position, ForceMode.Force);
		}
	}

	private void OnDrawGizmos()
	{
		pointsGenerator.DrawGizmos(transform, CurrentRotationAngle);
	}
}
