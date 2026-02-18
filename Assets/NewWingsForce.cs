using System;
using UnityEngine;

[Serializable]
public class AttackAngleCurve
{
    [Serializable]
    public class AngleKeyframe
    {
        public float attackAngle;
        public float forceMult;

        public AngleKeyframe(float angle, float value)
        {
            attackAngle = angle;
            forceMult = value;
        }
    }

    public AngleKeyframe NegativeStall = new(-20, 0.2f);
    public AngleKeyframe NegativePeak = new(-15, -0.5f);

    public AngleKeyframe Center = new(0, 0);

    public AngleKeyframe PositivePeak = new(15, 1);
    public AngleKeyframe PositiveStall = new(25, 0.2f);

    public float StallForceLimit = 6;


    public float GetForce(float peakForce, float attackAngle)
    {
        AngleKeyframe leftEdge, rightEdge;

        if (attackAngle <= NegativePeak.attackAngle)
        {
            leftEdge = NegativeStall;
            rightEdge = NegativePeak;
        }

        else if (attackAngle <= Center.attackAngle)
        {
            leftEdge = NegativePeak;
            rightEdge = Center;
        }

        else if (attackAngle <= PositivePeak.attackAngle)
        {
            leftEdge = Center;
            rightEdge = PositivePeak;
        }

        else
        {
            leftEdge = PositivePeak;
            rightEdge = PositiveStall;
        }

        return CalculateForce(peakForce, attackAngle, leftEdge, rightEdge);
    }

    private float CalculateForce(float peakForce, float attackAngle, AngleKeyframe leftEdge, AngleKeyframe rightEdge)
    {
        float leftEdgeForce = leftEdge.forceMult * peakForce;
        if (leftEdge == NegativeStall) leftEdgeForce = Mathf.Max(leftEdgeForce, -StallForceLimit);

        float rightEdgeForce = rightEdge.forceMult * peakForce;
        if (rightEdge == PositiveStall) rightEdgeForce = Mathf.Min(rightEdgeForce, StallForceLimit);

        float lerpValue = Mathf.InverseLerp(leftEdge.attackAngle, rightEdge.attackAngle, attackAngle);
        return Mathf.Lerp(leftEdgeForce, rightEdgeForce, lerpValue);
    }
}


[Serializable]
public class WingProfile
{
    public AttackAngleCurve attackAngleParams;
    public float NoFlapsStallSpeed = 60;
    public float FullFlapsStallSpeed = 100;


    private float GetFlapsPeakForce(float speed)
    {
        // 9.81 == FullFlapsStallSpeed^2 * speedMult
        float speedMult = 9.81f / Mathf.Pow(FullFlapsStallSpeed, 2);

        return Mathf.Pow(speed, 2) * speedMult;
    }

    private float noFlapsMult => 9.81f / GetFlapsPeakForce(NoFlapsStallSpeed);

    public float GetWingsForce(float speed, float attackAngle, float flapsValue)
    {
        float peakForce = GetFlapsPeakForce(speed);

        float flapsMult = Mathf.Lerp(noFlapsMult, 1, flapsValue);

        return attackAngleParams.GetForce(peakForce, attackAngle) * flapsMult;
    }
}
