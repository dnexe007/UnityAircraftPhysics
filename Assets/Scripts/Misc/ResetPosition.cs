using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetPosition : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
