using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    private WingConfig config;
    private Aircraft root;
    [SerializeField] private MultipointWing pointsGenerator;
    public float CurrentFlapDeployment01 { get; private set; }


    protected override void Start()
    {
        base.Start();
        root = GetComponentInParent<Aircraft>();
		config = root.Config.WingConfig;
        CurrentFlapDeployment01 = root.FlapsValue01;
    }


	private void Update()
	{
        CurrentFlapDeployment01 = config.UpdateFlaps(
            CurrentFlapDeployment01,
            root.FlapsValue01,
            Time.deltaTime
        );
	}

	//protected override void ApplyLift()
	//{
	//	foreach(WingPoint point in pointsGenerator.GetPoints(transform))
 //       {
 //           point.UpdateMovementData(rb);
 //           float lift = config.GetLift(point.VelocityMagnitude, point.VerticalAOA, CurrentFlapDeployment01);
 //           rb.AddForceAtPosition(lift * point.normal * point.forceMult / pointsGenerator.NumOfPoints, point.position, ForceMode.Force);
 //       }
	//}

	protected override float CalculateLift()
    {
        return config.GetLift(
            VelocityMagnitude,
            VerticalAOA,
            CurrentFlapDeployment01
        );
    }

	//private void OnDrawGizmos()
	//{
 //       pointsGenerator.DrawGizmos(transform, 0);
	//}
}
