using Gameplay.Weapons.ConcreteWeapon;
using UnityEngine;

namespace Gameplay.Weapons.AnimationControllers
{
    public class LightsaberAnimation : WeaponAnimation
    {
        [SerializeField] private GameObject _laser;
        
        private bool _active = true;
        private bool _animationActive = false;
        private Lightsaber _lightsaber;
        
        private static readonly int Deactivate = Animator.StringToHash(DeactivateAnimation);
        private static readonly int Activate = Animator.StringToHash(ActivateAnimation);
        private static readonly int UseCyclical = Animator.StringToHash(CyclicalUsingAnimation);
        
        private const string ActivateAnimation = "Activate";
        private const string DeactivateAnimation = "Deactivate";
        private const string CyclicalUsingAnimation = "CyclicalUsing";

        protected override void Awake()
        {
            base.Awake();

            _lightsaber = _weapon.GetComponent<Lightsaber>();
            
            if (_lightsaber != null)
            {
                _lightsaber.CyclicallyUsingEvent += OnUseCyclical;
                _lightsaber.StopCyclicalUsingEvent += OnUseCyclicalStop;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_lightsaber != null)
            {
                _lightsaber.CyclicallyUsingEvent -= OnUseCyclical;
                _lightsaber.StopCyclicalUsingEvent -= OnUseCyclicalStop;
            }
        }

        protected override void OnReload()
        {
            if (_animationActive)
                return;

            _animationActive = true;
            
            var animationName = _active 
                ? Deactivate 
                : Activate;
            
            _animator.SetTrigger(animationName);
        }

        protected override void OnShoot()
        {
            if (!_active)
                return;
            
            base.OnShoot();
        }

        private void OnUseCyclical()
        {
            _animator.SetBool(UseCyclical,true);
        }
        
        private void OnUseCyclicalStop()
        {
            _animator.SetBool(UseCyclical,false);
        }


        private void DisableLaser()
        {
            _laser.SetActive(false);
            _active = false;
            _animationActive = false;
        }

        private void ActivateLaser()
        {
            _laser.SetActive(true);
            _active = true;
            _animationActive = false;
        }
        
        private void SetLaserActivity(bool value)
        {
            _laser.SetActive(value);
            _active = value;
            _animationActive = false;
        }
    }
}