using System;
using UnityEngine;

[Serializable]
public class YawHelperConfig
{
	[SerializeField] private DragAnchor forceDragAnchor = new(100, 70_000);
	[SerializeField] private AnimationCurve forceMultOverHorizontalAOA = new(
		new(3, 1),
		new(5, 0)
	);

	public Vector3 CalculateForce(Vector3 up, Vector3 rightHorizontalVector, float movementSpeed, float horizontalAOA)
	{
		Vector3 forceVector = Vector3.Project(up, rightHorizontalVector);
		float angleMult = forceMultOverHorizontalAOA.Evaluate(Mathf.Abs(horizontalAOA));
		return angleMult * forceDragAnchor.GetQuadraticDrag(movementSpeed) * forceVector;
	}
}
