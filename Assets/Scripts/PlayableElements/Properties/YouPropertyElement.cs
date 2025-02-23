using UnityEngine;

namespace GameCore
{
    public sealed class YouPropertyElement : PlaiyingElement
    {
        [SerializeField]
        private Player _player;

        public YouPropertyElement()
        {
            Type = ElementType.Property;
            IsInteractable = true;
            IsMovable = true;
        }

        public override void ApplyProperty(PlaiyingElement[] subjectElements)
        {
            if (subjectElements.Length > 1)
            {
                return;
            }

            foreach (var element in subjectElements)
            {
                _player.SetPlayer(element);
            }
        }

        public override void CancelProperty(PlaiyingElement[] subjectElements)
        {
            foreach (var element in subjectElements)
            {
                if (_player.CurrentPlayer == element)
                {
                    _player.SetPlayer(null);
                }
            }
        }
    }
}