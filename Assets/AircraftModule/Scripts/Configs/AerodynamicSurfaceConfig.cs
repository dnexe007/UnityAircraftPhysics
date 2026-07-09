using System;
using UnityEngine;

[Serializable]
public class AerodynamicSurfaceConfig
{
	[SerializeField] private string surfaceName;
	[SerializeField] private DragAnchor liftAnchor;
	[SerializeField] private AnimationCurve liftMultOverAOA;
	[SerializeField] private float maxRotationAngle;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private AnimationCurve angleMultOverSpeed;

	public static AerodynamicSurfaceConfig DefaultSetup => new()
	{
		surfaceName = "Default",
		liftAnchor = new(60, 10000),
		liftMultOverAOA = new(
			new(-20f, -1f, 0f, 0f),
			new(0f, 0f, 0.112f, 0.112f),
			new(20f, 1f, 0f, 0f)
		),
		maxRotationAngle = 30,
		rotationSpeed = 60,
		angleMultOverSpeed = new(
			new(0, 1),
			new(750, 0.1f)
		)
	};

	public string SurfaceName => surfaceName;

	public float RotationSpeed => rotationSpeed;

	public float GetLift(float velocityMagnitude, float angleOfAttack)
	{
		float basicLift = liftAnchor.GetQuadraticDrag(velocityMagnitude);
		float mult = liftMultOverAOA.Evaluate(angleOfAttack);
		return basicLift * mult;
	}

	public float GetMaxRotationAngle(float velocityMagnitude)
	{
		return maxRotationAngle * angleMultOverSpeed.Evaluate(velocityMagnitude);
	}
}