using System;
using UnityEngine;


public class SurfaceController: MonoBehaviour
{
	public enum InputType { Roll, Pitch, Yaw, None }

	[SerializeField] private InputType inputType = InputType.None;
	[SerializeField] private bool invertInput;
	[SerializeField] private SurfaceControllerConfig config;

    private AerodynamicSurfaceBase surface;
    private Aircraft root;
	private Rigidbody rb;

	private void Start()
	{
		surface = GetComponent<AerodynamicSurfaceBase>();
        root = GetComponentInParent<Aircraft>();
		rb = GetComponentInParent<Rigidbody>();
	}

	private float GetInput()
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

	private void Update()
    {
		Vector3 localVelocity = transform.InverseTransformDirection(
			rb.GetPointVelocity(transform.position)
		);
		localVelocity.x = 0;


		float input = GetInput();
		float newAngle = config.UpdateRotationAngle(
			surface.RotationAngle,
			localVelocity.magnitude,
			input,
			Time.deltaTime
		);

        surface.SetRotationAngle(newAngle);
    }
}
