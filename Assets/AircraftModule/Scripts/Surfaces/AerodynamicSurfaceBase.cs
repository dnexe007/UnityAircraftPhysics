using System;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
	[Serializable] private struct RotatingPoint
	{
		[Min(0)] public int index;
		[Range(0, 1)] public float rotationInfluence;
	}
	[SerializeField][Range(0, 1)] private float AOALerpSpeed;
	[SerializeField] private RotatingPoint[] rotatingPoints;
	[SerializeField][Range(-90, 90)] private float horizontalAOAOffset;



	private WingGenerator pointsGenerator;
	private float[] mainVerticalAOAList = new float[0];

	public float RotationAngle { get; private set; }
	protected Rigidbody Rb { get; private set; }
	protected Aircraft Root { get; private set; }


	protected virtual void Start()
	{
		Rb = GetComponentInParent<Rigidbody>();
		Root = Rb.GetComponent<Aircraft>();
		pointsGenerator = GetComponent<WingGenerator>();
		mainVerticalAOAList = new float[pointsGenerator.NumOfPoints];
	}


	private void FixedUpdate() => ApplyLift();


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
			float rotationInfluence = GetRotationInfluence(pointIndex);

			SurfaceMovementData movementData = GetMovementData(
				point.position,
				pointIndex,
				rotationInfluence
			);

			float pointLift = GetLift(movementData) * point.forceMult;
			Vector3 pointUp = Vector3.Slerp(transform.up, rotatedUp, rotationInfluence);

			Rb.AddForceAtPosition(pointLift * pointUp, point.position);

			pointIndex++;
		}
	}


	private SurfaceMovementData GetMovementData(Vector3 pointPosition, int pointIndex, float rotationInfluence)
	{
		Vector3 pointLocalVelocity = transform.InverseTransformDirection(
				Rb.GetPointVelocity(pointPosition)
		);

		float mainVerticalAOA = GetMainVerticalAOA(pointLocalVelocity, pointIndex);
		float rotatingVerticalAOA = GetRotatingVerticalAOA(mainVerticalAOA, rotationInfluence);
		float horizontalAOA = GetHorizontalAOA(pointLocalVelocity);

		return new()
		{
			velocityMagnitude = pointLocalVelocity.magnitude,
			mainVerticalAOA = mainVerticalAOA,
			rotatingVerticalAOA = rotatingVerticalAOA,
			horizontalAOA = horizontalAOA
		};
	}


	private float GetRotationInfluence(int pointIndex)
	{
		return rotatingPoints.FirstOrDefault(p => p.index == pointIndex).rotationInfluence;
	}
	

	private float GetMainVerticalAOA(Vector3 pointLocalVelocity, int pointIndex)
	{
		float targetAOA = AnglesOfAttack.GetVerticalAOA(pointLocalVelocity);
		mainVerticalAOAList[pointIndex] = Mathf.Lerp(
			mainVerticalAOAList[pointIndex],
			targetAOA,
			AOALerpSpeed
		);
		return mainVerticalAOAList[pointIndex];
	}


	private float GetRotatingVerticalAOA(float mainVerticalAOA, float rotationInfluence)
	{
		float rotatingVerticalAOA = (
			mainVerticalAOA -
			RotationAngle * rotationInfluence
		);
			
		return FixAOA(rotatingVerticalAOA);
	}


	private float GetHorizontalAOA(Vector3 pointLocalVelocity)
	{
		float horizontalAOA = Mathf.Abs(
				AnglesOfAttack.GetHorizontalAOA(pointLocalVelocity) *
				(pointsGenerator.ReverseDirection ? -1 : 1) +
				horizontalAOAOffset
			);
		return FixAOA(horizontalAOA);
	}


	private static float FixAOA(float angle)
	{
		if (angle > 180) angle -= 360;
		if (angle < -180) angle += 360;
		return angle;
	}


	public void SetRotationAngle(float angle)
	{
		RotationAngle = Mathf.Clamp(angle, -90, 90);
	}


	protected abstract float GetLift(SurfaceMovementData movementData);
}
