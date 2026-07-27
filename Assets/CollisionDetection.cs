using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private List<float> velocityList = new();



	private void OnCollisionEnter(Collision collision)
	{
		velocityList.Add(collision.impulse.magnitude);
		if(velocityList.Count > 10) velocityList.RemoveAt(0);
	}
}
