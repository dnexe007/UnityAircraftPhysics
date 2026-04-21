using System;
using UnityEngine;


[Serializable]
public class ControlSurfaceCFG
{
    [SerializeField] private Common.QuadDragAnchor liftAnchor = new(60, 3_000);
    [SerializeField] private AnimationCurve liftMultOverAOA = new(
        new(20, 1),
        new(0, 0),
        new(-20, -1)
    );
    
    [SerializeField] private float surfaceRotationAngle = 30;
    [SerializeField] private AnimationCurve rotationAngleMultOverSpeed = new(
        new(0, 1),
        new(400, 0.1f)
    );
    [SerializeField] private float surfaceRotationSpeed = 60;

    public float GetRotationAngle(float playerInput, float movementSpeed, float currentAngle, float deltaTime)
    {
        float targetAngle = rotationAngleMultOverSpeed.Evaluate(movementSpeed) * playerInput * surfaceRotationAngle;
        return Mathf.MoveTowards(currentAngle, targetAngle, deltaTime * surfaceRotationSpeed);
    }

    public float GetLift(float speed, float angleOfAttack)
    {
        float basicLift = liftAnchor.GetDrag(speed);
        float mult = liftMultOverAOA.Evaluate(angleOfAttack);
        return basicLift * mult;
    }
}


[Serializable]
public class WingCFG 
{
    [SerializeField] private Common.QuadDragAnchor LiftAnchorFlapsZero = new (60, 100_000);
    [SerializeField] private Common.QuadDragAnchor LiftAnchorFlapsFull = new(35, 100_000);
    [SerializeField] private AnimationCurve BasicLiftMultOverAOA = new(
        new (-25, -0.1f),
        new (-16, -1),
        new(-13, -1),
        new(0, 0),
        new (13, 1),
        new (16, 1),
        new (25, 0.1f)
    );
    [SerializeField] private AnimationCurve HighSpeedLiftMultOverAOA = new(
        new(-25, -0.1f),
        new(-16 / 2, -1 * 3),
        new(-13/2, -1 * 3),
        new(0, 0),
        new(13/2, 1 * 3),
        new(16/2, 1 * 3),
        new(25, 0.1f)
    );
    [SerializeField] private AnimationCurve AOACurvesBlendOverSpeed = new(
        new(60, 0),
        new(120, 1)
    );

    public float GetLift(float speed, float angleOfAttack, float flapsValue)
    {
        float flapsZeroLift = LiftAnchorFlapsZero.GetDrag(speed);
        float flapsFullLift = LiftAnchorFlapsFull.GetDrag(speed);
        float currentFlapsLift = Mathf.Lerp(flapsZeroLift, flapsFullLift, flapsValue);


        float basicAOAMult = BasicLiftMultOverAOA.Evaluate(angleOfAttack);
        float highSpeedAOAMult = HighSpeedLiftMultOverAOA.Evaluate(angleOfAttack);
        float blendValue = AOACurvesBlendOverSpeed.Evaluate(speed);
        float totalAOAMult = Mathf.Lerp(basicAOAMult, highSpeedAOAMult, blendValue);

        return currentFlapsLift * totalAOAMult;
    }
}


[Serializable]
public class FuselageDragCFG
{
    public struct DragData
    {
        public Vector3 force;
        public Vector3 position;
    }

    [SerializeField] private AnimationCurve resistanceMultOverFlowAngle = new(
        new(0, 1),
        new(90, 10),
        new(180, 1) 
    );
    [SerializeField] private Common.QuadDragAnchor resistanceAnchor = new(400, 120_000);
    [SerializeField] private float rotatingFactor = - 0.5f;

    public DragData GetFuselageDrag(Vector3 velocity, Vector3 forward, Vector3 worldCOM)
    {
        float basicDrag = resistanceAnchor.GetDrag(velocity.magnitude);
        float dragMult = resistanceMultOverFlowAngle.Evaluate(Vector3.Angle(velocity, forward));
        Vector3 force = - velocity.normalized * basicDrag * dragMult;

        Vector3 position = worldCOM + forward * rotatingFactor;

        return new(){ force = force, position = position };
    }
}


[Serializable]
public class FuselageAngularDragCFG
{
    //[SerializeField] private float basicDrag = 50_000;
    [SerializeField] private Vector3 axesCoefs = new(5000, 5000, 5000);
    [SerializeField] private Common.QuadDragAnchor speedStabilityAnchor = new(200, 10);

    public Vector3 GetAngularDrag(Vector3 localAngularVelocity, float speed)
    {
        float speedMult = 1 + speedStabilityAnchor.GetDrag(speed);


        return new Vector3(
            -localAngularVelocity.x * axesCoefs.x,
            -localAngularVelocity.y * axesCoefs.y,
            -localAngularVelocity.z * axesCoefs.z
        ) * speedMult;
    }
}


[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
    public float mass = 9_000;
    public Vector3 tensor = new(70_000, 80_000, 15_000);
    public float enginesThrust = 12;
    [Range(1, 10)] public int flapsSteps = 5;
    public WingCFG wingParams;
    public ControlSurfaceCFG pitchParams;
    public ControlSurfaceCFG aileronParams;
    public ControlSurfaceCFG rudderParams;
    public FuselageDragCFG fuselageDragParams;
    public FuselageAngularDragCFG fuselageAngularDragParams;
}
