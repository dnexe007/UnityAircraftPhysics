using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class WingConfig
{
	[SerializeField] private DragAnchor liftAnchorFlapsZero = new(60, 100_000);
	[SerializeField] private DragAnchor liftAnchorFlapsFull = new(50, 100_000);
	[SerializeField] private AnimationCurve liftMultOverAOA;
	[SerializeField] [Range(1, 10)] private int flapsSteps = 5;
	[SerializeField] private float flapsRotationAngle = 30;
	[SerializeField] private float flapsRotationSpeed = 15;
	[SerializeField] private float aileronMaxRotationAngle;
	[SerializeField] private float aileronRotationSpeed;


	public int FlapsSteps => flapsSteps;
	public float FlapsRotationAngle => flapsRotationAngle;

	public float GetLift(float speed, float angleOfAttack, float flapsValue)
	{
		float basicForce = Mathf.Lerp(
			liftAnchorFlapsZero.GetQuadraticDrag(speed),
			liftAnchorFlapsFull.GetQuadraticDrag(speed),
			flapsValue
		);

		float angleOfAttackMult = Mathf.Sign(angleOfAttack) * liftMultOverAOA.Evaluate(Mathf.Abs(angleOfAttack));

		return basicForce * angleOfAttackMult;
	}


	public float UpdateFlaps(float currentFlapsValue01, float tartgetFlapsValue01, float deltaTime)
	{
		return Mathf.MoveTowards(currentFlapsValue01, tartgetFlapsValue01, flapsRotationSpeed / FlapsRotationAngle * deltaTime);
	}
}