using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AircraftMassSetup : MonoBehaviour
{
	void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		Aircraft root = GetComponentInParent<Aircraft>();

		rb.mass = root.Config.MassConfig.BodyMass;
		rb.inertiaTensor = root.Config.MassConfig.BodyTensor;
	}
}
