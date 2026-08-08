using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LandingGearAnimator : MonoBehaviour
{
	[Serializable] private class AnimatedElement
	{
		[SerializeField] private Transform transform;
		[SerializeField] private Vector3 rotationAngles;
		[SerializeField] private AnimationCurve anglesMultOverAnimValue;

		public void SetAnimValue(float value)
		{
			transform.localEulerAngles = anglesMultOverAnimValue.Evaluate(value) * rotationAngles;
		}
	}


	[SerializeField] private AnimatedElement[] elements;

	[Range(0, 1)] public float deployment;

	private void Update()
	{
		foreach(AnimatedElement el in elements) el.SetAnimValue(deployment);
	}
}