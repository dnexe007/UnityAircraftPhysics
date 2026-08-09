using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    private WingConfig config;
    private Aircraft root;

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


	protected override float CalculateLift()
    {
        return config.GetLift(
            VelocityMagnitude,
            VerticalAOA,
            CurrentFlapDeployment01
        );
    }
}
