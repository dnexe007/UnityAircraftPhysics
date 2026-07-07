using System;
using UnityEngine;

[Serializable]
public class AerodynamicSurfaceConfig
{
	[SerializeField] private string surfaceName;
	[SerializeField] private QuadDragAnchor liftAnchor;
	[SerializeField] private AnimationCurve liftMultOverAOA;
	[SerializeField] private float maxRotationAngle;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private AnimationCurve angleMultOverSpeed;


	public AerodynamicSurfaceConfig(
		string surfaceName,
		QuadDragAnchor liftAnchor,
		AnimationCurve liftMultOverAOA,
		float maxRotationAngle,
		float rotationSpeed,
		AnimationCurve angleMultOverSpeed
	)
	{
		this.surfaceName = surfaceName;
		this.liftAnchor = liftAnchor;
		this.liftMultOverAOA = liftMultOverAOA;
		this.maxRotationAngle = maxRotationAngle;
		this.rotationSpeed = rotationSpeed;
		this.angleMultOverSpeed = angleMultOverSpeed;
	}

	public static AerodynamicSurfaceConfig PitchSetup()
	{
		AerodynamicSurfaceConfig config = new(
			"Pitch",
			new(60, 10000),
			new(
				new(-20f, -1f, 0f, 0f),
				new(0f, 0f, 0.112f, 0.112f),
				new(20f, 1f, 0f, 0f)
			),
			30,
			7,
			new(
				new(0, 1),
				new(750, 0.1f)
			)
		);
		return config;
	}

	public string SurfaceName => surfaceName;

	public float RotationSpeed => rotationSpeed;

	public float GetLift(float velocityMagnitude, float angleOfAttack)
	{
		float basicLift = liftAnchor.GetDrag(velocityMagnitude);
		float mult = liftMultOverAOA.Evaluate(angleOfAttack);
		return basicLift * mult;
	}

	public float GetMaxRotationAngle(float velocityMagnitude)
	{
		return maxRotationAngle * angleMultOverSpeed.Evaluate(velocityMagnitude);
	}
}