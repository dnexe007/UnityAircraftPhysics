using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private bool FollowRoll;
    [SerializeField] private float RotationSpeed = 3;
    [SerializeField] private PlayerControls player;

    private Transform aircraftTransform;
    private Aircraft aircraftRoot;

    private Quaternion currentRt;

    private void Start()
    {
        currentRt = player.Aircraft.transform.rotation;

        aircraftRoot = player.Aircraft;
        aircraftTransform = aircraftRoot.transform.Find("Body");
    }
    private void Update()
    {
        transform.position = aircraftTransform.position;

        Vector3 noRollUp = Vector3.Cross(aircraftTransform.forward, aircraftRoot.RightHorizontalVector);
        Vector3 rollUp = aircraftTransform.up;

        Quaternion targetRt = Quaternion.LookRotation(
            aircraftTransform.forward, 
            FollowRoll? rollUp: noRollUp
        );

        currentRt = Quaternion.Slerp(currentRt, targetRt, RotationSpeed * Time.deltaTime);
        transform.rotation = currentRt;
    }
}
