using System.Collections.Generic;
using UnityEngine;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
	//[SerializeField] private PointsGenerator pointsGenerator;

	protected Rigidbody rb;
	protected Aircraft root;

	public float CurrentRotationAngle { get; protected set; }

	private Vector3 startAngles;
	[SerializeField] private Vector3 rotationVector;


	protected virtual void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
		root = rb.GetComponent<Aircraft>();

		startAngles = transform.localEulerAngles;
	}

	protected abstract float GetLift(float velocityMagnitude, float verticalAOA);

	//private void ApplyLift()
	//{
	//	IEnumerable<WingPoint> points = pointsGenerator.GetPoints(
	//		transform,
	//		CurrentRotationAngle,
	//		rb
	//	);

	//	multSum = 0;

	//	foreach (WingPoint point in points)
	//	{
	//		float liftAtPoint = GetLift(
	//			point.VelocityMagnitude,
	//			point.VerticalAOA
	//		);

	//		rb.AddForceAtPosition(
	//			liftAtPoint * point.TotalForceMult * point.Normal,
	//			point.Position,
	//			ForceMode.Force
	//		);

	//		multSum += point.TotalForceMult;

	//	}
	//}

	private void ApplyLift()
	{
		//Vector3 forward = Vector3.Slerp(
		//	transform.forward,
		//	transform.up * (CurrentRotationAngle > 0 ? -1 : 1),
		//	Mathf.Abs(CurrentRotationAngle) / 90
		//);

		//Vector3 up = Vector3.Cross(forward, transform.right);

		Vector3 localVelocity = rb.GetLocalVelocity(transform, transform.position);

		//Vector3 localVelocity = new(
		//	0,
		//	Vector3.Dot(pointVelocity, up),
		//	Vector3.Dot(pointVelocity, forward)
		//);

		float verticalAOA = AnglesOfAttack.GetVerticalAOA(localVelocity);


		float lift = GetLift(localVelocity.magnitude, verticalAOA);

		rb.AddForceAtPosition(lift * transform.up, transform.position, ForceMode.Force);
	}


	private void FixedUpdate()
	{
		transform.localEulerAngles = startAngles + rotationVector * CurrentRotationAngle;
		ApplyLift();
	}

}
