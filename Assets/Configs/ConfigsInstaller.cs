using Code.ControlSystem.Scriptable;
using Code.Tools.Scriptable;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
{
    public GroundPointValue GroundPointValue;
    public SelectableValue SelectableValue;
    public AssetsContext AssetsContext;
    public AttackedValue AttackedValue;
    public override void InstallBindings()
    {
        Container.BindInstances(GroundPointValue, SelectableValue,AttackedValue, AssetsContext);
    }
}