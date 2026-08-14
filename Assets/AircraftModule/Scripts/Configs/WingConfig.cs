using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class WingConfig
{
	[SerializeField] private DragAnchor liftAnchorFlapsZero = new(60, 100_000);
	[SerializeField] private DragAnchor liftAnchorFlapsFull = new(50, 100_000);
	[SerializeField] private AnimationCurve liftMultOverAOA;
	[SerializeField] private float peakAttackAngle;
	[SerializeField] [Range(1, 10)] private int flapsSteps = 5;
	[SerializeField] private float flapsRotationAngle = 30;
	[SerializeField] private float flapsRotationSpeed = 15;
	[SerializeField] private float aileronMaxRotationAngle;
	[SerializeField] private float aileronRotationSpeed;


	public int FlapsSteps => flapsSteps;
	public float FlapsRotationAngle => flapsRotationAngle;



	public float GetLift(float velocityMagnitude, float mainAOA, float rotatingAOA, float flapsValue)
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

		float basicForce = Mathf.Lerp(
			liftAnchorFlapsZero.GetQuadraticDrag(velocityMagnitude),
			liftAnchorFlapsFull.GetQuadraticDrag(velocityMagnitude),
			flapsValue
		);

		return basicForce * mainAOAMult * rotatingAOAMult;
	}


	public float UpdateFlaps(float currentFlapsValue01, float tartgetFlapsValue01, float deltaTime)
	{
		return Mathf.MoveTowards(currentFlapsValue01, tartgetFlapsValue01, flapsRotationSpeed / FlapsRotationAngle * deltaTime);
	}
}