using System;
using UnityEngine;

[Serializable]
public class SurfaceController
{
	private enum InputType { Roll, Pitch, Yaw, None}


	[SerializeField] private InputType inputType = InputType.None;
	[SerializeField] private bool invertInput;

    public float CurrentRotationAngle { get; private set; }
    
	private float GetInput(Aircraft root)
    {
        float input = inputType switch
        {
            InputType.Roll => root.RollInput,
            InputType.Pitch => root.PitchInput,
            InputType.Yaw => root.YawInput,
            _ => 0,
        };

        return input * (invertInput ? -1 : 1);
    }

    public void UpdateAngle(
        float maxRotationAngle,
        float rotationSpeed,
        Aircraft root
    )
    {
        CurrentRotationAngle = Mathf.MoveTowards(
            CurrentRotationAngle,
            maxRotationAngle * GetInput(root),
            Time.deltaTime * rotationSpeed
        );
    }
}
