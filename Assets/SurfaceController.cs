using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(AerodynamicSurface))]
public class SurfaceController : MonoBehaviour
{
    [SerializeField] private string PositiveKey;
    [SerializeField] private string NegativeKey;
    [SerializeField] private Vector3 SurfaceRotationMaxAngles = new(30, 0, 0);
    [SerializeField] private float SurfaceRotationSpeed = 30;
    [SerializeField] [Range(-1, 1)] private float trim;

    private AerodynamicSurface surface;
    private Vector3 startAngles;
    private Vector3 currentOffset;

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

    private void Update()
    {
        currentOffset = Vector3.MoveTowards(currentOffset, SurfaceRotationMaxAngles * FullInput, Time.deltaTime * SurfaceRotationSpeed);
        transform.localEulerAngles = startAngles + currentOffset;
    }
}
