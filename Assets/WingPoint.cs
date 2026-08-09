using UnityEngine;

public struct WingPoint
{
	public Vector3 Position { get; private set; }
	public Vector3 Forward { get; private set; }
	public Vector3 Normal { get; private set; }

	public float PositionForceMult { get; private set; }
	public float TotalForceMult { get; private set; }
	public float VelocityMagnitude { get; private set; }
	public float VerticalAOA { get; private set; }

	public readonly Vector3 Right => Vector3.Cross(Normal, Forward).normalized;


	public WingPoint(
		Vector3 position,
		Vector3 forward,
		Vector3 normal,
		float positionForceMult,
		int numOfPoints,
		Rigidbody rb = null
	)
	{
		Position = position;
		Forward = forward;
		Normal = normal;
		PositionForceMult = positionForceMult;
		TotalForceMult = positionForceMult / numOfPoints;
		VelocityMagnitude = VerticalAOA = 0;

		if (rb != null)
		{
			Vector3 pointVelocity = rb.GetPointVelocity(Position);
			Vector3 forwardVelocity = Vector3.Project(pointVelocity, Forward);
			Vector3 verticalVelocity = Vector3.Project(pointVelocity, Normal);

			Vector3 projectedVelocity = forwardVelocity + verticalVelocity;

			VerticalAOA = Vector3.SignedAngle(Forward, projectedVelocity, Right);
			VelocityMagnitude = (forwardVelocity + verticalVelocity).magnitude;
		}
	}
}