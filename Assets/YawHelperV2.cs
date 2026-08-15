using UnityEngine;

public class YawHelperV2 : MonoBehaviour
{
	private Rigidbody rb;
	private YawHelperConfig config;


	public float forcePointYOffset;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
		config = GetComponentInParent<Aircraft>().Config.YawHelperConfig;
	}

	private void FixedUpdate()
	{
		Vector3 forcePosition = transform.position + transform.up * forcePointYOffset;

		Vector3 localVelocity = transform.InverseTransformDirection(
			rb.GetPointVelocity(forcePosition)
		);

		float movementSpeed = new Vector2(localVelocity.z, localVelocity.y).magnitude;

		Vector3 rightHorizontalVector = Attitude.CalculateRightHorizontalVector(transform);

		float horizontalAOA = AnglesOfAttack.GetHorizontalAOA(localVelocity);

		rb.AddForceAtPosition(
			config.CalculateForce(transform.up, rightHorizontalVector, movementSpeed, horizontalAOA),
			forcePosition,
			ForceMode.Force
		);
	}
}
