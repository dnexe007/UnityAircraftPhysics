using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float spring;
    [SerializeField] private float dampCoef;
	[SerializeField] private float rayDistance;
	[SerializeField] private float springCoef;
	[SerializeField] private LayerMask excludeAircraftLayer;
	[SerializeField] private Transform model;
	[SerializeField] private float modelVerticalOffset = 0.25f;
	[SerializeField] private float sphereRadius = 0.149f;

    private Rigidbody rb;
	private float lastDistance;


	public float compressionSpeed;
	public float localVelocity;
	public float localVelocity2;

	public AnimationCurve forceMultOverCompression = new(
		new(1, 20),
		new(0.8f, 1)
	);

	private void Start()
	{
		rb = GetComponentInParent<Rigidbody>();
	}
	[SerializeField] private float compressionValue;
	[SerializeField] private float appliedForce;

	private void FixedUpdate()
	{
		appliedForce = 0;
		if(Physics.SphereCast(transform.position, sphereRadius, -transform.up, out RaycastHit hit, rayDistance, excludeAircraftLayer, QueryTriggerInteraction.Collide))
		{
			compressionValue = 1 - hit.distance / rayDistance;

			compressionSpeed = (hit.distance - lastDistance) / Time.fixedDeltaTime;

			localVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point)).y;
			localVelocity2 = transform.InverseTransformDirection(rb.GetPointVelocity(transform.position)).y;

			float dampForce = compressionSpeed * spring * dampCoef * springCoef ;

			float springForce = spring * compressionValue * springCoef * forceMultOverCompression.Evaluate(compressionValue);

			appliedForce = springForce - dampForce;

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
		

		if(model != null )
		{
			model.transform.position = transform.position - transform.up * lastDistance + transform.up * modelVerticalOffset;
		}
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
