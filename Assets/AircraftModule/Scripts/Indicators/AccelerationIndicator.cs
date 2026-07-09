using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationIndicator : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastVelocity;

	[SerializeField] private Vector3 localAcceleration;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}


	private void FixedUpdate()
	{
		Vector3 acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
		acceleration -= Physics.gravity;

		localAcceleration = transform.InverseTransformDirection(acceleration) / 9.81f;

		lastVelocity = rb.velocity;
	}
}
