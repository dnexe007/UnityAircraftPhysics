using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictPosition : MonoBehaviour
{
    [SerializeField] private Vector3 predictedPosition;
    [SerializeField] private Vector3 realPosition;
    [SerializeField] private float positionDelta;
	[SerializeField] private float angle;

    private Rigidbody rb;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
	}

	private Vector3 lastVel;

	private void FixedUpdate()
	{
		realPosition = transform.position;

		positionDelta = (realPosition - predictedPosition).magnitude;


		Vector3 currentVel = rb.GetPointVelocity(transform.position);
		Vector3 acceleration = currentVel - lastVel;

		predictedPosition = transform.position + (currentVel + acceleration) * Time.fixedDeltaTime;


		angle = Vector3.Angle(transform.forward, predictedPosition - transform.position);
		lastVel = currentVel;
	}
}
