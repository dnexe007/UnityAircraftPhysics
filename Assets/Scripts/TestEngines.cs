using UnityEngine;
using static Common;

public class TestEngines : MonoBehaviour
{
    public float LowSpeed;
    public float HighSpeed;

    private Rigidbody rb;
    private FlightData fd;

    private void ApplyEngines()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * fd.aircraftParams.enginesThrust, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftAlt)) SetForwardVelocity(LowSpeed);
        if (Input.GetKey(KeyCode.RightAlt)) SetForwardVelocity(HighSpeed);
    }
    
    private void SetForwardVelocity(float forwardVelocity)
    {
        Vector3 localVel = fd.LocalVelocity;
        localVel.z = forwardVelocity;
        rb.AddRelativeForce(localVel - fd.LocalVelocity, ForceMode.VelocityChange);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponentInParent<FlightData>();
    }

    private void FixedUpdate() => ApplyEngines();
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 40);
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity.normalized * 40);
        }
    }

    private void OnGUI()
    {
        float SpeedMs = fd.LocalVelocity.z;
        float SpeedKmh = SpeedMs * MsToKmh;
        float SpeedKnots = SpeedMs * MsToKnots;

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(0, 0, 500, 50), $"knots: {Mathf.Round(SpeedKnots)}", style);

        float angle = Vector3.Angle(transform.right, Vector3.up) - 90;
        GUI.Label(new Rect(0, 150, 500, 50), $"roll: {Mathf.Round(angle)}", style);
    }
}
