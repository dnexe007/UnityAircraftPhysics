using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    [SerializeField] private Transform flapsModel;
    [SerializeField] private Vector3 flapsModelRotationVector;
    


    private WingConfig config;
    private Aircraft root;

    public float currentFlapsValue01;
    private Vector3 flapsModelStartAngles;


    protected override void Start()
    {
        base.Start();
        root = GetComponentInParent<Aircraft>();
		config = root.Config.WingConfig;
        currentFlapsValue01 = root.FlapsValue01;

        if (flapsModel != null) flapsModelStartAngles = flapsModel.localEulerAngles;
    }


	private void Update()
	{
        currentFlapsValue01 = config.UpdateFlaps(currentFlapsValue01, root.FlapsValue01, Time.deltaTime);

        if(flapsModel != null)
        {
            flapsModel.localEulerAngles = flapsModelStartAngles + flapsModelRotationVector * currentFlapsValue01 * config.FlapsRotationAngle;
        }
	}

	protected override float CalculateLift()
    {
        return config.GetLift(VelocityMagnitude, VerticalAOA, currentFlapsValue01);
    }
}
