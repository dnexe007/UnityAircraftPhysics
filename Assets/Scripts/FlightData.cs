using UnityEngine;
using static AngleOfAttack;
using static RollAndPitch;

public class FlightData : MonoBehaviour
{
    public AircraftConfig config;
    private Rigidbody rb;

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
        FlapsValue = Mathf.Clamp(value, 0, config.flapsSteps);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.mass = config.mass;
        rb.inertiaTensor = config.tensor;
    }

    private void FixedUpdate()
    {
        LocalVelocity = transform.InverseTransformDirection(rb.velocity);

        RollAndPitchData rollAndPitch = CalculateRollAndPitch(transform);
        Roll = rollAndPitch.roll;
        Pitch = rollAndPitch.pitch;
        RightHorizontalVector = rollAndPitch.rightHorizontalVector;

        AOAData anglesOfAttack = CalculateAOA(LocalVelocity);
        VerticalAOA = anglesOfAttack.vertical;
        HorizontalAOA = anglesOfAttack.horizontal;
    }
}
