using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapsAnimator : MonoBehaviour
{
	[SerializeField] private Vector3 rotationVector;
	[SerializeField] private Vector3 flap2MovementVector;
	[Range(0, 1)] public float deployment;

	private Vector3 startAngles;

	private Transform flap2;
	private Vector3 flap2StartPos;

	private void Start()
	{
		startAngles = transform.localEulerAngles;
		flap2 = transform.Find("Flap2");
		flap2StartPos = flap2.localPosition;
	}

	private void Update()
	{
		transform.localEulerAngles = startAngles + rotationVector * deployment;
		flap2.localPosition = flap2StartPos + flap2MovementVector * deployment;
	}
}
