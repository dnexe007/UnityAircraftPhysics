using System;
using UnityEngine;

[Serializable] public class AerodynamicSurfaceConfig
{
	[SerializeField] private string surfaceName;
	[SerializeField] private DragAnchor liftAnchor;
	[SerializeField] private SurfaceAOAConfig AOAConfig;

	public string SurfaceName => surfaceName;

	public float GetLift(SurfaceMovementData movementData)
	{
		float basicLift = liftAnchor.GetQuadraticDrag(movementData.velocityMagnitude);
		float AOAMult = AOAConfig.GetAOAMult(movementData);

		return basicLift * AOAMult;
	}
}