using Systems;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.World.DamageableObjects
{
    public class SimpleTarget : MonoBehaviour,IDamageable
    {
        [SerializeField] private float _flyHeight = 10;
        [SerializeField] private float _flyTime = 2;
        
        [Inject] private TextPoolController _textPool;
        
        public void TakeDamage(int damage)
        {
            Debug.Log($"Take damage {damage}");
            ShowDamageText(damage);
        }

        private void ShowDamageText(int damage)
        {
            var freeText = _textPool.GetFreeText();

            if (freeText != null)
            {
                freeText.text = ($"-{damage}");

                freeText.transform.position = transform.position;
                freeText.DOFade(1, 0);
                
                var sequence = DOTween.Sequence();
                sequence.Append(freeText.transform.DOMoveY(transform.position.y + _flyHeight, _flyTime));
                sequence.Join(freeText.DOFade(0, _flyTime));
                sequence.OnComplete((() => freeText.gameObject.SetActive(false)));
            }
        }
    }
}