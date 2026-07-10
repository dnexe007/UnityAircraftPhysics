using Unity.VisualScripting;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    [field: SerializeField] public AircraftConfig Config { get; private set; }
	

	public Vector3 LocalVelocity { get; private set; }
	public float Roll { get; private set; }
	public float Pitch { get; private set; }
	public Vector3 RightHorizontalVector { get; private set; }
	public float VerticalAOA { get; private set; }
	public float HorizontalAOA { get; private set; }

	public float Overload { get; private set; }
	public void SetFlightData(
		Vector3 localVelocity,
		Vector3 rightHorizontalVector,
		float roll,
		float pitch,
		float verticalAOA,
		float horizontalAOA,
		float overload
	)
	{
		LocalVelocity = localVelocity;
		RightHorizontalVector = rightHorizontalVector;
		Roll = roll;
		Pitch = pitch;
		VerticalAOA = verticalAOA;
		HorizontalAOA = horizontalAOA;
		Overload = overload;
	}


	public float PitchInput { get; private set; }
	public float RollInput { get; private set; }
	public float YawInput { get; private set; }
	public float ThrustValue { get; private set; }
	public int FlapsSteps { get; private set; }
	public int FlapsValue { get; private set; }
	public float FlapsValue01 => (float)FlapsValue / FlapsSteps;
	public void SetPitchInput(float value) => PitchInput = Mathf.Clamp(value, -1, 1);
	public void SetRollInput(float value) => RollInput = Mathf.Clamp(value, -1, 1);
	public void SetYawInput(float value) => YawInput = Mathf.Clamp(value, -1, 1);
	public void SetThrustValue(float value) => ThrustValue = Mathf.Clamp01(value);
	public void SetFlapsValue(int value) => FlapsValue = Mathf.Clamp(value, 0, FlapsSteps);


	private void Start() => FlapsSteps = Config.WingConfig.FlapsSteps;
}