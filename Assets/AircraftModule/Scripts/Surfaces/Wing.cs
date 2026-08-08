using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    [SerializeField] private FlapAnimator flapAnimator;


    private WingConfig config;
    private Aircraft root;

    public float currentFlapsValue01;
   // private Vector3 flapsModelStartAngles;


    protected override void Start()
    {
        base.Start();
        root = GetComponentInParent<Aircraft>();
		config = root.Config.WingConfig;
        currentFlapsValue01 = root.FlapsValue01;

        //if (flapAnimator != null) flapsModelStartAngles = flapAnimator.localEulerAngles;
    }


	private void Update()
	{
        currentFlapsValue01 = config.UpdateFlaps(currentFlapsValue01, root.FlapsValue01, Time.deltaTime);

        if(flapAnimator != null) flapAnimator.SetDeployment(currentFlapsValue01);

        //if(flapAnimator != null)
        //{
        //    flapAnimator.localEulerAngles = flapsModelStartAngles + flapsModelRotationVector * currentFlapsValue01 * config.FlapsRotationAngle;
        //}
	}

	protected override float CalculateLift()
    {
        return config.GetLift(VelocityMagnitude, VerticalAOA, currentFlapsValue01);
    }
}
