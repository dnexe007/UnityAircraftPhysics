using System.Collections;
using UnityEngine;

public class AircraftSetup : MonoBehaviour
{
    public AircraftConfig config;

	private void Start()
	{
		Rigidbody rootRb = transform.Find("Body").GetComponent<Rigidbody>();
		rootRb.mass = config.MassConfig.BodyMass;
		rootRb.inertiaTensor = config.MassConfig.BodyTensor;
	}
}
