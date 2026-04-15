using UnityEngine;

public class AdditionalSideSlip : MonoBehaviour
{
    [SerializeField] private Common.QuadDragAnchor slipOverSpeedAnchor = new(200, 1);

    [SerializeField] private AnimationCurve slipMultOverRoll = new(
        new(0, 0),
        new(30, 1)
    );

    private Rigidbody rb;
    private FlightData fd;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fd = GetComponent<FlightData>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(
            transform.right *
            Mathf.Sign(fd.Roll) *
            slipOverSpeedAnchor.GetDrag(fd.LocalVelocity.z) *
            slipMultOverRoll.Evaluate(Mathf.Abs(fd.Roll))
            ,
            ForceMode.Acceleration
        );
    }
}
