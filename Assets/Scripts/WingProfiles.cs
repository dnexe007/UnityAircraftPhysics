using System;
using UnityEngine;

public class AttackAngleProfile
{
    public AnimationCurve LowForce;
    public AnimationCurve HighForce;

    public float LowSpeed;
    public float HighSpeed;
    
    private float GetBlendValue(float speedKnots)
    {
        return Mathf.InverseLerp(LowSpeed, HighSpeed, speedKnots);
    }

    public float GetMult(float attackAngle, float speedKnots)
    {
        if (speedKnots <= 0) return 0;

        float blendValue = GetBlendValue(speedKnots);

        float lowForce = this.LowForce.Evaluate(attackAngle);
        float highForce = this.HighForce.Evaluate(attackAngle);

        return Mathf.Lerp(lowForce, highForce, blendValue);
    }
}

public class BasicForceProfile
{
    public float LowSpeed;
    public float HighSpeed;

    public float LowForce;
    public float HighForce;

    public float GetForce(float speedKnots)
    {
        if (speedKnots <= 0) return 0;

        if(speedKnots < LowSpeed)
        {
            float lerpValue = Mathf.InverseLerp(0, LowSpeed, speedKnots);
            return Mathf.Lerp(0, LowForce, lerpValue);
        }

        else
        {
            float lerpValue = Mathf.InverseLerp(LowSpeed, HighSpeed, speedKnots);
            return Mathf.Lerp(LowForce, HighForce, lerpValue);
        }
    }
}