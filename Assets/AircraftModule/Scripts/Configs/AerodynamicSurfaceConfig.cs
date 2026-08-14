using System;
using UnityEngine;

[Serializable]
public class AerodynamicSurfaceConfig
{
	[field: SerializeField] public string SurfaceName { get; private set; }
	[SerializeField] private DragAnchor liftAnchor;
	[SerializeField] private AnimationCurve liftMultOverAOA;
	[SerializeField] private float peakAttackAngle;

	public float GetLift(float velocityMagnitude, float mainAOA, float rotatingAOA)
	{
		float mainAOAClampedAbs = (
			Mathf.Max(Mathf.Abs(mainAOA), peakAttackAngle)
		);

		float rotatingAOAClampedAbs = (
			Mathf.Min(Mathf.Abs(rotatingAOA), peakAttackAngle)
		);

		float mainAOAMult = liftMultOverAOA.Evaluate(
			mainAOAClampedAbs
		);

		float rotatingAOAMult = liftMultOverAOA.Evaluate(
			rotatingAOAClampedAbs
		) * Mathf.Sign(rotatingAOA);

		float basicForce = liftAnchor.GetQuadraticDrag(velocityMagnitude);

		return basicForce * mainAOAMult * rotatingAOAMult;
	}
}