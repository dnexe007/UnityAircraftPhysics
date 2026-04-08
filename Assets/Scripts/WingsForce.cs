using UnityEngine;

public class WingsForce : MonoBehaviour
{
    public WingLiftProfile WingParams;

    private Rigidbody rb;
    private FlightData flightData;
    
    private Vector3 totalForce;


    [Range(0, 1)] public float FlapsValue;


    private void ApplyWingForce()
    {
        float speed = new Vector2(flightData.LocalVelocity.z, flightData.LocalVelocity.y).magnitude;
        float forwardSpeed = flightData.LocalVelocity.z;

        Vector3 centerPosition = transform.TransformPoint(rb.centerOfMass);


        Vector3 liftVector = transform.up * WingParams.GetLift(speed, flightData.VerticalAOA, FlapsValue);
        
        rb.AddForceAtPosition(
            liftVector, 
            centerPosition, 
            ForceMode.Acceleration
        );
    }
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
