using System;
using UnityEngine;

[Serializable]
public class AngularDragConfig
{
	[SerializeField] private float basicDrag = 20000;
	[SerializeField] private Vector3 axesCoefs = new(1, 1, 1);
	[SerializeField] private QuadDragAnchor speedStabilityAnchor = new(200, 10);

	public Vector3 GetAngularDrag(Vector3 localAngularVelocity, float speed)
	{
		float speedMult = 1 + speedStabilityAnchor.GetDrag(speed);


		return basicDrag * speedMult * new Vector3(
			-localAngularVelocity.x * axesCoefs.x,
			-localAngularVelocity.y * axesCoefs.y,
			-localAngularVelocity.z * axesCoefs.z
		);
	}
}
