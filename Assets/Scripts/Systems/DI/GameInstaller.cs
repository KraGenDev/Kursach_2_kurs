using Gameplay.World.DamageableObjects;
using Interfaces;
using UI;
using UnityEngine;
using Zenject;

namespace Systems.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _inputObject;
        [SerializeField] private WindowsController _windowsController;
        [SerializeField] private TextPoolController _textPoolController;

        public override void InstallBindings()
        {
            var input = _inputObject.GetComponent<IInput>();
            var weaponInput = _inputObject.GetComponent<IWeaponInput>();
            
            Container.Bind<IInput>().FromInstance(input).AsSingle();
            Container.Bind<IWeaponInput>().FromInstance(weaponInput).AsSingle();
            Container.Bind<WindowsController>().FromInstance(_windowsController).AsSingle();
            Container.Bind<TextPoolController>().FromInstance(_textPoolController).AsSingle();
        }
    }
}