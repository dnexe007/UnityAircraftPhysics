using System;
using UnityEngine;



[Serializable]
public class MassConfig
{
	[Header("Mass settings")]
	[SerializeField] private float mass = 20_000;


	[Header("Tensor settings")]
	[SerializeField] private float pitchTensor = 300_000;
	[SerializeField] private float yawTensor = 350_000;
	[SerializeField] private float rollTensor = 40_000;

	public float Mass => mass;
	

	public Vector3 Tensor => new(pitchTensor, yawTensor, rollTensor);


}
