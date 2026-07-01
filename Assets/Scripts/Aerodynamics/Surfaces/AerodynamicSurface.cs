using UnityEngine;

public enum SurfaceType
{
    AileronR,
    AileronL,
    Pitch,
    Rudder
}

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    [SerializeField] private SurfaceType surfaceType;

    public SurfaceType GetSurfaceType() => surfaceType;
    public ControlSurfaceConfig surfaceParams
    {
        get
        {
            switch (surfaceType)
            {
                case SurfaceType.AileronR:
                    return config.aileronParams;
                case SurfaceType.AileronL:
                    return config.aileronParams;
                case SurfaceType.Pitch:
                    return config.pitchParams;
                default:
                    return config.rudderParams;
            }
        }
    }

    protected override void ApplyForce()
    {
        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * surfaceParams.GetLift(data.speed, data.aoa);

        rb.AddForceAtPosition(
            liftVector
            ,
            transform.position
            ,
            ForceMode.Force
        );
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;

		Vector3 lf = transform.position + transform.forward / 2 - transform.right / 2;
		Vector3 lb = transform.position - transform.forward / 2 - transform.right / 2;
		Vector3 rf = transform.position + transform.forward / 2 + transform.right / 2;
		Vector3 rb = transform.position - transform.forward / 2 + transform.right / 2;

		Gizmos.DrawLine(lf, lb);
		Gizmos.DrawLine(lb, rb);
		Gizmos.DrawLine(rb, rf);
		Gizmos.DrawLine(rf, lf);

		Gizmos.DrawWireSphere(transform.position, 0.125f);
	}
}
