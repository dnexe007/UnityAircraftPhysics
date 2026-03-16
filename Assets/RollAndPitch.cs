using UnityEngine;

public class RollAndPitch : MonoBehaviour
{
    public float roll { get; private set; }
    public float pitch { get; private set; }
    public Vector3 rightHorizontalVector { get; private set; }

    private void FixedUpdate()
    {
        rightHorizontalVector = Vector3.Cross(Vector3.up, transform.forward).normalized;
        if (rightHorizontalVector == Vector3.zero)
            rightHorizontalVector = transform.right;

        roll = Vector3.SignedAngle(transform.right, rightHorizontalVector, transform.forward);

        Vector3 fdProject = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        if (fdProject == Vector3.zero)
            fdProject = Vector3.forward;
        pitch = Vector3.Angle(fdProject, transform.forward) * Mathf.Sign(transform.forward.y);
    }
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + rightHorizontalVector * 15);
        }
    }
}
