using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
	private Vector3 startAngles;

	private void Start()
	{
		startAngles = transform.localEulerAngles;
	}

	public void SetAngle(float angle)
	{
		transform.localEulerAngles = startAngles + rotationVector * angle;
	}
}

