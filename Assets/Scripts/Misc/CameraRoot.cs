using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    [SerializeField] private bool FollowRoll;
    [SerializeField] private float RotationSpeed = 3;

    private Quaternion currentRt;
    private Aircraft root;

    private void Start()
    {
        root = GetComponentInParent<Aircraft>();
        currentRt = transform.parent.rotation;
    }
    private void Update()
    {
        Vector3 noRollUp = Vector3.Cross(transform.parent.forward, root.RightHorizontalVector);
        Vector3 rollUp = transform.parent.up;

        Quaternion targetRt = Quaternion.LookRotation(
            transform.parent.forward, 
            FollowRoll? rollUp: noRollUp
        );

        currentRt = Quaternion.Slerp(currentRt, targetRt, RotationSpeed * Time.deltaTime);
        transform.rotation = currentRt;
    }
}
