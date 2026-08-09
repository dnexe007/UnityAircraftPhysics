using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensions
{
    public static Vector3 GetLocalVelocity(this Rigidbody rb)
    {
        return rb.transform.InverseTransformDirection(rb.velocity);
    }

	public static Vector3 GetLocalVelocity(this Rigidbody rb, Vector3 position)
	{
		return rb.transform.InverseTransformDirection(
			rb.GetPointVelocity(position)
		);
	}
}
