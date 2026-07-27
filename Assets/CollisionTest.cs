using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (Input.GetKey(KeyCode.O))
		{
			print($"{collision.collider.name}");

			Time.timeScale = 0;
		}
	}
}
