using UnityEngine;

public class SurfaceAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
	[SerializeField] private AerodynamicSurfaceBase surface;

	private Vector3 startAngles;

	private void Start()
	{
		startAngles = transform.localEulerAngles;
	}

	public void Update()
	{
		if (surface == null) return;
		transform.localEulerAngles = (
			startAngles +
			rotationVector * surface.RotationAngle
		);
	}
}

