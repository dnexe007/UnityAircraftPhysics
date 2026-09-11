using System;
using UnityEngine;

[Serializable] public class SurfaceAOAConfig
{
	[SerializeField] private AnimationCurve liftMultOverVerticalAOA;
	[SerializeField] private float peakVerticalAOA;
	[SerializeField] private AnimationCurve liftMultOverHorizontalAOA;


	private float GetBasicMult(float rotatingVerticalAOA)
	{
		float rotatingAOAClampedAbs = (
			Mathf.Min(Mathf.Abs(rotatingVerticalAOA), peakVerticalAOA)
		);

		return liftMultOverVerticalAOA.Evaluate(
			rotatingAOAClampedAbs
		) * Mathf.Sign(rotatingVerticalAOA);
	}

	private float GetBreakdownMult(float mainVerticalAOA, float horizontalAOA)
	{
		float mainVerticalAOAClampedAbs = (
			Mathf.Max(Mathf.Abs(mainVerticalAOA), peakVerticalAOA)
		);

		float verticalBreakdown = liftMultOverVerticalAOA.Evaluate(
			mainVerticalAOAClampedAbs
		);

		float horizontalBreakdown = liftMultOverHorizontalAOA.Evaluate(
			Mathf.Abs(horizontalAOA)
		);

		return Mathf.Min(verticalBreakdown, horizontalBreakdown);
	}

	public float GetAOAMult(
		float mainVerticalAOA,
		float rotatingVerticalAOA,
		float horizontalAOA
	) => (
		GetBasicMult(rotatingVerticalAOA) *
		GetBreakdownMult(mainVerticalAOA, horizontalAOA)
	);

	public float GetAOAMult(
		SurfaceMovementData movementData
	) => GetAOAMult(
		movementData.mainVerticalAOA,
		movementData.rotatingVerticalAOA,
		movementData.horizontalAOA
	);
}
