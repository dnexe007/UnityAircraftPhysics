using System;
using System.Globalization;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCurveGenerator", menuName = "Tools/CurveGenerator")]
public class CurveGenerator: ScriptableObject
{
	[SerializeField] private AnimationCurve curve;
	[SerializeField] private string result;
	private string NumToStr(float number)
	{
		return Math.Round(number, 3).ToString(CultureInfo.InvariantCulture) + "f";
	}


	[ContextMenu("PrintConstructor")]
	private void PrintConstructor()
	{
		StringBuilder sb = new("new(\n");
		for(int i = 0; i < curve.keys.Length; i++)
		{
			Keyframe k = curve.keys[i];
			sb.Append(
				$"	new({NumToStr(k.time)}, " +
				$"{NumToStr(k.value)}, " +
				$"{NumToStr(k.inTangent)}, " +
				$"{NumToStr(k.outTangent)})"
			);
			if(i != curve.keys.Length -1) sb.Append(",");
			sb.Append("\n");
		}
		sb.Append(");");
		result = sb.ToString();
	}
}
