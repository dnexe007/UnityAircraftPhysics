using UnityEngine;

public class YawHelperV2 : MonoBehaviour
{
	private Rigidbody rb;
	private YawHelperConfig config;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
		config = GetComponentInParent<Aircraft>().Config.YawHelperConfig;
	}

	private void FixedUpdate()
	{
		Vector3 localVelocity = transform.InverseTransformDirection(
			rb.GetPointVelocity(transform.position)
		);

		float movementSpeed = new Vector2(localVelocity.z, localVelocity.y).magnitude;

		Vector3 rightHorizontalVector = Attitude.CalculateRightHorizontalVector(transform);

		float horizontalAOA = AnglesOfAttack.GetHorizontalAOA(localVelocity);

		rb.AddForceAtPosition(
			config.CalculateForce(transform.up, rightHorizontalVector, movementSpeed, horizontalAOA),
			transform.position,
			ForceMode.Force
		);
	}
}
