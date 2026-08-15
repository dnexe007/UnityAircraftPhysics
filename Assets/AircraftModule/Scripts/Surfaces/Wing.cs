using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
	private WingConfig config;
    public float FlapDeployment01 { get; private set; }


	protected override void Start()
    {
        base.Start();
		config = Root.Config.WingConfig;
        FlapDeployment01 = Root.FlapsValue01;
    }


	private void Update()
	{
        FlapDeployment01 = config.UpdateFlaps(
            FlapDeployment01,
            Root.FlapsValue01,
            Time.deltaTime
        );
	}


	protected override float GetLift(SurfaceMovementData movementData)
	{
		return config.GetLift(
			movementData,
			FlapDeployment01
		);
	}
}
