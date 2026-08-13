using UnityEngine;

public struct WingPoint
{
	public readonly Vector3 position;
	public readonly float forceMult;
	public WingPoint(Vector3 position, float forceMult)
	{
		this.position = position;
		this.forceMult = forceMult;
	}
}