using System;
using UnityEngine;

[Serializable]
public class AngularDragConfig
{
	[SerializeField] private float basicDragForce;
	[SerializeField] private Vector3 axesCoefs;
	[SerializeField] private QuadDragAnchor speedFactorAnchor = new(750, 10);
	public Vector3 GetAngularDrag(Vector3 localAngularVelocity, float linearVelocityMagnitude)
	{
		float speedFactor = 1 + speedFactorAnchor.GetDrag(linearVelocityMagnitude);

		return basicDragForce * speedFactor * new Vector3(
			GetAxisDrag(localAngularVelocity.x, axesCoefs.x),
			GetAxisDrag(localAngularVelocity.y, axesCoefs.y),
			GetAxisDrag(localAngularVelocity.z, axesCoefs.z)
		);
	}

	private float GetAxisDrag(float angularVel, float axisCoef)
	{
		return -Mathf.Abs(angularVel) * angularVel * axisCoef;
	}
}
