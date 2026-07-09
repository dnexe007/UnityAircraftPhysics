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
}
