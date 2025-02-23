namespace GameCore
{
    public class PushPropertyElement : PlaiyingElement
    {
        public PushPropertyElement()
        {
            Type = ElementType.Property;

            IsInteractable = true;
            IsMovable = true;
        }

        public override void ApplyProperty(PlaiyingElement[] subjectElements)
        {
            foreach (var element in subjectElements)
            {
                element.IsInteractable = true;
                element.IsMovable = true;
            }
        }

        public override void CancelProperty(PlaiyingElement[] subjectElements)
        {
            foreach (var element in subjectElements)
            {
                element.IsInteractable = false;
                element.IsMovable = false;
            }
        }
    }
}