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


    void ChangeFlaps(int x)
    {
        //print($"flaps change: {x}");
        fd.SetFlapsValue(fd.FlapsValue + x);
       // print($"flaps: {fd.FlapsValue}");
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
}
