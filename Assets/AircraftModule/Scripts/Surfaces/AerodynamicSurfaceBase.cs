using System.Collections.Generic;
using UnityEngine;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
	[SerializeField] private PointsGenerator pointsGenerator;

	protected Rigidbody rb;
	protected Aircraft root;

	public float CurrentRotationAngle { get; protected set; }


	protected virtual void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
		root = rb.GetComponent<Aircraft>();
	}

	protected abstract float GetLift(float velocityMagnitude, float verticalAOA);

	private void ApplyLift()
	{
		IEnumerable<WingPoint> points = pointsGenerator.GetPoints(
			transform,
			CurrentRotationAngle,
			rb
		);

		multSum = 0;

		foreach (WingPoint point in points)
		{
			float liftAtPoint = GetLift(
				point.VelocityMagnitude,
				point.VerticalAOA
			);

			rb.AddForceAtPosition(
				liftAtPoint * point.TotalForceMult * point.Normal,
				point.Position,
				ForceMode.Force
			);

			multSum += point.TotalForceMult;

		}
	}

	public float multSum;

	private void OnDrawGizmos()
	{
		pointsGenerator.DrawGizmos(transform, CurrentRotationAngle);
	}

	private void FixedUpdate()
	{
		ApplyLift();
	}


	[ContextMenu("Test force mult")]
	public void TestForceMult() => pointsGenerator.TestForceMult(transform);
}
