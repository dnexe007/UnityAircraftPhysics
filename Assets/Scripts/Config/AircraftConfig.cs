using System;
using UnityEngine;



[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
    public float mass = 9_000;
    public Vector3 tensor = new(70_000, 80_000, 15_000);
    public float enginesThrust = 12;
    [Range(1, 10)] public int flapsSteps = 5;
    public WingConfig wingParams;
    public ControlSurfaceConfig pitchParams;
    public ControlSurfaceConfig aileronParams;
    public ControlSurfaceConfig rudderParams;
    public MovementDragConfig fuselageDragParams;
    public AngularDragConfig fuselageAngularDragParams;
    public WheelProfile[] wheelProfiles;

    public WheelProfile GetWheelProfile(string name)
    {
        foreach(WheelProfile profile in wheelProfiles)
        {
            if(profile.name == name) return profile;
        }
        return null;
    }
}


