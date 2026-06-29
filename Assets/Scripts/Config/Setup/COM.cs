using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COM : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        rb.centerOfMass = transform.localPosition;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
