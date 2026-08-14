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

	[SerializeField][Range(2, 20)] private int numOfPoints = 4;

	public int NumOfPoints => numOfPoints;

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

	public float totalMult;

	public IEnumerable<WingPoint> GetPoints()
	{
		float widthSum = (baseWidth + edgeWidth) / 2 * NumOfPoints;
		float _totalMult = 0;


		for (int i = 0; i < NumOfPoints; i++)
		{
			float t = (float)i / (NumOfPoints - 1);

			Vector3 front = Vector3.Lerp(BaseFront, EdgeFront, t);
			Vector3 back = Vector3.Lerp(BaseBack, EdgeBack, t);

			Vector3 position = Vector3.Lerp(front, back, forcePointPosition);

			float localWidth = Vector3.Distance(front, back);

			float forceMult = localWidth / widthSum;
			_totalMult += forceMult;

			yield return new(position, forceMult);
		}
		totalMult = _totalMult;
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
	}
}
