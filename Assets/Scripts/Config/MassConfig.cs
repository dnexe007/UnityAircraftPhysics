using System;
using UnityEngine;

//1 = wheelMass * numOfWheels + wheelMass * numOfWheels * parentToChildCoef + wheelMass * parentToChildCoef^2
//wheelMassCoef = 1 / (numOfWheels + numOfWheels * parentToChildCoef + parentToChileCoef^2)
//gearMassCoef = wheelMassCoef * parentToChildCoef
//bodyMassCoef = gearMassCoef * parentToChildCoef

[Serializable]
public class MassConfig
{
	[Header("Mass settings")]
	[SerializeField] private float totalMass = 20_000;
	

	[Header("Tensor settings")]
	[SerializeField] private float pitchTensor = 250_000;
	[SerializeField] private float yawTensorCoef = 1.2f;
	[SerializeField] private float rollTensorCoef = 0.2f;


	[Header("Joint settings")]
	[SerializeField] private int parentToChildMassMult = 10;
	[SerializeField] private int numOfWheels = 0;


	private Vector3 TotalTensor =>  new Vector3(1, yawTensorCoef, rollTensorCoef) * pitchTensor;


	private float WheelMassCoef => 1 / (numOfWheels + numOfWheels * parentToChildMassMult + Mathf.Pow(parentToChildMassMult, 2));
	private float GearMassCoef => WheelMassCoef * parentToChildMassMult;
	private float BodyMassCoef => GearMassCoef * parentToChildMassMult;


	public float WheelMass => totalMass * WheelMassCoef;
	public float GearMass => totalMass * GearMassCoef;
	public float BodyMass => totalMass * BodyMassCoef;


	public Vector3 WheelTensor => TotalTensor * WheelMassCoef;
	public Vector3 GearTensor => TotalTensor * GearMassCoef;
	public Vector3 BodyTensor => TotalTensor * BodyMassCoef;
}
