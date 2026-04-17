using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
    protected Rigidbody rb;
    protected AircraftConfig config;

    protected struct SpeedAndAOA
    {
        public float speed;
        public float aoa;

        public SpeedAndAOA(float speed, float aoa)
        {
            this.speed = speed;
            this.aoa = aoa;
        }
    }

    protected SpeedAndAOA GetSpeedAndAOA()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(
            rb.GetPointVelocity(transform.position)
        );

        float speed = new Vector2(localVelocity.z, localVelocity.y).magnitude;
        float aoa = -Mathf.Atan2(localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;

        return new(speed, aoa);
    }

    protected abstract void ApplyForce();


    protected virtual void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        config = rb.GetComponent<FlightData>().config;
    }

    private void FixedUpdate()
    {
        ApplyForce();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 lf = transform.position + transform.forward - transform.right * 2;
        Vector3 lb = transform.position - transform.forward - transform.right * 2;
        Vector3 rf = transform.position + transform.forward + transform.right * 2;
        Vector3 rb = transform.position - transform.forward + transform.right * 2;

        Gizmos.DrawLine(lf, lb);
        Gizmos.DrawLine(lb, rb);
        Gizmos.DrawLine(rb, rf);
        Gizmos.DrawLine(rf, lf);
    }
}
