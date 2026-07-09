using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightIndicators : MonoBehaviour
{
	private Rigidbody rb;
	private Aircraft root;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		root = GetComponentInParent<Aircraft>();
	}


	private void FixedUpdate() => UpdateFlightData();


	private void UpdateFlightData()
	{
		Vector3 localVelocity = rb.transform.InverseTransformDirection(rb.velocity);

		Vector3 rightHorizontalVector = Attitude.CalculateRightHorizontalVector(rb.transform);
		float roll = Attitude.CalculateRoll(rb.transform, rightHorizontalVector);
		float pitch = Attitude.CalculatePitch(rb.transform);

		float verticalAOA = AnglesOfAttack.GetVerticalAOA(localVelocity);
		float horizontalAOA = AnglesOfAttack.GetHorizontalAOA(localVelocity);

		root.SetFlightData(
			localVelocity,
			rightHorizontalVector,
			roll,
			pitch,
			verticalAOA,
			horizontalAOA
		);
	}
}
