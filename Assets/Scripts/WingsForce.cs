using UnityEngine;

public class WingsForce : MonoBehaviour
{
    public WingLiftProfile WingParams;

    private Rigidbody rb;
    private FlightData flightData;
    
    private Vector3 totalForce;


    public float ForcePointsWidth = 5;


    [Range(0, 1)] public float RightFlaps;
    [Range(0, 1)] public float LeftFlaps;


    public float ZOffset = 3;
    public float XForce = 10;


    private void ApplyWingForce()
    {
        float speed = new Vector2(flightData.LocalVelocity.z, flightData.LocalVelocity.y).magnitude;

        Vector3 centerPosition = transform.TransformPoint(rb.centerOfMass);
        Vector3 offset = transform.right * ForcePointsWidth / 2;


        Vector3 rightWingLift = transform.up * WingParams.GetLift(speed, flightData.VerticalAOA, RightFlaps);
        Vector3 leftWingLift = transform.up * WingParams.GetLift(speed, flightData.VerticalAOA, LeftFlaps);

        // ZOffset = rotatingFactorOverAOA.Evaluate(info.AttackAngle);

        float rollAngle = Vector3.Angle(transform.right, Vector3.up) - 90;

        IsRight = rollAngle > 1;

        float rightWingSide = speed * speed *  rollAngle * XForce / 100000; 


        Vector3 zofffset = transform.forward * ZOffset;

        rb.AddForceAtPosition(flightData.RightHorizontalVector * rightWingSide, centerPosition + zofffset, ForceMode.Acceleration);
        rb.AddForceAtPosition(rightWingLift / 2, centerPosition + offset, ForceMode.Acceleration);
        rb.AddForceAtPosition(leftWingLift / 2, centerPosition - offset, ForceMode.Acceleration);
    }
    public bool IsRight;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        flightData = GetComponent<FlightData>();
    }

    private void FixedUpdate()
    {
        ApplyWingForce();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 40);
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity.normalized * 40);
            Gizmos.DrawLine(transform.position, transform.position + totalForce);
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new()
        {
            fontSize = 30
        };
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(0, 50, 500, 50), $"path vector angle: {Mathf.Round(Vector3.Angle(rb.velocity, transform.forward))}", style);

        Vector3 fdParalel = transform.forward;
        fdParalel.y = 0;
        float angle = Vector3.Angle(fdParalel, transform.forward);
        GUI.Label(new Rect(0, 100, 500, 50), $"pitch angle: {Mathf.Round(angle)}", style);
    }
}
