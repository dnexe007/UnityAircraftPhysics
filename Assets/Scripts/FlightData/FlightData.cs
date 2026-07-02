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


    public void SetThrustValue(float value)
    {
        ThrustValue = Mathf.Clamp01(value);
    }

    public void SetFlapsValue(int value)
    {
        FlapsValue = Mathf.Clamp(value, 0, setup.config.wingParams.flapsSteps);
    }

	private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setup = GetComponentInParent<AircraftSetup>();
    }
    private void FixedUpdate()
    {
        LocalVelocity = transform.InverseTransformDirection(rb.velocity);

        Attitude attitude = new(transform);
        Roll = attitude.roll;
        Pitch = attitude.pitch;
        RightHorizontalVector = attitude.rightHorizontalVector;

        AnglesOfAttack aoa = new(LocalVelocity);
        VerticalAOA = aoa.vertical;
        HorizontalAOA = aoa.horizontal;
    }
}
