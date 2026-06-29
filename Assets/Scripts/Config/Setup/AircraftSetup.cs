using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSetup : MonoBehaviour
{
    [SerializeField] private AircraftConfig config;


	private void Awake()
	{
		Rigidbody rootRigidbody = transform.Find("Body").GetComponent<Rigidbody>();

		ConfigurableJoint[] wheels = GetComponentsInChildren<ConfigurableJoint>();
	}
}
