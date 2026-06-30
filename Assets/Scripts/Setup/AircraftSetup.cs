using UnityEngine;

public class AircraftSetup : MonoBehaviour
{
    public AircraftConfig config;

	private void Awake()
	{
		Rigidbody rootRb = transform.Find("Body").GetComponent<Rigidbody>();

		print($"{config.BodyMass} {config.BodyTensor}");
		rootRb.mass = config.BodyMass;
		rootRb.inertiaTensor = config.BodyTensor;
	}
}
