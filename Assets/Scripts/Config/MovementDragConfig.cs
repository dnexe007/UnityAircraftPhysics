using System;
using UnityEngine;

[Serializable]
public class MovementDragConfig
{
	[SerializeField] private QuadDragAnchor dragSpeedAnchor = new(500, 200_000);

	[SerializeField] private AnimationCurve dragMultOverAOA = new(
		new(0f, 1f, 0.1f, 0.1f),
		new(90f, 10f, 0f, 0f),
		new(180f, 1f, -0.1f, -0.1f)
	);

	[SerializeField] private float rotatingFactor = -0.5f;

	[SerializeField] private AnimationCurve rotatingFactorMultOverAOA = new(
		new(0, 1),
		new(90, 0),
		new(180, -1)
	);

	public Vector3 GetDragVector(Vector3 velocity, float angleOfAttack)
	{
		float basicDrag = dragSpeedAnchor.GetDrag(velocity.magnitude);
		float angleMult = dragMultOverAOA.Evaluate(angleOfAttack);
		return -velocity.normalized * basicDrag * angleMult;
	}

	public float GetRotatingFactor(float angleOfAttack)
	{
		float angleMult = rotatingFactorMultOverAOA.Evaluate(angleOfAttack);
		return rotatingFactor * angleMult;
	}
}