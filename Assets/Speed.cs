using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public FlightData fd;

    private TMP_Text text;

    private void Start()
    {
         text = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        Vector2 vel = new(fd.LocalVelocity.z, fd.LocalVelocity.y);
        text.text = $"{Mathf.Round(vel.magnitude * Common.MsToKnots)} KTS";
    }
}
