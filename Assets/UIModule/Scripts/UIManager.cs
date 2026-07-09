using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[field: SerializeField] public PlayerControls Player { get; private set; }
	public Aircraft Aircraft => Player.Aircraft;

}
