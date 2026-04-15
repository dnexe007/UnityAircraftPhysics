using System;
using UnityEngine;

[RequireComponent(typeof(AerodynamicSurface))]
public class SurfaceController : MonoBehaviour
{
    [SerializeField] private string PositiveKey;
    [SerializeField] private string NegativeKey;
    [SerializeField] private Vector3 SurfaceRotationMaxAngles = new(30, 0, 0);
    [SerializeField] private float SurfaceRotationSpeed = 30;
    [SerializeField] [Range(-1, 1)] private float trim;

    private KeyCode PositiveKeycode => (KeyCode)Enum.Parse(typeof(KeyCode), PositiveKey);
    private KeyCode NegativeKeycode => (KeyCode)Enum.Parse(typeof(KeyCode), NegativeKey);
    private float PlayerInput => (Input.GetKey(PositiveKeycode) ? 1 : 0) - (Input.GetKey(NegativeKeycode) ? 1 : 0);
    private float FullInput => PlayerInput + trim;

    private Vector3 startAngles;
    private Vector3 currentOffset;

    private void Start()
    {
        startAngles = transform.localEulerAngles;
    }

    private void Update()
    {
        currentOffset = Vector3.MoveTowards(currentOffset, SurfaceRotationMaxAngles * FullInput, Time.deltaTime * SurfaceRotationSpeed);
        transform.localEulerAngles = startAngles + currentOffset;
    }
}
