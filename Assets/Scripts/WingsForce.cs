using UnityEngine;

public class WingsForce : MonoBehaviour
{
    public WingLiftProfile WingParams;

    private Rigidbody rb;
    private PlaneInfo info;
    
    private Vector3 totalForce;


    public float ForcePointsWidth = 5;


    [Range(0, 1)] public float RightFlaps;
    [Range(0, 1)] public float LeftFlaps;


    public float ZOffset = 3;
    public float XForce = 10;

    RollAndPitch rt;

    private void ApplyWingForce()
    {
        float speed = new Vector2(info.LocalVelocity.z, info.LocalVelocity.y).magnitude;

        Vector3 centerPosition = transform.TransformPoint(rb.centerOfMass);
        Vector3 offset = transform.right * ForcePointsWidth / 2;


        Vector3 rightWingLift = transform.up * WingParams.GetLift(speed, info.AttackAngle, RightFlaps);
        Vector3 leftWingLift = transform.up * WingParams.GetLift(speed, info.AttackAngle, LeftFlaps);

        // ZOffset = rotatingFactorOverAOA.Evaluate(info.AttackAngle);

        float rollAngle = Vector3.Angle(transform.right, Vector3.up) - 90;

        IsRight = rollAngle > 1;

        float rightWingSide = speed * speed *  rollAngle * XForce / 100000; 


        Vector3 zofffset = transform.forward * ZOffset;

        rb.AddForceAtPosition(rt.rightHorizontalVector * rightWingSide, centerPosition + zofffset, ForceMode.Acceleration);
        rb.AddForceAtPosition(rightWingLift / 2, centerPosition + offset, ForceMode.Acceleration);
        rb.AddForceAtPosition(leftWingLift / 2, centerPosition - offset, ForceMode.Acceleration);
    }
    public bool IsRight;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponent<PlaneInfo>();
        rt = GetComponent<RollAndPitch>();
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
