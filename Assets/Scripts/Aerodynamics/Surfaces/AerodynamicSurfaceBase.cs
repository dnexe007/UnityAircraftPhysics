using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AerodynamicSurfaceBase : MonoBehaviour
{
    [SerializeField] private Transform sensorPoint;

    protected Rigidbody rb;
    protected AircraftConfig config;

    public struct SpeedAndAOA
    {
        public float speed;
        public float aoa;

        public SpeedAndAOA(float speed, float aoa)
        {
            this.speed = speed;
            this.aoa = aoa;
        }
    }

    public SpeedAndAOA GetSpeedAndAOA()
    {
        if (sensorPoint == null) sensorPoint = transform;
        Vector3 localVelocity = transform.InverseTransformDirection(
            rb.GetPointVelocity(sensorPoint.position)
        );

        float speed = new Vector2(localVelocity.z, localVelocity.y).magnitude;
        float aoa = -Mathf.Atan2(localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;

        return new(speed, aoa);
    }

    protected abstract void ApplyForce();


    protected virtual void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        config = rb.GetComponentInParent<AircraftSetup>().config;
    }

    private void FixedUpdate()
    {
        ApplyForce();
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;

		Vector3 lf = transform.position + transform.forward / 2 - transform.right / 2;
		Vector3 lb = transform.position - transform.forward / 2 - transform.right / 2;
		Vector3 rf = transform.position + transform.forward / 2 + transform.right / 2;
		Vector3 rb = transform.position - transform.forward / 2 + transform.right / 2;

		Gizmos.DrawLine(lf, lb);
		Gizmos.DrawLine(lb, rb);
		Gizmos.DrawLine(rb, rf);
		Gizmos.DrawLine(rf, lf);

		Gizmos.DrawWireSphere(transform.position, 0.125f);
	}
}
