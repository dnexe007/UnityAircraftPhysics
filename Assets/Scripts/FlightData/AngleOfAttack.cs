using UnityEngine;

public static class AngleOfAttack
{
    public struct AOAData
    {
        public float vertical;
        public float horizontal;

        public AOAData(float vertical, float horizontal)
        {
            this.vertical = vertical;
            this.horizontal = horizontal;
        }
    }

    public static AOAData CalculateAOA(Vector3 localVelocity)
    {
        if (localVelocity.sqrMagnitude < 1)
            return new AOAData(0, 0);

        float vertical = -Mathf.Atan2(localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;
        float horizontal = -Mathf.Atan2(localVelocity.x, localVelocity.z) * Mathf.Rad2Deg;

        return new AOAData(vertical, horizontal);
    }
}
