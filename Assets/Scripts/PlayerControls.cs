using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float thrustSensitivity = 3;
	[field: SerializeField] public Aircraft Aircraft { get; private set; }


	private bool isMouseActive;


	private void SetInputs()
    {
		Aircraft.SetYawInput((Input.GetKey(KeyCode.E) ? 1 : 0) - (Input.GetKey(KeyCode.Q) ? 1 : 0));

		if (Input.GetMouseButtonDown(1)) isMouseActive = !isMouseActive;

        if (!isMouseActive)
        {
            Aircraft.SetRollInput(Input.GetAxis("Horizontal"));
            Aircraft.SetPitchInput(Input.GetAxis("Vertical"));
            return;
        }

        Vector2 mouseInput = Input.mousePosition;
        mouseInput.y /= Screen.height;
        mouseInput.x /= Screen.width;
        mouseInput -= new Vector2(0.5f, 0.5f);
        mouseInput *= 2;

        mouseInput.x = Mathf.Clamp(mouseInput.x, -1, 1);
        mouseInput.y = Mathf.Clamp(mouseInput.y, -1, 1);

        Aircraft.SetPitchInput(mouseInput.y);
        Aircraft.SetRollInput(mouseInput.x);
	}


    private void SetFlaps()
    {
        int flapsValue = Aircraft.FlapsValue;
        if (Input.GetKeyDown(KeyCode.R)) flapsValue++;
        if (Input.GetKeyDown(KeyCode.F)) flapsValue--;
        Aircraft.SetFlapsValue(flapsValue);
    }


    private void SetThrust()
    {
        float thrustValue = Aircraft.ThrustValue;
        if (Input.GetKey(KeyCode.LeftShift)) thrustValue += thrustSensitivity * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftControl)) thrustValue -= thrustSensitivity * Time.deltaTime;
        Aircraft.SetThrustValue(thrustValue);
    }


    private void Update()
    {
        SetInputs();
        SetFlaps();
        SetThrust();
    }
}
