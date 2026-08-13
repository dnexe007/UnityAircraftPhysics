using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControls : MonoBehaviour
{
    [SerializeField] private float rollForce;
    [SerializeField] private float pitchForce;

    private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		rb.AddTorque(Input.GetAxis("Horizontal") * rollForce * transform.forward, ForceMode.Force);
		rb.AddTorque(Input.GetAxis("Vertical") * pitchForce * transform.right, ForceMode.Force);
	}
}
