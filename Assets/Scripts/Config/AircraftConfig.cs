using System;
using UnityEngine;



[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
	[SerializeField] private float totalMass = 10_000;
	[SerializeField] private Vector3 totalTensor = new(70_000, 80_000, 15_000);

	[SerializeField] private int numOfWheels = 3;


	private float CalculateWheelMassMult()
	{
		float numOfParts = 10 + numOfWheels;
		
		return 1 / numOfParts;
	}

	public float WheelMass => totalMass * CalculateWheelMassMult();
	public Vector3 WheelTensor => totalTensor * CalculateWheelMassMult();

	public float BodyMass => WheelMass * 10;
	public Vector3 BodyTensor => WheelTensor * 10;


	public float enginesThrust = 12;
	[Range(1, 10)] public int flapsSteps = 5;
	public WingConfig wingParams;
	public ControlSurfaceConfig pitchParams;
	public ControlSurfaceConfig aileronParams;
	public ControlSurfaceConfig rudderParams;
	public MovementDragConfig fuselageDragParams;
	public AngularDragConfig fuselageAngularDragParams;
	
}


