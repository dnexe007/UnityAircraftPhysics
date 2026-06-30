using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSetup : MonoBehaviour
{
	private void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		AircraftConfig config = GetComponentInParent<AircraftSetup>().config;
		rb.mass = config.WheelMass;
		rb.inertiaTensor = config.WheelTensor;
	}
}
