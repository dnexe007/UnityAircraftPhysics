using UnityEngine;

public class Engine : MonoBehaviour
{
    private Rigidbody rb;
    private Aircraft root;


    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        root = GetComponentInParent<Aircraft>();
    }


    private void FixedUpdate() => ApplyEngines();


	private void ApplyEngines()
	{
		rb.AddForceAtPosition(
			root.ThrustValue * root.Config.EngineConfig.thrust * transform.forward,
			transform.position,
			ForceMode.Force
		);
	}
}
