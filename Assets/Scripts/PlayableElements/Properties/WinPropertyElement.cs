using UnityEngine;

namespace GameCore
{
    public sealed class WinPropertyElement : PlaiyingElement
    {
        [SerializeField]
        private WinManager _winManager;

        [SerializeField]
        private Player _player;

        private PlaiyingElement[] _elements;

        public WinPropertyElement()
        {
            Type = ElementType.Property;
            IsInteractable = true;
            IsMovable = true;
        }

        public override void ApplyProperty(PlaiyingElement[] subjectElements)
        {
            _elements = new PlaiyingElement[subjectElements.Length];

            for (int i = 0; i < subjectElements.Length; i++)
            {
                if (IsPlayer(subjectElements[i]))
                {
                    _elements = new PlaiyingElement[0];

                    MakeOnWin();
                    return;
                }

                subjectElements[i].OnCheckPlayerAndTarget += CheckIsPlayer;

                _elements[i] = subjectElements[i];
            }
        }

        public override void CancelProperty(PlaiyingElement[] subjectElements)
        {
            foreach (var element in subjectElements)
            {;
                element.OnCheckPlayerAndTarget -= CheckIsPlayer;
            }
        }

        private bool IsPlayer(PlaiyingElement element)
        {
            return element == _player.CurrentPlayer;
        }

        private void MakeOnWin()
        {
            CancelProperty(_elements);

            _winManager.MakeOnWin();
        }

        private void CheckIsPlayer(PlaiyingElement element)
        {
            if (IsPlayer(element))
            {
                MakeOnWin();
            }
        }
    }
}