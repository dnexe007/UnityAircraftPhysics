using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    [SerializeField][Range(0, 1)] private float FlapsValue;

    private WingCFG wingParams => config.wingParams;

    protected override void ApplyForce()
    {
        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * wingParams.GetLift(data.speed, data.aoa, FlapsValue);
        
        rb.AddForceAtPosition(
            liftVector, 
            transform.position, 
            ForceMode.Force
        );
    }
}
