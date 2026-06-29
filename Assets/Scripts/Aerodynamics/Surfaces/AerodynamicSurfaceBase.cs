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
        config = rb.GetComponent<FlightData>().config;
    }

    private void FixedUpdate()
    {
        ApplyForce();
    }
}
