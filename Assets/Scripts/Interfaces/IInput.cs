using System;

namespace Interfaces
{
    public interface IInput
    {
        public float Horizontal => 0;
        public float Vertical => 0;

        public float RotationHorizontal => 0;
        public float RotationVertical => 0;

        public event Action Interact;

        public bool Run { get; }
    }
}