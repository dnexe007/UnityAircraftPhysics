using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpeed : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
		if (Input.GetKey(KeyCode.T))
		{
			rb.velocity = transform.forward * speed;
		}
    }

}
