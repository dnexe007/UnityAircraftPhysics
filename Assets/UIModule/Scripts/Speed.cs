using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speed : MonoBehaviour
{
	private UIManager root;

	private TMP_Text text;

	private void Start()
	{
		text = GetComponentInChildren<TMP_Text>();
		root = GetComponentInParent<UIManager>();
	}

	private void Update()
	{
		Vector2 vel = new(root.Aircraft.LocalVelocity.z, root.Aircraft.LocalVelocity.y);
		text.text = $"{Mathf.Round(vel.magnitude * Constants.MsToKnots)} KTS";
	}
}
