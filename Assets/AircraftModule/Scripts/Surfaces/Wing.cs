using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    private WingConfig config;
    private Aircraft root;


    protected override void Start()
    {
        base.Start();
        root = GetComponentInParent<Aircraft>();
		config = root.Config.WingConfig;
    }


    protected override float CalculateLift()
    {
        return config.GetLift(VelocityMagnitude, VerticalAOA, root.FlapsValue01);
    }
}
