using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sliders : MonoBehaviour
{
    [SerializeField] private FlightData fd;


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
    }

    private void Update()
    {
        thrustText.text = $"THRUST\n{(Mathf.Round(fd.ThrustValue * 100))}%";
        thrustSlider.anchoredPosition = new Vector2(0, fd.ThrustValue * 200);


        flapsText.text = $"FLAPS\n{fd.FlapsValue}/{fd.config.flapsSteps}";
        flapsSlider.anchoredPosition = new Vector2(0, fd.FlapsValue / (float)fd.config.flapsSteps * 100);
    }
}
