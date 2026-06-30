using UnityEngine;

public struct AnglesOfAttack
{
    public float vertical;
    public float horizontal;

    public AnglesOfAttack(Vector3 localVelocity)
    {
        if (localVelocity.sqrMagnitude < 1) vertical = horizontal = 0;

        else
        {
			vertical = -Mathf.Atan2(localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;
			horizontal = -Mathf.Atan2(localVelocity.x, localVelocity.z) * Mathf.Rad2Deg;
		}
    }
}
