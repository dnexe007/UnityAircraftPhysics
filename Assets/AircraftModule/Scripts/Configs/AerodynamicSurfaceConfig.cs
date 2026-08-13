using System;
using UnityEngine;

[Serializable]
public class AerodynamicSurfaceConfig
{
	[field: SerializeField] public string SurfaceName { get; private set; }
	[SerializeField] private DragAnchor liftAnchor;
	[SerializeField] private AnimationCurve liftMultOverAOA;

	public float GetLift(float velocityMagnitude, float angleOfAttack)
	{
		float basicLift = liftAnchor.GetQuadraticDrag(velocityMagnitude);
		float mult = Mathf.Sign(angleOfAttack) * liftMultOverAOA.Evaluate(Mathf.Abs(angleOfAttack));
		return basicLift * mult;
	}
}