using System;
using UnityEngine;

[Serializable]
public class WingConfig
{
	[SerializeField] private QuadDragAnchor liftAnchorFlapsZero = new(60, 100_000);
	[SerializeField] private QuadDragAnchor liftAnchorFlapsFull = new(50, 100_000);
	[SerializeField]
	private AnimationCurve liftMultOverAOA = new(
		new(-25, -0.1f, -0.24f, -0.24f),
		new(-15, -1f, 0, 0),
		new(0, 0, 0.15f, 0.15f),
		new(15, 1f, 0, 0),
		new(25, 0.1f, -0.24f, -0.24f)
	);
	[SerializeField] [Range(1, 10)] private int flapsSteps = 5;
	[SerializeField] private AnimationCurve highSpeedFactorOverSpeed = new(
		new(60f, 0f, 0.025f, 0.025f),
		new(100f, 1f, 0.025f, 0.025f)
	);
	[SerializeField] private float highSpeedAOAMult = 2;
	[SerializeField] private float highSpeedForceMult = 4;

	public int FlapsSteps => flapsSteps;

	public float GetLift(float speed, float angleOfAttack, float flapsValue)
	{
		float highSpeedFactor = highSpeedFactorOverSpeed.Evaluate(speed);

		float basicForce = Mathf.Lerp(
			liftAnchorFlapsZero.GetDrag(speed),
			liftAnchorFlapsFull.GetDrag(speed),
			flapsValue
		) * Mathf.Lerp(1, highSpeedForceMult, highSpeedFactor);

		angleOfAttack *= Mathf.Lerp(1, highSpeedAOAMult, highSpeedFactor);
		float angleOfAttackMult = liftMultOverAOA.Evaluate(angleOfAttack);

		return basicForce * angleOfAttackMult;
	}
}
