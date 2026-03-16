using System;
using UnityEngine;

[Serializable]
public class WingLiftProfile
{
    [SerializeField] private AnimationCurve LiftMultOverAOA = new(
        new(-25, -0.1f),
        new(-15, -0.5f),
        new(0, 0),
        new(15, 1),
        new(25, 0.1f)
    );
    [SerializeField] private float FlapsZeroStallSpeed = 100;
    [SerializeField] private float FlapsFullStallSpeed = 60;

    private float FlapsForceMult => Physics.gravity.magnitude / CalculatePeakLift(FlapsFullStallSpeed);

    private float CalculatePeakLift(float speed)
    {
        //Mathf.Pow(FlapsZeroStallSpeed, 2) * mult = gravity
        float mult = Physics.gravity.magnitude / Mathf.Pow(FlapsZeroStallSpeed, 2);
        return Mathf.Pow(speed, 2) * mult;
    }

    public float GetLift(float speed, float angleOfAttack, float flapsValue)
    {
        float flapsZeroLift = CalculatePeakLift(speed) * LiftMultOverAOA.Evaluate(angleOfAttack);
        float flapsLiftMultiplier = Mathf.Lerp(1, FlapsForceMult, flapsValue);
        return flapsZeroLift * flapsLiftMultiplier;
    }
}
