using UnityEngine;
using System;

[Serializable]
public struct DragAnchor
{
	[SerializeField] private float anchorSpeed;
	[SerializeField] private float anchorForce;

	public DragAnchor(float speed, float force)
	{
		anchorSpeed = speed;
		anchorForce = force;
	}

	public readonly float GetQuadraticDrag(float velocityMagnitude, float startForce = 0)
	{
		float velocityQuad = velocityMagnitude * velocityMagnitude;
		float anchorSpeedQuad = anchorSpeed * anchorSpeed;
		float anchorForceWithOffset = anchorForce - startForce;
		return startForce + anchorForceWithOffset * velocityQuad / anchorSpeedQuad;
	}

	public readonly float GetLinearDrag(float velocityMagnitude, float startForce = 0)
	{
		float anchorForceWithOffset = anchorForce - startForce;
		return startForce + anchorForceWithOffset * velocityMagnitude / anchorSpeed;
	}
}