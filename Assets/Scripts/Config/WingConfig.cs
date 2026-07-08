using System;
using UnityEngine;

[Serializable]
public class WingConfig
{
	[SerializeField] private DragAnchor liftAnchorFlapsZero = new(60, 100_000);
	[SerializeField] private DragAnchor liftAnchorFlapsFull = new(50, 100_000);
	[SerializeField]
	private AnimationCurve liftMultOverAOA = new(
		new(-25, -0.1f, -0.24f, -0.24f),
		new(-15, -1f, 0, 0),
		new(0, 0, 0.15f, 0.15f),
		new(15, 1f, 0, 0),
		new(25, 0.1f, -0.24f, -0.24f)
	);
	[SerializeField] [Range(1, 10)] private int flapsSteps = 5;

	public int FlapsSteps => flapsSteps;

	public float GetLift(float speed, float angleOfAttack, float flapsValue)
	{

		float basicForce = Mathf.Lerp(
			liftAnchorFlapsZero.GetQuadraticDrag(speed),
			liftAnchorFlapsFull.GetQuadraticDrag(speed),
			flapsValue
		);

		float angleOfAttackMult = liftMultOverAOA.Evaluate(angleOfAttack);

		return basicForce * angleOfAttackMult;
	}
}