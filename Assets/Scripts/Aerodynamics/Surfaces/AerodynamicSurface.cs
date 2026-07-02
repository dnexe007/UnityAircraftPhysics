using UnityEngine;

public class AerodynamicSurface : AerodynamicSurfaceBase
{
    public string surfaceType;

    private AerodynamicSurfaceConfig surfaceConfig;

	protected override void Start()
	{
		base.Start();
		surfaceConfig = config.GetSurfaceConfigByName(surfaceType);
	}

    public string GetSurfaceType() => surfaceType;

    protected override void ApplyForce()
    {
        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * surfaceConfig.GetLift(data.speed, data.aoa);

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
