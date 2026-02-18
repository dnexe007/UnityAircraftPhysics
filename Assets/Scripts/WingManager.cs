using System;
using UnityEngine;

public class WingManager : MonoBehaviour
{
    [Header("Attack angle curves")]
    public AnimationCurve LowForceAttackAngle = new(
        new(-30, -0.2f),
        new(-15, -1),
        new(-2, 0),
        new(0, 0),
        new(4, 1),
        new(10, 2),
        new(25, 0.2f)
    );
    public AnimationCurve HighForceAttackAngle = new(
        new(-30, -0.2f),
        new(-15, -1 * 3),
        new(-2, 0),
        new(0, 0),
        new(4, 1),
        new(10, 2 * 3),
        new(25, 0.2f)
    );

    [Header("Basic force values")]
    public float LowBasicForce = 3;
    public float HighBasicForce = 9.81f;

    [Header("Critical speed values")]
    public float NoFlapsCriticalSpeed = 75;
    public float FlapsCriticalSpeed = 50;

    [Header("Full speed offsets")]
    public float BasicForceFullSpeedOffset = 25;
    public float AttackAngleFullSpeedOffset = 50;

    [Header("Flaps test")]
    [Range(0, 1)] public float FlapsValue;


    private AttackAngleProfile attackAngle;
    private BasicForceProfile basicForce;


    private void Start()
    {
        WingsForce wings = GetComponent<WingsForce>();

      //  attackAngle = wings.AttackAngleParams;
        //basicForce = wings.BasicForceParams;

        attackAngle.LowForce = LowForceAttackAngle;
        attackAngle.HighForce = HighForceAttackAngle;

        basicForce.LowForce = LowBasicForce;
        basicForce.HighForce = HighBasicForce;
    }

    private void FixedUpdate()
    {
        float criticalSpeed = Mathf.Lerp(
            NoFlapsCriticalSpeed,
            FlapsCriticalSpeed,
            FlapsValue
        );

        basicForce.LowSpeed = criticalSpeed;
        attackAngle.LowSpeed = criticalSpeed;

        basicForce.HighSpeed = criticalSpeed + BasicForceFullSpeedOffset;
        attackAngle.HighSpeed = criticalSpeed + AttackAngleFullSpeedOffset;
    }
}
