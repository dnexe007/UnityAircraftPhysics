using System;
using UnityEngine;

[Serializable]
public class AerodynamicSurfaceConfig
{
	[SerializeField] private string surfaceName;
	[SerializeField] private QuadDragAnchor liftAnchor;
	[SerializeField] private float forceClamp;
	[SerializeField] private AnimationCurve liftMultOverAOA;


	public AerodynamicSurfaceConfig(
		string surfaceName,
		QuadDragAnchor liftAnchor,
		float forceClamp,
		AnimationCurve liftMultOverAOA
	)
	{
		this.surfaceName = surfaceName;
		this.liftAnchor = liftAnchor;
		this.forceClamp = forceClamp;
		this.liftMultOverAOA = liftMultOverAOA;
	}

	public static AerodynamicSurfaceConfig PitchSetup()
	{
		AerodynamicSurfaceConfig config = new(
			"Pitch",
			new(60, 20000),
			150,
			new(
				new(-20f, -1f, 0f, 0f),
				new(0f, 0f, 0.112f, 0.112f),
				new(20f, 1f, 0f, 0f)
			)
		);
		return config;
	}

	public string SurfaceName => surfaceName;

	public float GetLift(float speed, float angleOfAttack)
	{
		float basicLift = Mathf.Clamp(liftAnchor.GetDrag(speed), -forceClamp, forceClamp);
		float mult = liftMultOverAOA.Evaluate(angleOfAttack);
		return basicLift * mult;
	}
}