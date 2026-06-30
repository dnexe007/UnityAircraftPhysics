using System;
using UnityEngine;

public struct MassAndTensor
{
	public float mass;
	public Vector3 tensor;
}


[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
	[SerializeField] private int ParentToChildMassMult = 20;
	[SerializeField] private float totalMass = 10_000;
	[SerializeField] private Vector3 totalTensor = new(70_000, 80_000, 15_000);
	[SerializeField] private int numOfWheels = 3;

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


	public float enginesThrust = 12;
	[Range(1, 10)] public int flapsSteps = 5;
	public WingConfig wingParams;
	public ControlSurfaceConfig pitchParams;
	public ControlSurfaceConfig aileronParams;
	public ControlSurfaceConfig rudderParams;
	public MovementDragConfig fuselageDragParams;
	public AngularDragConfig fuselageAngularDragParams;
	
}


