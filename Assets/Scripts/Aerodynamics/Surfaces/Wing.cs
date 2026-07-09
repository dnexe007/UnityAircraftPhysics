using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    private Aircraft root;
    protected override void Start()
    {
        base.Start();
        root = GetComponentInParent<Aircraft>();
    }
    protected override float CalculateLift()
    {
        return config.WingConfig.GetLift(VelocityMagnitude, VerticalAOA, root.FlapsValue01);
    }
}
