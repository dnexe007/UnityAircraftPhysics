using UnityEngine;
using System;

public class ControlSurface : MonoBehaviour
{
    public float FullPower = 5;
    public float FullPowerSpeed = 100;
    public string PositiveKey;
    public string NegativeKey;
    public float ValueChangeSpeed = 2;

    private Rigidbody rb;
    private float currentInputValue;

    [Range(-1, 1)] public float CenterValue;
    private KeyCode PositiveKeycode => (KeyCode)Enum.Parse(typeof(KeyCode), PositiveKey);
    private KeyCode NegativeKeycode => (KeyCode)Enum.Parse(typeof(KeyCode), NegativeKey);

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float forwardSpeed = transform.InverseTransformDirection(rb.velocity).z;

        Vector3 steerDirection = - transform.up * currentInputValue * Mathf.Sign(forwardSpeed);

        float multiplier = Mathf.InverseLerp(0, FullPowerSpeed, Mathf.Abs(forwardSpeed));
        multiplier = Mathf.Clamp01(multiplier);

        rb.AddForceAtPosition(steerDirection * multiplier * FullPower, transform.position, ForceMode.Acceleration);
    }

    private void Update()
    {
        float targetValue = (Input.GetKey(PositiveKeycode) ? 1 : 0) - (Input.GetKey(NegativeKeycode) ? 1 : 0) + CenterValue;
        targetValue = Mathf.Clamp(targetValue, -1, 1);
        currentInputValue = Mathf.MoveTowards(currentInputValue, targetValue, Time.deltaTime * ValueChangeSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 center = transform.position;
        Vector3 forward = transform.position + transform.forward;
        Vector3 steer = transform.position + ( - transform.forward + transform.up * 0.5f) * 0.5f;
        Vector3 right = transform.right * 2;
        Vector3 left = -transform.right * 2;

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(forward + right, forward + left);
        Gizmos.DrawLine(forward + right, center + right);
        Gizmos.DrawLine(forward + left, center + left);
        Gizmos.DrawLine(center + right, center + left);
        Gizmos.DrawLine(steer + right, center + right);
        Gizmos.DrawLine(steer + left, center + left);
        Gizmos.DrawLine(steer + right, steer + left);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, 0.25f);


        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, transform.position + steerDirection * 5);
        }
    }
}
