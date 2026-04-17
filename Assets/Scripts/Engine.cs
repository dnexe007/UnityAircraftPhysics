using UnityEngine;
using static Common;

public class Engine : MonoBehaviour
{
    [SerializeField] private float ThrustSensitivity = 3;

    private Rigidbody rb;
    private FlightData fd;

    private void ApplyEngines()
    {
        rb.AddForce(transform.forward * fd.config.enginesThrust * fd.ThrustValue, ForceMode.Acceleration);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponentInParent<FlightData>();
    }

    private void FixedUpdate() => ApplyEngines();

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            fd.SetThrustValue(fd.ThrustValue + ThrustSensitivity * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
            fd.SetThrustValue(fd.ThrustValue - ThrustSensitivity * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 40);
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity.normalized * 40);
        }
    }
}
