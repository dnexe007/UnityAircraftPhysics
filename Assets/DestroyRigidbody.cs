using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyRigidbody : MonoBehaviour
{
	private List<Transform> children = new();
	private Rigidbody rb;
	private Vector3 lastVelocity;


	private void Start()
	{
		foreach(Transform t in GetComponentInChildren<Transform>())
		{
			if(t != transform) children.Add(t);
		}
		rb = GetComponent<Rigidbody>();
	}


	private void FixedUpdate()
	{
		lastVelocity = rb.velocity;
	}

	private void OnCollisionEnter(Collision collision)
	{
		foreach(Transform t in children)
		{
			Rigidbody childRb = t.AddComponent<Rigidbody>();
			t.parent = null;
			childRb.velocity = lastVelocity;
		}
		Destroy(gameObject);
	}
}
