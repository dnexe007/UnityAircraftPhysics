using System.Collections;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class AircraftSetup : MonoBehaviour
{
    public AircraftConfig config;


	[SerializeField] private float totalMass;

	[SerializeField] private Vector3 totalTensor;


	private void Start()
	{
		Rigidbody rootRb = transform.Find("Body").GetComponent<Rigidbody>();
		rootRb.mass = config.massConfig.BodyMass;
		rootRb.inertiaTensor = config.massConfig.BodyTensor;

		StartCoroutine(CalculateMass());
	}


	private IEnumerator CalculateMass()
	{
		yield return new WaitForSeconds(1);
		Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in rigidbodies)
		{
			totalMass += rb.mass;
			totalTensor += rb.inertiaTensor;
		}
	}
}
