using UnityEngine;
using System;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
	[Serializable] private struct SurfaceZone
	{
		[Min(0)] public int startIndex;
		[Min(0)] public int endIndex;
	}

	[SerializeField][Range(0, 1)] private float AOALerpSpeed;


	[SerializeField] private SurfaceZone rotatingZone;



	private VisualWing pointsGenerator;
	private float[] CurrentAOAList = new float[0];
	public float liftError;

	protected Rigidbody Rb { get; private set; }
	protected Aircraft Root { get; private set; }
	public float RotationAngle { get; private set; }

	

	protected virtual void Start()
	{
		Rb = GetComponentInParent<Rigidbody>();
		Root = Rb.GetComponent<Aircraft>();
		pointsGenerator = GetComponent<VisualWing>();
	}

	private void FixedUpdate()
	{
		UpdateLiftList();
		ApplyLift();
	}


	private void UpdateLiftList()
	{
		if(CurrentAOAList.Length != pointsGenerator.NumOfPoints)
			CurrentAOAList = new float[pointsGenerator.NumOfPoints];
	}
	private void ApplyLift()
	{
		Vector3 rotatedForward = Vector3.Slerp(transform.forward,
			transform.up * (RotationAngle > 0 ? -1 : 1),
			Mathf.Abs(RotationAngle) / 90
		);

		Vector3 rotatedUp = Vector3.Cross(rotatedForward, transform.right);

		int pointIndex = 0;
		foreach (WingPoint point in pointsGenerator.GetPoints())
		{
			bool isRotatingPoint = (
				rotatingZone.startIndex <= pointIndex
				&&
				pointIndex <= rotatingZone.endIndex
			);

			Vector3 pointVelocity = Rb.GetPointVelocity(point.position);

			Vector3 up = isRotatingPoint ? rotatedUp : transform.up;
			Vector3 forward = isRotatingPoint ? rotatedForward : transform.forward;

			Vector3 localVelocity = new(
				0,
				Vector3.Dot(pointVelocity, up),
				Vector3.Dot(pointVelocity, forward)
			);


			float targetAOA = AnglesOfAttack.GetVerticalAOA(localVelocity);

			float currentAOA = Mathf.Lerp(
				CurrentAOAList[pointIndex],
				targetAOA,
				AOALerpSpeed
			);

			float pointLift = GetLift(
				localVelocity.magnitude,
				currentAOA
			) * point.forceMult;


			Rb.AddForceAtPosition(
				pointLift * up,
				point.position,
				ForceMode.Force
			);

			CurrentAOAList[pointIndex] = currentAOA;

			pointIndex++;
		}
	}

	public void SetRotationAngle(float angle)
	{
		RotationAngle = Mathf.Clamp(angle, -90, 90);
	}

	protected abstract float GetLift(float velocityMagnitude, float verticalAOA);
}
