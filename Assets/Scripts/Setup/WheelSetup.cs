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
		gearRb.mass = config.massConfig.GearMass;
		gearRb.inertiaTensor = config.massConfig.GearTensor;
		gearRb.centerOfMass = Vector3.zero;

		Rigidbody wheelRb = transform.Find("Wheel").GetComponent<Rigidbody>();
		wheelRb.mass = config.massConfig.WheelMass;
		wheelRb.inertiaTensor = config.massConfig.WheelTensor;
		wheelRb.centerOfMass = Vector3.zero;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawWireSphere(gearRb == null? transform.position: gearRb.position, 0.25f);
	}
}
