using System;
using UnityEngine;

[Serializable] public class SurfaceAOAConfig
{
	[SerializeField] private AnimationCurve liftMultOverVerticalAOA;
	[SerializeField] private float peakVerticalAOA;
	[SerializeField] private AnimationCurve liftMultOverHorizontalAOA;

	private float GetVerticalAOAMult(float mainVerticalAOA, float rotatingVerticalAOA)
	{
		float mainAOAClampedAbs = (
			Mathf.Max(Mathf.Abs(mainVerticalAOA), peakVerticalAOA)
		);
		float rotatingAOAClampedAbs = (
			Mathf.Min(Mathf.Abs(rotatingVerticalAOA), peakVerticalAOA)
		);
		float mainVerticalAOAMult = liftMultOverVerticalAOA.Evaluate(
			mainAOAClampedAbs
		);
		float rotatingVerticalAOAMult = liftMultOverVerticalAOA.Evaluate(
			rotatingAOAClampedAbs
		) * Mathf.Sign(rotatingVerticalAOA);

		return mainVerticalAOAMult * rotatingVerticalAOAMult;
	}

	private float GetHorizontalAOAMult(float horizontalAOA)
	{
		return liftMultOverHorizontalAOA.Evaluate(
			Mathf.Abs(horizontalAOA)
		);
	}

	public float GetAOAMult(
		float mainVerticalAOA,
		float rotatingVerticalAOA,
		float horizontalAOA
	)
	{
		return (
			GetVerticalAOAMult(mainVerticalAOA, rotatingVerticalAOA) *
			GetHorizontalAOAMult(horizontalAOA)
		);
	}

	public float GetAOAMult(SurfaceMovementData movementData)
	{
		return GetAOAMult(
			movementData.mainVerticalAOA,
			movementData.rotatingVerticalAOA,
			movementData.horizontalAOA
		);
	}
}
