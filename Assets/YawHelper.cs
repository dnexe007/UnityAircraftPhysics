using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YawHelper : MonoBehaviour
{
	[SerializeField] private DragAnchor forceDragAnchor = new(100, 150_000);
	[SerializeField] private AnimationCurve forceMultOverHorizontalAOA = new(
		new(3, 1),
		new(5, 0)
	);

	private Rigidbody rb;
	private Aircraft root;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		root = GetComponentInParent<Aircraft>();
	}

	private void FixedUpdate()
	{
		Vector3 forceVector = Vector3.Project(transform.up, root.RightHorizontalVector);

		float speed = new Vector2(root.LocalVelocity.z, root.LocalVelocity.y).magnitude;

		float angleMult = forceMultOverHorizontalAOA.Evaluate(Mathf.Abs(root.HorizontalAOA));

		rb.AddForce(forceVector * forceDragAnchor.GetQuadraticDrag(speed) * angleMult);
	}
}
