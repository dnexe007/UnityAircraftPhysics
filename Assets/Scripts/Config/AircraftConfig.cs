using System.Linq;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
	public MassConfig massConfig;
	public EngineConfig engineConfig;
	public WingConfig wingParams;
	public MovementDragConfig fuselageDragConfig;
	public AngularDragConfig fuselageAngularDragConfig;


	[SerializeField] private List<AerodynamicSurfaceConfig> surfaceConfigs = new() {new()};
	public AerodynamicSurfaceConfig GetSurfaceConfigByName(string name)
	{
		return surfaceConfigs.FirstOrDefault(x => x.SurfaceName == name);
	}
}




