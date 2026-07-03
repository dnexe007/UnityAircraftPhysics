using System;
using UnityEngine;

public struct MassAndTensor
{
	public float mass;
	public Vector3 tensor;
}


[Serializable]
public class MassConfig
{
	[SerializeField] private int ParentToChildMassMult = 10;
	[SerializeField] private float totalMass = 20_000;
	[SerializeField] private Vector3 totalTensor = new(350_000, 450_000, 100_000);
	[SerializeField] private int numOfWheels = 0;

	public float WheelMassMult => 1 / (numOfWheels + numOfWheels * ParentToChildMassMult + Mathf.Pow(ParentToChildMassMult, 2));

	public float GearMassMult => WheelMassMult * ParentToChildMassMult;

	public float RootMassMult => WheelMassMult * Mathf.Pow(ParentToChildMassMult, 2);

	public MassAndTensor RootMassAndTensor => new()
	{
		mass = RootMassMult * totalMass,
		tensor = RootMassMult * totalTensor
	};

	public MassAndTensor GearMassAndTensor => new()
	{
		mass = GearMassMult * totalMass,
		tensor = GearMassMult * totalTensor
	};

	public MassAndTensor WheelMassAndTensor => new()
	{
		mass = WheelMassMult * totalMass,
		tensor = WheelMassMult * totalTensor
	};
}
