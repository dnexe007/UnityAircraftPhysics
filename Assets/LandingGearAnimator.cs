using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGearAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 xRotationVector = new(-90, -14, 0);
    [SerializeField] private Vector3 yRotationVector = new(0, -90, 0);
	[SerializeField] private Vector3 boxRotationVector = new(0, 0, -90);

	[SerializeField] private AnimationCurve boxOverDeployment = new(
		new(0, 0),
		new(0.5f, 1)
	);
	[SerializeField] private AnimationCurve gearXOverDeployment = new(
		new(0.5f, 1),
		new(1, 0)
	);
	[SerializeField] private AnimationCurve gearYOverDeployment = new(
		new(0.75f, 1),
		new(1, 0)
	);

	[Range(0, 1)] public float deployment;

	private Transform gearX;
	private Transform gearY;
	private Transform box;
	
	private void Start()
	{
		gearX = transform.Find("GearX");
		gearY = transform.Find("GearX/GearY");
		box = transform.Find("Box");
	}

	private void Update()
	{
		gearX.localEulerAngles = Vector3.Lerp(
			Vector3.zero,
			xRotationVector,
			gearXOverDeployment.Evaluate(deployment)
		);

		gearY.localEulerAngles = Vector3.Lerp(
			Vector3.zero,
			yRotationVector,
			gearYOverDeployment.Evaluate(deployment)
		);

		box.localEulerAngles = Vector3.Lerp(
			Vector3.zero,
			boxRotationVector,
			boxOverDeployment.Evaluate(deployment)
		);
	}
}