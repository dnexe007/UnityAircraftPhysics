using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AircraftMassSetup : MonoBehaviour
{
	void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		Aircraft root = GetComponent<Aircraft>();

		rb.mass = root.Config.MassConfig.Mass;
		rb.inertiaTensor = root.Config.MassConfig.Tensor;
	}
}
