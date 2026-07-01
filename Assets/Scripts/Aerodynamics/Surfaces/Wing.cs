using UnityEngine;

public class Wing : AerodynamicSurfaceBase
{
    private FlightData fd;
    protected override void Start()
    {
        base.Start();
        fd = GetComponentInParent<FlightData>();
        Controls.singletone.OnFlapsChange += ChangeFlaps;
    }


    void ChangeFlaps(int delta)
    {
        fd.SetFlapsValue(fd.FlapsValue + delta);
    }

    protected override void ApplyForce()
    {
        SpeedAndAOA data = GetSpeedAndAOA();

        Vector3 liftVector = transform.up * config.wingParams.GetLift(data.speed, data.aoa, fd.FlapsValue/(float)config.flapsSteps);
        
        rb.AddForceAtPosition(
            liftVector, 
            transform.position, 
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
