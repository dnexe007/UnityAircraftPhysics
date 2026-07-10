using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadCalculator
{
    private readonly Rigidbody rb;
	private Vector3 lastVelocity;
	private float currentOverload;
	public const float lerpSpeed = 5;


	public OverloadCalculator(Rigidbody rb)
	{
		this.rb = rb;
		lastVelocity = rb.velocity;
	}


	public float CalculateOverload()
	{
		Vector3 acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
		acceleration -= Physics.gravity;
		lastVelocity = rb.velocity;

		float targetOverload = rb.transform.InverseTransformDirection(acceleration).y / Physics.gravity.magnitude;

		currentOverload = Mathf.Lerp(currentOverload, targetOverload, Time.fixedDeltaTime *  lerpSpeed);

		return currentOverload;
	}
}
