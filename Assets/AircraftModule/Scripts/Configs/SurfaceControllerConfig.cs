using System;
using UnityEngine;

[Serializable] public class SurfaceControllerConfig
{
	[SerializeField] private float maxRotationAngle;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private AnimationCurve angleMultOverSpeed;

	public float UpdateRotationAngle(
		float currentAngle,
		float velocityMagnitude,
		float input,
		float deltaTime
	)
	{
		return Mathf.MoveTowards(
			currentAngle,
			maxRotationAngle * input *
			angleMultOverSpeed.Evaluate(velocityMagnitude),
			deltaTime * rotationSpeed
		);
	}
}