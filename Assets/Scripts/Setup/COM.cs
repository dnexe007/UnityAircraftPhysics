using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COM : MonoBehaviour
{
    [SerializeField] private Vector3 COMLocalPosition;
    private Rigidbody rb;

	private void Start() => UpdateCOM();
    private void OnValidate() => UpdateCOM();

	private void UpdateCOM()
	{
		if (rb == null) rb = GetComponent<Rigidbody>();
		if (rb != null) rb.centerOfMass = COMLocalPosition;
	}

	private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(COMLocalPosition), 0.5f);
    }
}
