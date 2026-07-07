using System;
using UnityEngine;

[Serializable]
public class AngularDragConfig
{
	[SerializeField] private float basicDragForce;
	[SerializeField] private Vector3 axesCoefs;

	[SerializeField] private float anchorSpeed = 100;
	[SerializeField] private float anchorSpeedForceMult = 10;

	public Vector3 GetAngularDrag(Vector3 localAngularVelocity, float linearVelocityMagnitude)
	{
		float speedFactor = Mathf.LerpUnclamped(
			1,
			anchorSpeedForceMult,
			linearVelocityMagnitude / anchorSpeed
		);

		return basicDragForce * speedFactor * new Vector3(
			GetAxisDrag(localAngularVelocity.x, axesCoefs.x),
			GetAxisDrag(localAngularVelocity.y, axesCoefs.y),
			GetAxisDrag(localAngularVelocity.z, axesCoefs.z)
		);
	}

	private float GetAxisDrag(float angularVel, float axisCoef)
	{
		float quadFactor = Mathf.Abs(angularVel) * angularVel;
		float linearFactor = Mathf.Clamp(angularVel, -1, 1);
		return - (quadFactor + linearFactor) * axisCoef;
	}
}
