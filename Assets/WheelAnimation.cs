using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimation : MonoBehaviour
{
    [Serializable]
    public struct Wheel
    {
        public Transform model;
        public WheelCollider collider;

        public Wheel(Transform model, WheelCollider collider)
        {
            this.model = model;
            this.collider = collider;
        }
    }


    [SerializeField] private Transform[] wheelTransforms;

    [SerializeField] private Wheel[] wheels;

    private void Start()
    {
        wheels = new Wheel[wheelTransforms.Length];
        for(int i = 0; i < wheelTransforms.Length; i++)
        {
            var wheel = wheelTransforms[i];
            var collider = wheel.GetComponentInChildren<WheelCollider>();
            var model = wheel.Find("Model");
            wheels[i] = new(model, collider);
        }
    }


    private void Update()
    {
        foreach(var wheel in wheels)
        {
            Vector3 pos;
            Quaternion rot;

            wheel.collider.GetWorldPose(out pos, out rot);

            wheel.model.position = pos;
            wheel.model.rotation = rot;
        }
    }
}
