using System;
using UnityEngine;

[Serializable]
public class WingLiftProfile
{
    [SerializeField] private AnimationCurve PeakLiftOverSpeed = new(
        new(0, 0),
        new(100, 9.81f),
        new(200, 100)
    );

    [SerializeField] private float StallLiftClamp = 2;

    [SerializeField] private Vector2[] LiftMultOverAOA = {
        new(-25, 0.2f),
        new(-18, -0.5f),
        new(0, 0),
        new(15, 1),
        new(25, 0.2f)
    };

    [SerializeField] private float MaxFlapsLiftMult = 3;

    private float ApplyAngleOfAttack(float peakLift, float angleOfAttack)
    {
        for (int rightEdgeIndex = 1; rightEdgeIndex < LiftMultOverAOA.Length; rightEdgeIndex++)
        {
            if (angleOfAttack <= LiftMultOverAOA[rightEdgeIndex].x || rightEdgeIndex == LiftMultOverAOA.Length - 1)
            {
                int leftEdgeIndex = rightEdgeIndex - 1;

                float leftEdgeLift = LiftMultOverAOA[leftEdgeIndex].y * peakLift;
                float rightEdgeLift = LiftMultOverAOA[rightEdgeIndex].y * peakLift;

                //apply lift limits for stall keyframes
                if (leftEdgeIndex == 0)
                    leftEdgeLift = Mathf.Max(leftEdgeLift, -StallLiftClamp);
                if (rightEdgeIndex == LiftMultOverAOA.Length - 1)
                    rightEdgeLift = Mathf.Min(rightEdgeLift, StallLiftClamp);

                float lerpValue = Mathf.InverseLerp(
                    LiftMultOverAOA[leftEdgeIndex].x,
                    LiftMultOverAOA[rightEdgeIndex].x,
                    angleOfAttack
                );

                return Mathf.Lerp(leftEdgeLift, rightEdgeLift, lerpValue);
            }
        }
        return 0;
    }

    public float GetWingsForce(float speed, float attackAngle, float flapsValue)
    {
        float peakForce = PeakLiftOverSpeed.Evaluate(speed);

        float flapsMult = Mathf.Lerp(1, MaxFlapsLiftMult, flapsValue);

        return ApplyAngleOfAttack(peakForce, attackAngle) * flapsMult;
    }
}
