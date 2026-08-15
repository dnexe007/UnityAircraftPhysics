using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class WingConfig
{
	[SerializeField] private SurfaceAOAConfig AOAConfig; 
	[SerializeField] private DragAnchor liftAnchorFlapsZero = new(60, 100_000);
	[SerializeField] private DragAnchor liftAnchorFlapsFull = new(50, 100_000);
	
	[SerializeField] [Range(1, 10)] private int flapsSteps = 5;
	[SerializeField] private float flapsRotationAngle = 30;
	[SerializeField] private float flapsRotationSpeed = 15;


	public int FlapsSteps => flapsSteps;
	public float FlapsRotationAngle => flapsRotationAngle;

	private float GetBasicLift(float velocityMagnitude, float flapsValue)
	{
		return Mathf.Lerp(
			liftAnchorFlapsZero.GetQuadraticDrag(velocityMagnitude),
			liftAnchorFlapsFull.GetQuadraticDrag(velocityMagnitude),
			flapsValue
		);
	}

	public float GetLift(
		SurfaceMovementData movementData,
		float flapsDeployment01
	)
	{
		float basicLift = GetBasicLift(movementData.velocityMagnitude, flapsDeployment01);
		float AOAMult = AOAConfig.GetAOAMult(movementData);

		return basicLift * AOAMult;
	}

	public float UpdateFlaps(float currentFlapsValue01, float tartgetFlapsValue01, float deltaTime)
	{
		return Mathf.MoveTowards(currentFlapsValue01, tartgetFlapsValue01, flapsRotationSpeed / FlapsRotationAngle * deltaTime);
	}
}