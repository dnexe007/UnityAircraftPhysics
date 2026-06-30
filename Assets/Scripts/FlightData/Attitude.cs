using UnityEngine;

public struct Attitude
{
    public float roll;
    public float pitch;
    public Vector3 rightHorizontalVector;

	private static Vector3 CalculateRightHorizontalVector(Transform transform)
	{
		Vector3 rightHorizontalVector = Vector3.Cross(Vector3.up, transform.forward).normalized;
		if (rightHorizontalVector == Vector3.zero)
			rightHorizontalVector = transform.right;
		return rightHorizontalVector;
	}

	private static float CalculateRoll(Transform transform, Vector3 rightHorizontalVector)
	{
		return Vector3.SignedAngle(transform.right, rightHorizontalVector, transform.forward);
	}

	private static float CalculatePitch(Transform transform)
	{
		Vector3 fdProject = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
		if (fdProject == Vector3.zero)
			fdProject = Vector3.forward;
		return Vector3.Angle(fdProject, transform.forward) * Mathf.Sign(transform.forward.y);
	}

	public Attitude(Transform transform)
    {
		rightHorizontalVector = CalculateRightHorizontalVector(transform);
		roll = CalculateRoll(transform, rightHorizontalVector);
		pitch = CalculatePitch(transform);
	}
}


