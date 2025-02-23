using UnityEngine;

namespace GameCore
{
    public sealed class PointingElement : PlaiyingElement
    {
        [SerializeField]
        private PlaiyingElement[] _pointingElement;

        public PointingElement()
        {
            Type = ElementType.Pointer;
            IsInteractable = true;
            IsMovable = true;
        }

        public override PlaiyingElement[] GetPointer()
        {
            return _pointingElement;
        }
    }
}