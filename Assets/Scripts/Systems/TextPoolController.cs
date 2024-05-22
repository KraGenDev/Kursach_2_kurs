using TMPro;
using UnityEngine;

namespace Systems
{
    public class TextPoolController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textPrefab;
        [SerializeField] private Transform _textsContainer;
        [SerializeField] private int _textsCount = 10;
        
        private PoolMono<TextMeshPro> _textPool;

        
        private void Awake()
        {
            _textPool = new PoolMono<TextMeshPro>(_textPrefab, _textsCount, _textsContainer) {AutoExpand = true};
        }
        
        public TextMeshPro GetFreeText()
        {
            return _textPool.GetFreeElement();
        }
    }
}