using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sliders : MonoBehaviour
{
    private UIManager root;

    private RectTransform thrustSlider;
    private RectTransform flapsSlider;


    private TMP_Text thrustText;
    private TMP_Text flapsText;

    private void Start()
    {
        thrustSlider = transform.Find("Thrust/Image").GetComponent<RectTransform>();
        thrustText = thrustSlider.GetComponentInChildren<TMP_Text>();

        flapsSlider = transform.Find("Flaps/Image").GetComponent<RectTransform>();
        flapsText = flapsSlider.GetComponentInChildren<TMP_Text>();

        root = GetComponentInParent<UIManager>();
    }


    private void Update()
    {
        thrustText.text = $"THRUST\n{(Mathf.Round(root.Aircraft.ThrustValue * 100))}%";
        thrustSlider.anchoredPosition = new Vector2(0, root.Aircraft.ThrustValue * 200);


        flapsText.text = $"FLAPS\n{root.Aircraft.FlapsValue}/{root.Aircraft.Config.WingConfig.FlapsSteps}";
        flapsSlider.anchoredPosition = new Vector2(0, root.Aircraft.FlapsValue01 * 100);
    }
}
