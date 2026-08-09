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
		Forward = forward.normalized;
		Normal = normal.normalized;
		PositionForceMult = positionForceMult;
		TotalForceMult = positionForceMult / numOfPoints;
		VelocityMagnitude = VerticalAOA = 0;

		if (rb != null)
		{
			Vector3 pointVelocity = rb.GetPointVelocity(Position);
			float localVelocityZ = Vector3.Dot(pointVelocity, Forward);
			float localVelocityY = Vector3.Dot(pointVelocity, Normal);
			Vector3 localVelocity = new(0, localVelocityY, localVelocityZ);

			VerticalAOA = AnglesOfAttack.GetVerticalAOA(localVelocity);
			VelocityMagnitude = localVelocity.magnitude;
		}
	}
}