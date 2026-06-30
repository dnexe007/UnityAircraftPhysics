using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSetup : MonoBehaviour
{
    public AircraftConfig config;


	private void Awake()
	{
		Rigidbody rootRb = transform.Find("Body").GetComponent<Rigidbody>();

		rootRb.mass = config.mass;
		rootRb.inertiaTensor = config.tensor;
	}
}
