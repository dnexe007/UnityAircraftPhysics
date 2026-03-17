using UnityEngine;
using static AngleOfAttack;
using static RollAndPitch;

public class FlightData : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 LocalVelocity { get; private set; }
    public float Roll { get; private set; }
    public float Pitch { get; private set; }
    public Vector3 RightHorizontalVector { get; private set; }
    public float VerticalAOA { get; private set; }
    public float HorizontalAOA { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        LocalVelocity = transform.InverseTransformDirection(rb.velocity);

        RollAndPitchData rollAndPitch = CalculateRollAndPitch(transform);
        Roll = rollAndPitch.roll;
        Pitch = rollAndPitch.pitch;
        RightHorizontalVector = rollAndPitch.rightHorizontalVector;

        AOAData anglesOfAttack = CalculateAOA(LocalVelocity);
        VerticalAOA = anglesOfAttack.vertical;
        HorizontalAOA = anglesOfAttack.horizontal;
    }
}
