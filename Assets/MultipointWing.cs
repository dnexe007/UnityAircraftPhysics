using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class MultipointWing
{
	[SerializeField] private float endX;
	[SerializeField] private float endZ;
	[SerializeField] [Range(2, 20)] private int numOfPoints = 2;
	[SerializeField] [Range(1, 20)] private float edgePointsDeltaCoef = 3;
	[SerializeField] [Range(0.05f, 10)] private float gizmoLinesScale;

	public IEnumerable<WingPoint> GetPoints(Transform transform, float rotationAngle, Rigidbody rb = null)
	{
		Vector3 startPos = transform.position;
		Vector3 endPos = transform.TransformPoint(new(endX, 0, endZ));

		float endPointForceMult = 1 / (1 + edgePointsDeltaCoef);
		float startPointForceMult = endPointForceMult * edgePointsDeltaCoef;

		Vector3 wingForward = Vector3.Slerp(
			transform.forward,
			transform.up * (rotationAngle > 0 ? -1 : 1),
			Mathf.Abs(rotationAngle) / 90
		);
		Vector3 wingNormal = Vector3.Cross(wingForward, transform.right);


		for (float i = 0; i < numOfPoints; i++)
		{
			float pointPositionValue = i / (numOfPoints - 1);

			Vector3 pointPosition = Vector3.Lerp(
				startPos,
				endPos,
				pointPositionValue
			);

			float positionForceMult = Mathf.Lerp(
				startPointForceMult,
				endPointForceMult,
				pointPositionValue
			);

			yield return new(
				pointPosition,
				wingForward,
				wingNormal,
				positionForceMult,
				numOfPoints,
				rb
			);
		}
	}

	public void DrawGizmos(Transform transform, float rotationAngle)
	{
		Gizmos.color = Color.blue;

		foreach(WingPoint p in GetPoints(transform, rotationAngle))
		{

			Gizmos.DrawLine(p.Position, p.Position + gizmoLinesScale * 0.25f * p.TotalForceMult * p.Normal);
			Gizmos.DrawLine(p.Position, p.Position + gizmoLinesScale * p.TotalForceMult * p.Forward);
			Gizmos.DrawLine(p.Position, p.Position + gizmoLinesScale * 0.25f * p.TotalForceMult * p.Right);
			Gizmos.DrawWireSphere(p.Position, 0.125f);
		}
	}
}
