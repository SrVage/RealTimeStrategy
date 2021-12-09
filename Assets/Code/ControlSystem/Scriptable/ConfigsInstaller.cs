using System;
using Code.Abstractions;
using Code.Tools.Scriptable;
using UnityEngine;
using Zenject;

namespace Code.ControlSystem.Scriptable
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        public GroundPointValue GroundPointValue;
        public SelectableValue SelectableValue;
        public AssetsContext AssetsContext;
        public AttackedValue AttackedValue;
        [SerializeField]private Sprite _footmanSprite;

        public override void InstallBindings()
        {
            Container.Bind<IAwaitable<ICanAttacked>>().FromInstance(AttackedValue);
            Container.Bind<IAwaitable<Vector3>>().FromInstance(GroundPointValue);
            Container.Bind<IObservable<ISelectable>>().FromInstance(SelectableValue);
            Container.Bind<Sprite>().WithId("FootmanPBR").FromInstance(_footmanSprite);
            Container.BindInstances(SelectableValue, AssetsContext);
        }
    }
}