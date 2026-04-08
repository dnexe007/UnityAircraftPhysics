using System;
using UnityEngine;

public static class Common
{
    public const float MsToKmh = 3.6f;
    public const float MsToKnots = 1.94384f;

    [Serializable]
    public struct QuadDragAnchor
    {
        public float anchorSpeed;
        public float anchorForce;

        public QuadDragAnchor(float speed, float force)
        {
            anchorSpeed = speed;
            anchorForce = force;
        }

        public float GetDrag(float speed)
        {
            float speedMult = anchorForce / Mathf.Pow(anchorSpeed, 2);
            return Mathf.Pow(speed, 2) * speedMult;
        }
    }
}
