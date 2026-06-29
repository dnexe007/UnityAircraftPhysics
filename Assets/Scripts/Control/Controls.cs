using System;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls singletone;
    public float RudderInput => (Input.GetKey(KeyCode.E)? 1: 0) - (Input.GetKey(KeyCode.Q)? 1: 0);
    public Vector2 YokeInput { get; private set; }

    private bool isActive;

    public event Action<int> OnFlapsChange;

    private void Awake()
    {
        singletone = this;
    }

    private void OnDestroy()
    {
        singletone = null;
    }

    private void SetThrust()
    {
        if (Input.GetMouseButtonDown(1))
            isActive = !isActive;

        if (!isActive)
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
    }

    private void SetFlaps()
    {
        if (Input.GetKeyDown(KeyCode.R))
            OnFlapsChange?.Invoke(1);
        if (Input.GetKeyDown(KeyCode.F))
            OnFlapsChange?.Invoke(-1);
    }

    private void Update()
    {
        SetThrust();
        SetFlaps();
    }
}
