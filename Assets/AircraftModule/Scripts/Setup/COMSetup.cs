using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMGizmo : MonoBehaviour
{
    private Rigidbody rb;

	private void OnDrawGizmos()
    {
        if(rb == null) rb = GetComponent<Rigidbody>();
        if (rb == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.TransformPoint(rb.centerOfMass), 0.5f);
    }
}
