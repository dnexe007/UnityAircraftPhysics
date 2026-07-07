using UnityEngine;

[RequireComponent(typeof(AerodynamicSurface))]
public class SurfaceController : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector = new(1, 0, 0);
    [SerializeField] private bool invertInput;

    private Vector3 startAngles;
	float currentAngle;
	private AerodynamicSurface surface;


	private float PlayerInput => Controls.GetInputByName(surface.SurfaceType) * (invertInput? -1: 1);


    private void Start()
    {
        startAngles = transform.localEulerAngles;

        surface = GetComponent<AerodynamicSurface>();
    }


    private void Update()
    {
        currentAngle = Mathf.Lerp(
            currentAngle,
            surface.MaxRotationAngle * PlayerInput,
            Time.deltaTime * surface.RotationSpeed
        );

        transform.localEulerAngles = startAngles + rotationVector * currentAngle;
    }
}
