using UnityEngine;

public class WingsForce : MonoBehaviour
{
    public WingLiftProfile WingParams;

    private Rigidbody rb;
    private FlightData flightData;
    
    private Vector3 totalForce;

    public Vector3 ProjectResult;

    public float ForcePointsWidth = 3;
    public float HorizontalForceMult = 1.5f;


    [Range(0, 1)] public float RightFlaps;
    [Range(0, 1)] public float LeftFlaps;

    public AnimationCurve SideForceOverRoll = new(
        new(0, 0),
        new(30, 1),
        new(45, 0)
    );

    public AnimationCurve SideForceOverSpeed = new(
        new(0, 0),
        new(60, 10),
        new(200, 15)
    );

    public float SlipZOffset = -3;
    private void ApplyWingForce()
    {
        float speed = new Vector2(flightData.LocalVelocity.z, flightData.LocalVelocity.y).magnitude;
        float forwardSpeed = flightData.LocalVelocity.z;

        Vector3 centerPosition = transform.TransformPoint(rb.centerOfMass);

        /* Vector3 offset = transform.right * ForcePointsWidth / 2;


         Vector3 rightWingLift = transform.up * WingParams.GetLift(speed, flightData.VerticalAOA, RightFlaps);
         Vector3 leftWingLift = transform.up * WingParams.GetLift(speed, flightData.VerticalAOA, LeftFlaps);

         ProjectResult = Vector3.Project(transform.up, flightData.RightHorizontalVector);

         float rollAngle = Vector3.Angle(transform.right, Vector3.up) - 90;

         IsRight = rollAngle > 1;

         rb.AddForceAtPosition(rightWingLift / 2, centerPosition + offset, ForceMode.Acceleration);
         rb.AddForceAtPosition(leftWingLift / 2, centerPosition - offset, ForceMode.Acceleration);*/

        Vector3 rightWingLift = transform.up * WingParams.GetLift(speed, flightData.VerticalAOA, RightFlaps);
        Vector3 horizontalLift = Vector3.Project(rightWingLift, flightData.RightHorizontalVector);
        
        rb.AddForceAtPosition(
            rightWingLift, 
            centerPosition, 
            ForceMode.Acceleration
        );

        /*rb.AddForceAtPosition(
            transform.right *
            SideForceOverRoll.Evaluate(Mathf.Abs(flightData.Roll)) *
            Mathf.Sign(flightData.Roll) *
            SideForceOverSpeed.Evaluate(flightData.LocalVelocity.z),
            centerPosition + transform.up * SlipZOffset,
            ForceMode.Acceleration
        );*/
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
