using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedAndOverloadUI : MonoBehaviour
{
	private UIManager root;

	private TMP_Text speedText;
	private TMP_Text overloadText;

	private void Start()
	{
		speedText = transform.Find("Speed").GetComponentInChildren<TMP_Text>();
		overloadText = transform.Find("Overload").GetComponentInChildren<TMP_Text>();
		root = GetComponentInParent<UIManager>();
	}

	private void Update()
	{
		Vector2 localVelocity = new(root.Aircraft.LocalVelocity.z, root.Aircraft.LocalVelocity.y);
		float speed = Mathf.Round(localVelocity.magnitude * Constants.MsToKnots);
		speedText.text = $"{speed} KTS";

		float overload = MathF.Round(root.Aircraft.Overload, 1);
		overloadText.text = $"{overload} G";
	}
}
