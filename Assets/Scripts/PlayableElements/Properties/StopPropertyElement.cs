namespace GameCore
{
    public sealed class StopPropertyElement : PlaiyingElement
    {
        public StopPropertyElement()
        {
            Type = ElementType.Property;

            IsInteractable = true;
            IsMovable = true;
        }

        public override void ApplyProperty(PlaiyingElement[] subjectElements)
        {
            foreach (var element in subjectElements)
            {
                element.IsMovable = false;
                element.IsInteractable = true;
            }
        }

        public override void CancelProperty(PlaiyingElement[] subjectElements)
        {
            foreach (var element in subjectElements)
            {
                element.IsMovable = true;
                element.IsInteractable = false;
            }
        }
    }
}