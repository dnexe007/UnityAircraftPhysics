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
	[SerializeField] private float pitchTensor = 300_000;
	[SerializeField] private float yawTensor = 350_000;
	[SerializeField] private float rollTensor = 40_000;


	[Header("Joint settings")]
	[SerializeField] private int parentToChildMassMult = 10;
	[SerializeField] private int numOfWheels = 0;


	[field: SerializeField, HideInInspector] public float WheelMass { get; private set; }
	[field: SerializeField, HideInInspector] public float GearMass { get; private set; }
	[field: SerializeField, HideInInspector] public float BodyMass { get; private set; }


	[field: SerializeField, HideInInspector] public Vector3 WheelTensor { get; private set; }
	[field: SerializeField, HideInInspector] public Vector3 GearTensor { get; private set; }
	[field: SerializeField, HideInInspector] public Vector3 BodyTensor { get; private set; }


	public void UpdateData()
	{
		float wheelMassCoef = 1f / (numOfWheels + numOfWheels * parentToChildMassMult + Mathf.Pow(parentToChildMassMult, 2));
		float gearMassCoef = wheelMassCoef * parentToChildMassMult;
		float bodyMassCoef = gearMassCoef * parentToChildMassMult;

		WheelMass = totalMass * wheelMassCoef;
		BodyMass = totalMass * bodyMassCoef;
		GearMass = totalMass * gearMassCoef;

		Vector3 totalTensor = new Vector3(pitchTensor, yawTensor, rollTensor);

		WheelTensor = totalTensor * wheelMassCoef;
		GearTensor = totalTensor * gearMassCoef;
		BodyTensor = totalTensor * bodyMassCoef;
	}
}
