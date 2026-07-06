using System;
using UnityEngine;

[Serializable]
public class MovementDragConfig
{
	public struct DragData
	{
		public Vector3 force;
		public Vector3 position;
	}

	[SerializeField] private AnimationCurve dragMultOverAOA = new(
		new(0f, 1f, 0.1f, 0.1f),
		new(90f, 10f, 0f, 0f),
		new(180f, 1f, -0.1f, -0.1f)
	);

	[SerializeField] private AnimationCurve rotatingFactorMultOverAOA = new(
		new(0, 1),
		new(90, 0),
		new(180, -1)
	);

	[SerializeField] private QuadDragAnchor dragAnchor = new(500, 200_000);

	[SerializeField] private float rotatingFactor = -0.5f;

	public DragData GetFuselageDrag(Vector3 velocity, Vector3 forward, Vector3 worldCOM)
	{
		float basicDrag = dragAnchor.GetDrag(velocity.magnitude);
		float dragMult = dragMultOverAOA.Evaluate(Vector3.Angle(velocity, forward));
		Vector3 force = -velocity.normalized * basicDrag * dragMult;

		Vector3 position = worldCOM + forward * rotatingFactor;

		return new() { force = force, position = position };
	}

	public Vector3 GetDragVector(Vector3 velocity, float angleOfAttack)
	{
		float basicDrag = dragAnchor.GetDrag(velocity.magnitude);
		float angleMult = dragMultOverAOA.Evaluate(angleOfAttack);
		return -velocity.normalized * basicDrag * angleMult;
	}

	public float GetRotatingFactor(float angleOfAttack)
	{
		float angleMult = rotatingFactorMultOverAOA.Evaluate(angleOfAttack);
		return rotatingFactor * angleMult;
	}
}