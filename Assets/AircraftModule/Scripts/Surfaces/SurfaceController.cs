using UnityEngine;

[RequireComponent(typeof(AerodynamicSurface))]
public class SurfaceController : MonoBehaviour
{
	private enum InputType { Roll, Pitch, Yaw, None}


	[SerializeField] private Vector3 rotationVector = new(1, 0, 0);
	[SerializeField] private InputType inputType = InputType.None;
	[SerializeField] private bool invertInput;


	private Vector3 startAngles;
	private float currentAngle;


	private AerodynamicSurface surface;
    private Aircraft root;

    
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


    private void Start()
    {
        startAngles = transform.localEulerAngles;

        surface = GetComponent<AerodynamicSurface>();
        root = GetComponentInParent<Aircraft>();
    }


    private void Update()
    {
        currentAngle = Mathf.Lerp(
            currentAngle,
            surface.MaxRotationAngle * GetInput(),
            Time.deltaTime * surface.RotationSpeed
        );

        transform.localEulerAngles = startAngles + rotationVector * currentAngle;
    }
}
