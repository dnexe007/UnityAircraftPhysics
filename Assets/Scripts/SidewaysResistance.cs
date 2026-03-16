using UnityEngine;

public class SidewaysResistance : MonoBehaviour
{
    public AnimationCurve TerminalXSpeedOverZSpeed = new(
        new(0, 50),
        new(50, 20)
    );

    public float RotatingFactor = 0.09f;

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
        float terminalSpeed = TerminalXSpeedOverZSpeed.Evaluate(info.LocalVelocity.z);
        float deccelerationForce = CalculateForce(info.LocalVelocity.x, terminalSpeed);




        rb.AddForce(-transform.right * Mathf.Sign(info.LocalVelocity.x) * deccelerationForce, ForceMode.Acceleration);


        rb.AddTorque(transform.up * deccelerationForce * Mathf.Sign(info.LocalVelocity.x) * RotatingFactor, ForceMode.Acceleration);
    }
}
