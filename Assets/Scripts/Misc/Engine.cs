using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float ThrustSensitivity = 3;

    private Rigidbody rb;
    private FlightData fd;
    private AircraftSetup setup;

    public float autoThrustTargetSpeed = 60;

    bool ATEnabled;
    private void ApplyEngines()
    {
        rb.AddForceAtPosition(transform.forward * setup.config.enginesThrust * fd.ThrustValue, transform.position, ForceMode.Acceleration);
    }

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        fd = GetComponentInParent<FlightData>();
        setup = fd.GetComponentInParent<AircraftSetup>();
    }

    private void FixedUpdate() => ApplyEngines();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ATEnabled = !ATEnabled;

        if (ATEnabled) fd.SetThrustValue(
            new Vector2(fd.LocalVelocity.y, fd.LocalVelocity.z).magnitude > autoThrustTargetSpeed?
            0: 1
        );

        if (Input.GetKey(KeyCode.LeftShift))
            fd.SetThrustValue(fd.ThrustValue + ThrustSensitivity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
            fd.SetThrustValue(fd.ThrustValue - ThrustSensitivity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 40);
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity.normalized * 40);
        }
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
