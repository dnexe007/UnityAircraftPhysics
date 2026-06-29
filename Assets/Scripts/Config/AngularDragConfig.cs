using System;
using UnityEngine;

[Serializable]
public class AngularDragConfig
{
	[SerializeField] private Vector3 axesCoefs = new(5000, 5000, 5000);
	[SerializeField] private Common.QuadDragAnchor speedStabilityAnchor = new(200, 10);

	public Vector3 GetAngularDrag(Vector3 localAngularVelocity, float speed)
	{
		float speedMult = 1 + speedStabilityAnchor.GetDrag(speed);


		return new Vector3(
			-localAngularVelocity.x * axesCoefs.x,
			-localAngularVelocity.y * axesCoefs.y,
			-localAngularVelocity.z * axesCoefs.z
		) * speedMult;
	}
}
