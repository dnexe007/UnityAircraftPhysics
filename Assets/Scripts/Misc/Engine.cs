using UnityEngine;

public class Engine : MonoBehaviour
{
    private Rigidbody rb;
    private FlightData fd;
    private AircraftSetup setup;

    private void ApplyEngines()
    {
        rb.AddForceAtPosition(transform.forward * setup.config.engineConfig.thrust * fd.ThrustValue, transform.position);
    }

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        fd = GetComponentInParent<FlightData>();
        setup = fd.GetComponentInParent<AircraftSetup>();
    }

    private void FixedUpdate() => ApplyEngines();

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
