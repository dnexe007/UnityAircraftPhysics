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

	[SerializeField] private float rotatingFactor = -3;

	public Vector3 GetDragVector(Vector3 velocity, float angleOfAttack)
	{
		float basicDrag = dragSpeedAnchor.GetDrag(velocity.magnitude);
		float angleMult = dragMultOverAOA.Evaluate(angleOfAttack);
		return angleMult * basicDrag * -velocity.normalized;
	}

	public float GetRotatingFactor(float angleOfAttack)
	{
		float angleMult = Mathf.Lerp(1, -1, Mathf.InverseLerp(0, 180, angleOfAttack));
		return rotatingFactor * angleMult;
	}
}