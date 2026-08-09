using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private float startX;
    [SerializeField] private float startZ;
    [SerializeField] private float endX;
    [SerializeField] private float endZ;
    [SerializeField] private float totalForce;
    [SerializeField] [Range(2, 20)] private int numOfPoints;
    [SerializeField][Range(1, 20)] private float edgePointsDeltaCoef;


	private void OnDrawGizmos()
	{
        Gizmos.color = Color.blue;
        Vector3 startPos = transform.TransformPoint(new Vector3(startX, 0, startZ));
        Vector3 endPos = transform.TransformPoint(new Vector3(endX, 0, endZ));
        Gizmos.DrawLine(startPos, endPos);


        float endPointMult = 1 / (1 + edgePointsDeltaCoef);
        float startPointMult = endPointMult * edgePointsDeltaCoef;

        for(float i = 0; i < numOfPoints; i++)
        {
            float pointValue = i / (numOfPoints - 1);
            float pointMult = Mathf.Lerp(startPointMult, endPointMult, pointValue);

			Gizmos.DrawWireSphere(Vector3.Lerp(startPos, endPos, pointValue), pointMult * totalForce / numOfPoints);
        }
	}

}
