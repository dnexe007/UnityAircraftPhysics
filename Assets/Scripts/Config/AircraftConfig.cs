using System.Linq;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
	public MassConfig massConfig;
	public EngineConfig engineConfig;
	public WingConfig wingConfig;
	public MovementDragConfig movementDragConfig;
	public AngularDragConfig angularDragConfig;

	[SerializeField] private List<AerodynamicSurfaceConfig> surfaceConfigs = new()
	{
		AerodynamicSurfaceConfig.PitchSetup()
	};
	public AerodynamicSurfaceConfig GetSurfaceConfigByName(string name)
	{
		return surfaceConfigs.FirstOrDefault(x => x.SurfaceName == name);
	}

	private void OnValidate()
	{
		massConfig.UpdateData();
	}
}


