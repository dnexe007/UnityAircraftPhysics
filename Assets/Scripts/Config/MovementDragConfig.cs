using System;
using UnityEngine;

[Serializable]
public class MovementDragConfig
{
	public struct DragData
	{
		public Vector3 force;
		public Vector3 position;
	}

	[SerializeField]
	private AnimationCurve resistanceMultOverFlowAngle = new(
		new(0f, 1f, 0.1f, 0.1f),
		new(90f, 10f, 0f, 0f),
		new(180f, 1f, -0.1f, -0.1f)
	);

	[SerializeField] private QuadDragAnchor resistanceAnchor = new(400, 120_000);
	[SerializeField] private float rotatingFactor = -0.5f;

	public DragData GetFuselageDrag(Vector3 velocity, Vector3 forward, Vector3 worldCOM)
	{
		float basicDrag = resistanceAnchor.GetDrag(velocity.magnitude);
		float dragMult = resistanceMultOverFlowAngle.Evaluate(Vector3.Angle(velocity, forward));
		Vector3 force = -velocity.normalized * basicDrag * dragMult;

		Vector3 position = worldCOM + forward * rotatingFactor;

		return new() { force = force, position = position };
	}
}