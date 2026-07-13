using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float spring;
    [SerializeField] private float dampCoef;
	[SerializeField] private float rayDistance;
	[SerializeField] private float springCoef;
	[SerializeField] private LayerMask excludeAircraftLayer;


    private Rigidbody rb;
	private float lastDistance;


	public float compressionSpeed;
	public float localVelocity;
	public float localVelocity2;

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
	}


	private void FixedUpdate()
	{
		if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, rayDistance, excludeAircraftLayer))
		{
			float compressionValue = 1 - hit.distance / rayDistance;

			compressionSpeed = (hit.distance - lastDistance) / Time.fixedDeltaTime;

			localVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point)).y;
			localVelocity2 = transform.InverseTransformDirection(rb.GetPointVelocity(transform.position)).y;

			float dampForce = compressionSpeed * spring * dampCoef * springCoef;

			float springForce = spring * compressionValue * springCoef;

			rb.AddForceAtPosition(
				transform.up * (springForce - dampForce),
				hit.point,
				ForceMode.Force
			);

			lastDistance = hit.distance;
		}
		else lastDistance = rayDistance;
	}

	private void Update()
	{
		Debug.DrawRay(transform.position, -transform.up * rayDistance, Color.green);
		
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying && lastDistance < rayDistance)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position - transform.up * lastDistance + transform.up * 0.15f, 0.15f);
		}
	}
}
