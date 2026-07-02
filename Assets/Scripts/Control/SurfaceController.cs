using UnityEngine;

[RequireComponent(typeof(AerodynamicSurface))]
public class SurfaceController : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector = new(1, 0, 0);
    [SerializeField] private bool invertInput;
    [SerializeField] private float rotationAngle = 30;
    [SerializeField] private float rotationSpeed = 60;


    private Vector3 startAngles;
	float currentAngle;
	private AerodynamicSurface surface;


	private float PlayerInput => Controls.GetInputByName(surface.surfaceType) * (invertInput? -1: 1);


    private void Start()
    {
        startAngles = transform.localEulerAngles;

        surface = GetComponent<AerodynamicSurface>();
    }


    private void Update()
    {
        currentAngle = Mathf.MoveTowards(currentAngle, rotationAngle * PlayerInput, Time.deltaTime * rotationSpeed);

        transform.localEulerAngles = startAngles + rotationVector * currentAngle;
    }
}
