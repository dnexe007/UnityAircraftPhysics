using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    [Range(-90, 90)] public float currentAngle;

	private Vector3 startAngles;

	private void Start()
	{
		startAngles = transform.localEulerAngles;
	}

	private void Update()
	{
		transform.localEulerAngles = startAngles + rotationVector * currentAngle;
	}
}
