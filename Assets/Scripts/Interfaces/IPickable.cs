using UnityEngine;

namespace Interfaces
{
    public interface IPickable : IInteractable
    {
        public void Pick(Transform character);
    }
}