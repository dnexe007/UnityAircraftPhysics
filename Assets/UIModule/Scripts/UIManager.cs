using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[field: SerializeField] public PlayerControls Player { get; private set; }

	[SerializeField] private float overload;


	public Aircraft Aircraft => Player.Aircraft;


	private void FixedUpdate()
	{
		overload = Aircraft.Overload;
	}

}
