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

    [SerializeField] private Common.QuadDragAnchor FlapsZeroLiftAnchor = new(100, 9.81f);
    [SerializeField] private Common.QuadDragAnchor FlapsFullLiftAnchor = new(60, 9.81f);

    public float GetLift(float speed, float angleOfAttack, float flapsValue)
    {
        float flapsZeroLift = FlapsZeroLiftAnchor.GetDrag(speed);
        float flapsFullLift = FlapsFullLiftAnchor.GetDrag(speed);
        float currentFlapsLift = Mathf.Lerp(flapsZeroLift, flapsFullLift, flapsValue);

        float angleOfAttackMult = LiftMultOverAOA.Evaluate(angleOfAttack);

        return currentFlapsLift * angleOfAttackMult;
    }
}
