using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewCurvesManager", menuName = "ScriptableObjects/CurvesManager")]
public class CurvesManager : ScriptableObject
{
    [Serializable]
    private class CurveWithName
    {
        public string name;
        public AnimationCurve curve;
    }

    [SerializeField] private List<CurveWithName> curves = new();
}
