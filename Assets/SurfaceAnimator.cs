using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
	[SerializeField] private SurfaceController surfaceController;

	private Vector3 startAngles;

	private void Start()
	{
		startAngles = transform.localEulerAngles;
	}

	public void Update()
	{
		transform.localEulerAngles = startAngles + rotationVector * surfaceController.CurrentAngle;
	}
}

