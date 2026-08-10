using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensions
{
    public static Vector3 GetLocalVelocity(this Rigidbody rb, Transform transform)
    {
        return transform.InverseTransformDirection(rb.velocity);
    }

	public static Vector3 GetLocalVelocity(this Rigidbody rb, Transform transform, Vector3 position)
	{
		return transform.InverseTransformDirection(
			rb.GetPointVelocity(position)
		);
	}
}
