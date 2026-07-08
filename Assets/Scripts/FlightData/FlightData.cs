using UnityEngine;

public class FlightData : MonoBehaviour
{
    private Rigidbody rb;
    private AircraftSetup setup;

    public Vector3 LocalVelocity { get; private set; }
    public float Roll { get; private set; }
    public float Pitch { get; private set; }
    public Vector3 RightHorizontalVector { get; private set; }
    public float VerticalAOA { get; private set; }
    public float HorizontalAOA { get; private set; }
    public float ThrustValue { get; private set; }
    public int FlapsValue { get; private set; }
    public float FlapsValue01 => (float)FlapsValue / setup.config.WingConfig.FlapsSteps;


	private void OnDestroy()
	{
        if(Controls.singleton != null)
        {
			Controls.singleton.OnFlapsChange -= ChangeFlaps;
			Controls.singleton.OnThrustChange -= ChangeThrust;
		}
	}


	private void ChangeThrust(float delta)
    {
        ThrustValue = Mathf.Clamp01(ThrustValue + delta);
    }

    private void ChangeFlaps(int delta)
    {
        FlapsValue = Mathf.Clamp(FlapsValue + delta, 0, setup.config.WingConfig.FlapsSteps);
    }

	private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setup = GetComponentInParent<AircraftSetup>();

		Controls.singleton.OnFlapsChange += ChangeFlaps;
		Controls.singleton.OnThrustChange += ChangeThrust;
	}
    private void FixedUpdate()
    {
        LocalVelocity = transform.InverseTransformDirection(rb.velocity);

        RightHorizontalVector = Attitude.CalculateRightHorizontalVector(transform);
        Roll = Attitude.CalculateRoll(transform, RightHorizontalVector);
        Pitch = Attitude.CalculatePitch(transform);


        VerticalAOA = AnglesOfAttack.GetVerticalAOA(LocalVelocity);
        HorizontalAOA = AnglesOfAttack.GetHorizontalAOA(LocalVelocity);
    }
}
