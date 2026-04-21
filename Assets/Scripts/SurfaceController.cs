using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(AerodynamicSurface))]
public class SurfaceController : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector = new(1, 0, 0);
    [SerializeField] [Range(-1, 1)] private float trim;

    private AerodynamicSurface surface;
    private Vector3 startAngles;

    private float PlayerInput
    {
        get
        {
            switch (surface.GetSurfaceType())
            {
                case SurfaceType.AileronR:
                    return Controls.singletone.YokeInput.x;
                case SurfaceType.AileronL:
                    return -Controls.singletone.YokeInput.x;
                case SurfaceType.Pitch:
                    return - Controls.singletone.YokeInput.y;
                default:
                    return Controls.singletone.RudderInput;
            }
        }
    }
    private float FullInput => PlayerInput + trim;

    

    private void Start()
    {
        startAngles = transform.localEulerAngles;
        surface = GetComponent<AerodynamicSurface>();
    }

    float currentAngle = 0;

    private void Update()
    {
        float speed = surface.GetSpeedAndAOA().speed;
        //float rotationAngle = AnglesMultOverSpeed.Evaluate(speed);

        //currentOffset = Vector3.MoveTowards(currentOffset, SurfaceRotationMaxAngles * FullInput * rotationAngle, Time.deltaTime * SurfaceRotationSpeed);
        currentAngle = surface.surfaceParams.GetRotationAngle(FullInput, speed, currentAngle, Time.deltaTime);

        transform.localEulerAngles = startAngles + rotationVector * currentAngle;
    }
}
