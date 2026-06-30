using UnityEngine;

public class AircraftSetup : MonoBehaviour
{
    public AircraftConfig config;

	private void Start()
	{
		Rigidbody rootRb = transform.Find("Body").GetComponent<Rigidbody>();
		MassAndTensor rootData = config.RootMassAndTensor;
		rootRb.mass = rootData.mass;
		rootRb.inertiaTensor = rootData.tensor;
	}
}
