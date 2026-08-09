using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private bool FollowRoll;
    [SerializeField] private float RotationSpeed = 3;
    [SerializeField] private PlayerControls player;

    private Aircraft aircraft;

    private Quaternion currentRt;

    private void Start()
    {
        currentRt = player.Aircraft.transform.rotation;

        aircraft = player.Aircraft;
    }
    private void Update()
    {
        transform.position = aircraft.transform.position;

        Vector3 noRollUp = Vector3.Cross(aircraft.transform.forward, aircraft.RightHorizontalVector);
        Vector3 rollUp = aircraft.transform.up;

        Quaternion targetRt = Quaternion.LookRotation(
            aircraft.transform.forward, 
            FollowRoll? rollUp: noRollUp
        );

        currentRt = Quaternion.Slerp(currentRt, targetRt, RotationSpeed * Time.deltaTime);
        transform.rotation = currentRt;
    }
}
