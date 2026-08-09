using UnityEngine;

public class FlapAnimator : MonoBehaviour
{
	[SerializeField] private Vector3 rotationVector;
	[SerializeField] private Vector3 flap2MovementVector;
	[SerializeField] private Wing wing;


	private Transform flap2;


	private Vector3 startAngles;
	private Vector3 flap2StartPos;

	private void Start()
	{
		startAngles = transform.localEulerAngles;
		flap2 = transform.Find("Flap2");
		flap2StartPos = flap2.localPosition;
	}

	private void Update()
	{
		float deployment = Mathf.Clamp01(wing.CurrentFlapDeployment01);
		transform.localEulerAngles = startAngles + rotationVector * deployment;
		flap2.localPosition = flap2StartPos + flap2MovementVector * deployment;
	}
}
