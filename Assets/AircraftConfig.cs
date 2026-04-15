using System;
using UnityEngine;


[Serializable]
public class AirSurfaceCFG
{
    [SerializeField] protected Common.QuadDragAnchor LiftAnchor = new(200, 200000);
    [SerializeField] protected AnimationCurve LiftMultOverAOA = new(
        new(20, 1),

        new(0, 0),

        new(-20, -1)
    );

    public float GetLift(float speed, float angleOfAttack)
    {
        float basicLift = LiftAnchor.GetDrag(speed);
        float mult = LiftMultOverAOA.Evaluate(angleOfAttack);
        return basicLift * mult;
    }
}


[Serializable]
public class WingCFG 
{
    [SerializeField] private Common.QuadDragAnchor LiftAnchorFlapsZero = new (60, 300000);
    [SerializeField] private Common.QuadDragAnchor LiftAnchorFlapsFull = new(35, 300000);
    [SerializeField] private AnimationCurve BasicLiftMultOverAOA = new(
            new (-25, -0.1f),
            new (-15, -1),
            new (0, 0),
            new (15, 1),
            new (25, 0.1f)
    );
    [SerializeField] private AnimationCurve HighSPeedLiftMultOverAOA = new(
            new(-25, -0.1f),
            new(-15 / 2, -1 * 3),
            new(0, 0),
            new(15 / 2, 1 * 3),
            new(25, 0.1f)
    );
    [SerializeField] private AnimationCurve AOACurvesBlendOverSpeed = new(
        new(100, 0),
        new(200, 1)
    );

    public float GetLift(float speed, float angleOfAttack, float flapsValue)
    {
        float flapsZeroLift = LiftAnchorFlapsZero.GetDrag(speed);
        float flapsFullLift = LiftAnchorFlapsFull.GetDrag(speed);
        float currentFlapsLift = Mathf.Lerp(flapsZeroLift, flapsFullLift, flapsValue);


        float basicAOAMult = BasicLiftMultOverAOA.Evaluate(angleOfAttack);
        float highSpeedAOAMult = HighSPeedLiftMultOverAOA.Evaluate(angleOfAttack);
        float blendValue = AOACurvesBlendOverSpeed.Evaluate(speed);
        float totalAOAMult = Mathf.Lerp(basicAOAMult, highSpeedAOAMult, blendValue);

        return currentFlapsLift * totalAOAMult;
    }
}


[Serializable]
public class FuselageDragCFG
{
    public AnimationCurve resistanceMultOverFlowAngle = new(
        new(0, 1),
        new(90, 10),
        new(180, 1)
    );

    public Common.QuadDragAnchor resistanceAnchor = new(200, 5);

    public Vector3 forcePointOffset = new(0, 0, -1);
}


[Serializable]
public class FuselageAngularDragCFG
{
    public float basicDrag = 0.5f;
    public  Vector3 axesCoefs = new(1, 1, 1);
    public Common.QuadDragAnchor speedStabilityAnchor = new(200, 10);
}


[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
    public float mass = 30000;
    public Vector3 tensor = new(200000, 200000, 200000);
    public float enginesThrust = 6;
    public WingCFG wingParams;
    public AirSurfaceCFG pitchParams;
    public FuselageDragCFG fuselageDragParams;
    public FuselageAngularDragCFG fuselageAngularDragParams;
}
