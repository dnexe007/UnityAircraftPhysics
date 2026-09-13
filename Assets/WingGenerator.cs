using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WingGenerator : MonoBehaviour
{
	[SerializeField] [Min(0)] private float edgeZ = 1;
	[SerializeField] [Min(0)] private float edgeX = 1;

	[SerializeField] [Min(0.01f)] private float baseWidth = 1;
	[SerializeField] [Min(0.01f)] private float edgeWidth = 0.5f;

	[SerializeField] [Range(0, 1)] private float forcePointPosition = 0.5f;

	[SerializeField] private bool reverseDirection;

	[SerializeField] [Range(2, 20)] private int numOfPoints = 4;

	[SerializeField][Range(0, 1)] private float horizontalAOAOffsetValue;

	[SerializeField] private float calculatedHorizontalAOAOffset;


	public int NumOfPoints => numOfPoints;

	public float HorizontalAOAOffset => calculatedHorizontalAOAOffset;

	private Vector3 BaseFront => transform.position; 
	private Vector3 BaseBack => transform.TransformPoint(
		-Vector3.forward * baseWidth
	);
	private Vector3 EdgeFront => transform.TransformPoint(
		new(edgeX * (reverseDirection? -1 : 1), 0, -edgeZ)
	);
	private Vector3 EdgeBack => transform.TransformPoint(
		new(edgeX * (reverseDirection ? -1 : 1), 0, -edgeZ - edgeWidth)
	);

	private Vector3 StartForcePoint => Vector3.Lerp(BaseFront, BaseBack, forcePointPosition);
	private Vector3 EndForcePoint => Vector3.Lerp(EdgeFront, EdgeBack, forcePointPosition);

	private void OnValidate()
	{
		float calculatedSweepAngle = Vector3.Angle(
			transform.forward,
			EndForcePoint - StartForcePoint
		) - 90;

		calculatedHorizontalAOAOffset = Mathf.Lerp(
			0,
			calculatedSweepAngle,
			horizontalAOAOffsetValue
		) * (reverseDirection ? -1 : 1);
	}

	public IEnumerable<WingPoint> GetPoints()
	{
		Vector3 startPoint = StartForcePoint;
		Vector3 endPoint = EndForcePoint;

		float widthSum = (baseWidth + edgeWidth) / 2 * NumOfPoints;

		for (int i = 0; i < NumOfPoints; i++)
		{
			float t = (float)i / (NumOfPoints - 1);

			Vector3 position = Vector3.Lerp(startPoint, endPoint, t);

			float localWidth = Mathf.Lerp(baseWidth, edgeWidth, t);

			float forceMult = localWidth / widthSum;

			yield return new(position, forceMult);
		}
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawLine(EdgeFront, BaseFront);
		Gizmos.DrawLine(EdgeBack, BaseBack);
		Gizmos.DrawLine(EdgeFront, EdgeBack);
		Gizmos.DrawLine(BaseFront, BaseBack);

		foreach(WingPoint point in GetPoints())
		{	
			Gizmos.DrawWireSphere(
				point.position,
				0.125f
			);
		}


		Gizmos.DrawLine(
			EdgeFront,
			EdgeFront +
			Vector3.Slerp(
				transform.forward,
				transform.right * Mathf.Sign(HorizontalAOAOffset),
				Mathf.Abs(calculatedHorizontalAOAOffset) / 90
			)
		);
	}
}
