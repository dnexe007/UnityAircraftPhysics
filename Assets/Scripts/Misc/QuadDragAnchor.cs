using UnityEngine;
using System;

[Serializable]
public struct QuadDragAnchor
{
	[SerializeField] private float anchorSpeed;
	[SerializeField] private float anchorForce;

	public QuadDragAnchor(float speed, float force)
	{
		anchorSpeed = speed;
		anchorForce = force;
	}

	public readonly float GetDrag(float speed)
	{
		float speedMult = anchorForce / Mathf.Pow(anchorSpeed, 2);
		return Mathf.Pow(speed, 2) * speedMult;
	}
}