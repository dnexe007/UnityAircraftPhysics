using System;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private float thrustSensitivity = 3;


    public static Controls singleton;
    private float rudderInput;
    private Vector2 YokeInput;
    private bool mouseActive;

    public event Action<int> OnFlapsChange;
    public event Action<float> OnThrustChange;



    private void Awake()
    {
        singleton = this;
    }

    private void OnDestroy()
    {
        singleton = null;
    }

    private void SetInputs()
    {
        if (Input.GetMouseButtonDown(1))
            mouseActive = !mouseActive;

        if (!mouseActive)
        {
            YokeInput = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return;
        }

        Vector2 currentInput = Input.mousePosition;
        currentInput.y /= Screen.height;
        currentInput.x /= Screen.width;
        currentInput -= new Vector2(0.5f, 0.5f);
        currentInput *= 2;
        YokeInput = currentInput;

        rudderInput = (Input.GetKey(KeyCode.E) ? 1 : 0) - (Input.GetKey(KeyCode.Q) ? 1 : 0);
	}

    private void SetFlaps()
    {
        if (Input.GetKeyDown(KeyCode.R)) OnFlapsChange?.Invoke(1);
        if (Input.GetKeyDown(KeyCode.F)) OnFlapsChange?.Invoke(-1);
    }

    private void SetThrust()
    {
        if (Input.GetKey(KeyCode.LeftShift)) OnThrustChange?.Invoke(thrustSensitivity * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftControl)) OnThrustChange?.Invoke(-thrustSensitivity * Time.deltaTime);
    }


    private void Update()
    {
        SetInputs();
        SetFlaps();
        SetThrust();
    }

    public static float GetInputByName(string name)
    {
        switch (name)
        {
            case "Roll":
                return singleton.YokeInput.x;
            case "Pitch":
                return singleton.YokeInput.y;
            case "Yaw":
                return singleton.rudderInput;
            default:
                Debug.LogError("Controls: Unknown input name");
                return 0;
        }
    }
}
