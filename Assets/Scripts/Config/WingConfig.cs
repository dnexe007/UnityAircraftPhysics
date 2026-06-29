using System;
using UnityEngine;

[Serializable]
public class WingConfig
{
	[SerializeField] private Common.QuadDragAnchor LiftAnchorFlapsZero = new(60, 100_000);
	[SerializeField] private Common.QuadDragAnchor LiftAnchorFlapsFull = new(35, 100_000);
	[SerializeField]
	private AnimationCurve BasicLiftMultOverAOA = new(
		new(-25, -0.1f),
		new(-16, -1),
		new(-13, -1),
		new(0, 0),
		new(13, 1),
		new(16, 1),
		new(25, 0.1f)
	);
	[SerializeField]
	private AnimationCurve HighSpeedLiftMultOverAOA = new(
		new(-25, -0.1f),
		new(-16 / 2, -1 * 3),
		new(-13 / 2, -1 * 3),
		new(0, 0),
		new(13 / 2, 1 * 3),
		new(16 / 2, 1 * 3),
		new(25, 0.1f)
	);
	[SerializeField]
	private AnimationCurve AOACurvesBlendOverSpeed = new(
		new(60, 0),
		new(120, 1)
	);

	public float GetLift(float speed, float angleOfAttack, float flapsValue)
	{
		float flapsZeroLift = LiftAnchorFlapsZero.GetDrag(speed);
		float flapsFullLift = LiftAnchorFlapsFull.GetDrag(speed);
		float currentFlapsLift = Mathf.Lerp(flapsZeroLift, flapsFullLift, flapsValue);


		float basicAOAMult = BasicLiftMultOverAOA.Evaluate(angleOfAttack);
		float highSpeedAOAMult = HighSpeedLiftMultOverAOA.Evaluate(angleOfAttack);
		float blendValue = AOACurvesBlendOverSpeed.Evaluate(speed);
		float totalAOAMult = Mathf.Lerp(basicAOAMult, highSpeedAOAMult, blendValue);

		return currentFlapsLift * totalAOAMult;
	}
}
