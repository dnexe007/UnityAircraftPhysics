using UnityEngine;

[RequireComponent(typeof(AerodynamicSurface))]
public class SurfaceController : MonoBehaviour
{
	private enum InputType { Roll, Pitch, Yaw, None}


	[SerializeField] private Vector3 rotationVector = new(1, 0, 0);
    [SerializeField] private Vector3 modelRotationVector = new(1, 0, 0);
    [SerializeField] private Transform model;
	[SerializeField] private InputType inputType = InputType.None;
	[SerializeField] private bool invertInput;


	private Vector3 startAngles;
    private Vector3 modelStartAngles;
	public float CurrentAngle { get; private set; }


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

        if(model != null) modelStartAngles = model.localEulerAngles;

        surface = GetComponent<AerodynamicSurface>();
        root = GetComponentInParent<Aircraft>();
    }


    private void Update()
    {
        CurrentAngle = Mathf.MoveTowards(
            CurrentAngle,
            surface.MaxRotationAngle * GetInput(),
            Time.deltaTime * surface.RotationSpeed
        );

        transform.localEulerAngles = startAngles + rotationVector * CurrentAngle;



        if(model != null)
        {
            model.localEulerAngles = modelStartAngles + modelRotationVector * CurrentAngle;
        }
    }
}
