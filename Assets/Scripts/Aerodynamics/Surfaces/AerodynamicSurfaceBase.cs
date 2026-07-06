using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
	[SerializeField] [Range(0.1f, 1)] private float AOALerpSpeed;
	[SerializeField] private float AOADelta;

    private Rigidbody rb;
    protected AircraftConfig config;

    protected float VelocityMagnitude { get; private set; }
    protected float VerticalAOA { get; private set; }
	protected abstract float CalculateLift();

    private void UpdateData()
    {
		Vector3 localVelocity = transform.InverseTransformDirection(
			rb.GetPointVelocity(transform.position)
		);

        VelocityMagnitude = new Vector2(
			localVelocity.z,
			localVelocity.y
		).magnitude;
		
		float targetVerticalAOA = AnglesOfAttack.GetVerticalAOA(localVelocity);

		VerticalAOA = Mathf.Lerp(
			VerticalAOA,
			targetVerticalAOA,
			AOALerpSpeed
		);

		AOADelta = MathF.Round(VerticalAOA - targetVerticalAOA, 3);
	}

	protected virtual void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        config = rb.GetComponentInParent<AircraftSetup>().config;
    }

    private void FixedUpdate()
    {
        UpdateData();

		rb.AddForceAtPosition(
			transform.up * CalculateLift(),
			transform.position,
			ForceMode.Force
		);
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;

		Vector3 lf = transform.position + transform.forward / 2 - transform.right / 2;
		Vector3 lb = transform.position - transform.forward / 2 - transform.right / 2;
		Vector3 rf = transform.position + transform.forward / 2 + transform.right / 2;
		Vector3 rb = transform.position - transform.forward / 2 + transform.right / 2;

		Gizmos.DrawLine(lf, lb);
		Gizmos.DrawLine(lb, rb);
		Gizmos.DrawLine(rb, rf);
		Gizmos.DrawLine(rf, lf);

		Gizmos.DrawWireSphere(transform.position, 0.125f);
	}
}
