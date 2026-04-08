using UnityEngine;

public class AirResistance : MonoBehaviour
{
    private Rigidbody rb;
    private FlightData fd;

    public Common.QuadDragAnchor VerticalResistanceAnchor = new(50, 9.81f);
    public Common.QuadDragAnchor SidewaysResistanceAnchor = new(60, 9.81f);
    public Common.QuadDragAnchor SpeedStabilityAnchor = new(200, 3f);

    public Common.QuadDragAnchor ForwardResistanceAnchor = new(150, 9.81f);

    public float verticalSpeed;
    public float sidewaysSpeed;
    public float forwardSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponent<FlightData>();
    }
   
    private void FixedUpdate()
    {
        verticalSpeed = fd.LocalVelocity.y;
        float verticalResistance = VerticalResistanceAnchor.GetDrag(verticalSpeed);

        sidewaysSpeed = fd.LocalVelocity.x;
        float sidewaysResistance = SidewaysResistanceAnchor.GetDrag(sidewaysSpeed);

        forwardSpeed = fd.LocalVelocity.z;
        float stabilityFactor = 1 + SpeedStabilityAnchor.GetDrag(forwardSpeed);
        float forwardResistance = ForwardResistanceAnchor.GetDrag(sidewaysSpeed);

        float newVerticalSpeed = Mathf.MoveTowards(
            verticalSpeed,
            0,
            verticalResistance * Time.fixedDeltaTime * stabilityFactor
        );

        float newSidewaysSpeed = Mathf.MoveTowards(
            sidewaysSpeed,
            0,
            sidewaysResistance * Time.fixedDeltaTime * stabilityFactor
        );

        float newForwardSpeed = Mathf.MoveTowards(
            forwardSpeed,
            0,
            forwardResistance * Time.fixedDeltaTime
        );

        var velocityDelta = new Vector3(
            newSidewaysSpeed - sidewaysSpeed,
            newVerticalSpeed - verticalSpeed,
            newForwardSpeed - forwardSpeed
        );

        rb.AddRelativeForce(velocityDelta, ForceMode.VelocityChange);
    }
}
