using UnityEngine;

public class WingsForce : MonoBehaviour
{
    public WingLiftProfile WingParams;

    private Rigidbody rb;
    private PlaneInfo info;
    
    private Vector3 totalForce;


    public float ForcePointsWidth = 5;


    [Range(0, 1)] public float FlapsValue;

    private void ApplyWingForce()
    {
        float speed = new Vector3(info.localVelocity.y, info.localVelocity.z, 0).magnitude;

        Vector3 centerPosition = transform.TransformPoint(rb.centerOfMass) ;
        Vector3 offset = transform.right * ForcePointsWidth / 2;


        Vector3 wingForce = transform.up * WingParams.GetWingsForce(speed, info.attackAngle, FlapsValue);

        rb.AddForceAtPosition(wingForce / 2, centerPosition + offset, ForceMode.Acceleration);
        rb.AddForceAtPosition(wingForce / 2, centerPosition - offset, ForceMode.Acceleration);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        info = GetComponent<PlaneInfo>();
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
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(0, 50, 500, 50), $"path vector angle: {Mathf.Round(Vector3.Angle(rb.velocity, transform.forward))}", style);

        Vector3 fdParalel = transform.forward;
        fdParalel.y = 0;
        float angle = Vector3.Angle(fdParalel, transform.forward);
        GUI.Label(new Rect(0, 100, 500, 50), $"pitch angle: {Mathf.Round(angle)}", style);
    }
}
