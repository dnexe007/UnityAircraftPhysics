using UnityEngine;

public class SidewaysResistance : MonoBehaviour
{
    public AnimationCurve TerminalXSpeedOverZSpeed = new(
        new(0, 50),
        new(50, 20)
    );

    public AnimationCurve SteerHelperOverRoll = new(
        new(3, 0),
        new(25, 1)
    );

    public float RotatingFactor = 0.09f;

    public AnimationCurve RotFactorMultOverSteerHelper = new(
        new(0, 1),
        new(1, 0.1f)
    );

    public AnimationCurve ResistanceMultOverSteerHelper = new(
        new(0, 1),
        new(1, 4)
    );

    private Rigidbody rb;
    private PlaneInfo info;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponent<PlaneInfo>();   
    }

    private float CalculateForce(float speed, float terminalSpeed)
    {
        float speedMult = 9.81f / Mathf.Pow(Mathf.Abs(terminalSpeed), 2);
        return Mathf.Pow(Mathf.Abs(speed), 2) * speedMult;
    }

    private void FixedUpdate()
    {
        float terminalSpeed = TerminalXSpeedOverZSpeed.Evaluate(info.localVelocity.z);
        float deccelerationForce = CalculateForce(info.localVelocity.x, terminalSpeed);


        float rollAngle = Vector3.Angle(transform.right, Vector3.up) - 90;
        bool IsDriftingReversed = Mathf.Sign(rollAngle) != Mathf.Sign(info.localVelocity.x);
        float steerHelperValue = IsDriftingReversed ? SteerHelperOverRoll.Evaluate(Mathf.Abs(rollAngle)) : 0;


        float forceMult = ResistanceMultOverSteerHelper.Evaluate(steerHelperValue);
        rb.AddForce(-transform.right * Mathf.Sign(info.localVelocity.x) * deccelerationForce * forceMult, ForceMode.Acceleration);


        float torqueMult = RotFactorMultOverSteerHelper.Evaluate(steerHelperValue);
        rb.AddTorque(transform.up * deccelerationForce * Mathf.Sign(info.localVelocity.x) * RotatingFactor * torqueMult, ForceMode.Acceleration);
    }
}
