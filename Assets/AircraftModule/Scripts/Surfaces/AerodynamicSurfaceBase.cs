using UnityEngine;
using System;
using UnityEngine.UIElements;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
	[Serializable] private struct SurfaceZone
	{
		[Min(0)] public int startIndex;
		[Min(0)] public int endIndex;
	}

	[SerializeField][Range(0, 1)] private float AOALerpSpeed;


	[SerializeField] private SurfaceZone rotatingZone;



	private WingGenerator pointsGenerator;
	[SerializeField] private float[] CurrentMainAOAList = new float[0];
	public float liftError;

	protected Rigidbody Rb { get; private set; }
	protected Aircraft Root { get; private set; }
	public float RotationAngle { get; private set; }

	

	protected virtual void Start()
	{
		Rb = GetComponentInParent<Rigidbody>();
		Root = Rb.GetComponent<Aircraft>();
		pointsGenerator = GetComponent<WingGenerator>();
	}

	private void FixedUpdate()
	{
		UpdateLiftList();
		ApplyLift();
	}


	private void UpdateLiftList()
	{
		if(CurrentMainAOAList.Length != pointsGenerator.NumOfPoints)
			CurrentMainAOAList = new float[pointsGenerator.NumOfPoints];
	}

	public float currentMainAOA, currentRotatingAOA;
	private void ApplyLift()
	{
		Vector3 rotatedUp = Vector3.Slerp(
			transform.up,
			transform.forward * (RotationAngle > 0 ? -1 : 1),
			Mathf.Abs(RotationAngle) / 90
		);


		int pointIndex = 0;
		foreach (WingPoint point in pointsGenerator.GetPoints())
		{
			bool isRotatingPoint = (
				rotatingZone.startIndex <= pointIndex
				&&
				pointIndex <= rotatingZone.endIndex
			);

			Vector3 pointLocalVelocity = transform.InverseTransformDirection(
				Rb.GetPointVelocity(point.position)
			);
			pointLocalVelocity.x = 0;

			float targetMainAOA = AnglesOfAttack.GetVerticalAOA(pointLocalVelocity);

			currentMainAOA = Mathf.Lerp(
				CurrentMainAOAList[pointIndex],
				targetMainAOA,
				AOALerpSpeed
			);

			currentRotatingAOA = currentMainAOA - (isRotatingPoint ? RotationAngle : 0);
			if (currentRotatingAOA > 180) currentRotatingAOA -= 360;
			if (currentRotatingAOA < -180) currentRotatingAOA += 360;


			float pointLift = GetLift(
				pointLocalVelocity.magnitude,
				currentMainAOA,
				currentRotatingAOA
			) * point.forceMult;


			Rb.AddForceAtPosition(
				pointLift * (isRotatingPoint? rotatedUp: transform.up),
				point.position,
				ForceMode.Force
			);

			CurrentMainAOAList[pointIndex] = currentMainAOA;

			pointIndex++;
		}
	}

	public void SetRotationAngle(float angle)
	{
		RotationAngle = Mathf.Clamp(angle, -90, 90);
	}

	protected abstract float GetLift(float velocityMagnitude, float mainAOA, float rotatingAOA);
}
