using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSetup : MonoBehaviour
{
	Rigidbody gearRb;
	private void Start()
	{
		AircraftConfig config = GetComponentInParent<AircraftSetup>().config;

		gearRb = transform.Find("Gear").GetComponent<Rigidbody>();
		MassAndTensor gearData = config.massConfig.GearMassAndTensor;
		gearRb.mass = gearData.mass;
		gearRb.inertiaTensor = gearData.tensor;
		gearRb.centerOfMass = Vector3.zero;

		Rigidbody wheelRb = transform.Find("Wheel").GetComponent<Rigidbody>();
		MassAndTensor wheelData = config.massConfig.WheelMassAndTensor;
		wheelRb.mass = wheelData.mass;
		wheelRb.inertiaTensor = wheelData.tensor;
		wheelRb.centerOfMass = Vector3.zero;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawWireSphere(gearRb == null? transform.position: gearRb.position, 0.25f);
	}
}
