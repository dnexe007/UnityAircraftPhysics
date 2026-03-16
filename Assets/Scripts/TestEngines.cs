using UnityEngine;
using static Common;

public class TestEngines : MonoBehaviour
{
    public float LowSpeed;
    public AnimationCurve EngineForceOverSpeed = new AnimationCurve(new Keyframe(0, 8), new Keyframe(400, 3));

    private Rigidbody rb;
    private PlaneInfo info;

    public float SpeedKnots;
    public float SpeedKmh;
    public float SpeedMs;

    private void ApplyEngines()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float acceleration = EngineForceOverSpeed.Evaluate(rb.velocity.magnitude * MsToKnots) / MsToKnots;
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftAlt)) rb.velocity = transform.forward * LowSpeed;
        if (Input.GetKey(KeyCode.RightAlt)) rb.velocity = transform.forward * 400/ 1.94384f;
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponentInParent<PlaneInfo>();
    }

    public float rotationSpeed;

    private void FixedUpdate()
    {
        ApplyEngines();
        rotationSpeed = rb.angularVelocity.x * Mathf.Rad2Deg;
    }

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
        SpeedMs = info.LocalVelocity.z;
        SpeedKmh = SpeedMs * MsToKmh;
        SpeedKnots = SpeedMs * MsToKnots;

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(0, 0, 500, 50), $"knots: {Mathf.Round(SpeedKnots)}", style);

        float angle = Vector3.Angle(transform.right, Vector3.up) - 90;
        GUI.Label(new Rect(0, 150, 500, 50), $"roll: {Mathf.Round(angle)}", style);
    }
}
