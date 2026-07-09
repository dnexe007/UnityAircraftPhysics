using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAircraft", menuName = "ScriptableObjects/AircraftConfig")]
public class AircraftConfig : ScriptableObject
{
	[field: SerializeField] public MassConfig MassConfig { get; private set; } = new();
	[field: SerializeField] public EngineConfig EngineConfig { get; private set; } = new();
	[field: SerializeField] public WingConfig WingConfig { get; private set; } = new();
	[field: SerializeField] public MovementDragConfig MovementDragConfig { get; private set; } = new();
	[field: SerializeField] public AngularDragConfig AngularDragConfig { get; private set; } = new();

	[SerializeField] private List<AerodynamicSurfaceConfig> surfaceConfigs = new()
	{
		AerodynamicSurfaceConfig.DefaultSetup
	};

	public AerodynamicSurfaceConfig GetSurfaceConfigByName(string name)
	{
		return surfaceConfigs.FirstOrDefault(x => x.SurfaceName == name);
	}

	private void OnValidate()
	{
		MassConfig.UpdateData();
	}

	[ContextMenu("Add default surface config")]
	private void AddSurfaceConfig()
	{
		surfaceConfigs.Add(AerodynamicSurfaceConfig.DefaultSetup);
	}
}


