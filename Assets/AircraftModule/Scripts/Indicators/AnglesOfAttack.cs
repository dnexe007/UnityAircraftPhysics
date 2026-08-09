using UnityEngine;

public static class AnglesOfAttack
{
    public static float GetVerticalAOA(Vector3 localVelocity)
    {
        if(localVelocity.sqrMagnitude < 1) return 0;
		return -Mathf.Atan2(localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;
	}

	public static float GetHorizontalAOA(Vector3 localVelocity)
    {
		if (localVelocity.sqrMagnitude < 1) return 0;
		return -Mathf.Atan2(localVelocity.x, localVelocity.z) * Mathf.Rad2Deg;
	}




	public static float GetVerticalAOA(Vector3 velocity, Vector3 forward, Vector3 normal, Vector3 right)
	{
		if (velocity.sqrMagnitude < 1) return 0;

		Vector3 forwardVelocity = Vector3.Project(velocity, forward);
		Vector3 verticalVelocity = Vector3.Project(velocity, normal);

		Vector3 projectedVelocity = forwardVelocity + verticalVelocity;

		return Vector3.SignedAngle(forward, projectedVelocity, right);
	}


	public static float GetHorizontalAOA(Vector3 velocity, Vector3 forward, Vector3 normal, Vector3 right)
	{
		if (velocity.sqrMagnitude < 1) return 0;

		Vector3 forwardVelocity = Vector3.Project(velocity, forward);
		Vector3 sidewayslVelocity = Vector3.Project(velocity, right);

		Vector3 projectedVelocity = forwardVelocity + sidewayslVelocity;

		return Vector3.SignedAngle(projectedVelocity, forward,  normal);
	}
}
