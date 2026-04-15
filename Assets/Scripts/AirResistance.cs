using UnityEngine;

public class AirResistance : MonoBehaviour
{
    private Rigidbody rb;
    private FlightData fd;


    public Vector3 axesCoefs;

    public Common.QuadDragAnchor DragForceAnchor = new(50, 9.81f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponent<FlightData>();
    }

    public float resistanceMagnitude;
    private void FixedUpdate()
    {
        Vector3 vel = fd.LocalVelocity;

        float xCos = Mathf.Cos(Vector3.Angle(vel, Vector3.right) * Mathf.Deg2Rad);
        float yCos = Mathf.Cos(Vector3.Angle(vel, Vector3.up) * Mathf.Deg2Rad);
        float zCos = Mathf.Cos(Vector3.Angle(vel, Vector3.forward) * Mathf.Deg2Rad);

        float axesMult = Mathf.Sqrt(
            Mathf.Pow(xCos * axesCoefs.x, 2) +
            Mathf.Pow(yCos * axesCoefs.y, 2) +
            Mathf.Pow(zCos * axesCoefs.z, 2)
        );

        Vector3 forceVector = - vel.normalized * axesMult * DragForceAnchor.GetDrag(vel.magnitude);
        rb.AddRelativeForce(forceVector, ForceMode.Acceleration);
        resistanceMagnitude = forceVector.magnitude;
    }
}
