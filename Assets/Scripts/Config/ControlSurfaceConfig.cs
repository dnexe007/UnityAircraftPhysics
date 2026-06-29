using System;
using UnityEngine;

[Serializable]
public class ControlSurfaceConfig
{
	[SerializeField] private Common.QuadDragAnchor liftAnchor = new(60, 3_000);
	[SerializeField]
	private AnimationCurve liftMultOverAOA = new(
		new(20, 1),
		new(0, 0),
		new(-20, -1)
	);

	[SerializeField] private float surfaceRotationAngle = 30;
	[SerializeField]
	private AnimationCurve rotationAngleMultOverSpeed = new(
		new(0, 1),
		new(400, 0.1f)
	);
	[SerializeField] private float surfaceRotationSpeed = 60;

	public float GetRotationAngle(float playerInput, float movementSpeed, float currentAngle, float deltaTime)
	{
		float targetAngle = rotationAngleMultOverSpeed.Evaluate(movementSpeed) * playerInput * surfaceRotationAngle;
		return Mathf.MoveTowards(currentAngle, targetAngle, deltaTime * surfaceRotationSpeed);
	}

	public float GetLift(float speed, float angleOfAttack)
	{
		float basicLift = liftAnchor.GetDrag(speed);
		float mult = liftMultOverAOA.Evaluate(angleOfAttack);
		return basicLift * mult;
	}
}