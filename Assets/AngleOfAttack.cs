using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleOfAttack : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;

    public float verticalAngle { get; private set; }

    public float horizontalAngle { get; private set; }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.magnitude < 1)
        {
            verticalAngle = horizontalAngle = 0;
            return;
        }

        verticalAngle = Vector3.SignedAngle(
            Vector3.ProjectOnPlane(
                rb.velocity.normalized,
                transform.right
            ).normalized,
            transform.forward,
            transform.right
        );

        horizontalAngle = Vector3.SignedAngle(
            Vector3.ProjectOnPlane(
                rb.velocity.normalized,
                transform.up
            ).normalized,
            transform.forward,
            transform.up
        );
    }
}
