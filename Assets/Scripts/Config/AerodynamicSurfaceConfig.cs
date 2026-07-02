using System;
using UnityEngine;

[Serializable]
public class AerodynamicSurfaceConfig
{
	[SerializeField] private string surfaceName = "Default";
	[SerializeField] private Common.QuadDragAnchor liftAnchor = new(150, 50000);
	[SerializeField] private float speedClamp = 150;
	[SerializeField]
	private AnimationCurve liftMultOverAOA = new(
		new(20, 1),
		new(0, 0),
		new(-20, -1)
	);

	public string SurfaceName => surfaceName;

	public float GetLift(float speed, float angleOfAttack)
	{
		speed = Mathf.Clamp(speed, -speedClamp, speedClamp);
		float basicLift = liftAnchor.GetDrag(speed);
		float mult = liftMultOverAOA.Evaluate(angleOfAttack);
		return basicLift * mult;
	}
}