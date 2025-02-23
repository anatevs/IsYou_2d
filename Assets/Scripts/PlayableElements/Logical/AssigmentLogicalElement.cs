using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public sealed class AssigmentLogicalElement : PlaiyingElement
    {
        public AssigmentLogicalElement()
        {
            Type = ElementType.Logical;
            IsInteractable = true;
            IsMovable = true;

            foreach (var direction in _ruleDirections)
            {
                _isActiveRules.Add(direction, false);
            }
        }

        private readonly (int x, int y)[] _ruleDirections =
        {
            (1, 0),
            (0, -1)
        };

        private readonly Dictionary<(int x, int y), bool> _isActiveRules = new();

        private bool IsRuleDirection((int x, int y) direction)
        {
            foreach (var ruleDir in _ruleDirections)
            {
                if (ruleDir == direction)
                {
                    return true;
                }
            }

            return false;
        }

        public override void SetRule((int x, int y) direction, PlaiyingElement neighbor)
        {
            var oppositeDirection = (-direction.x, -direction.y);
            var oppositeElementSet = _udlrNeighbors[oppositeDirection];

            if (neighbor.Type == ElementType.Pointer)
            {
                if (IsRuleDirection(oppositeDirection))
                {
                    foreach (var oppositeElement in oppositeElementSet)
                    {
                        oppositeElement.ApplyProperty(neighbor.GetPointer());

                        _isActiveRules[oppositeDirection] = true;
                    }
                }
            }

            else
            {
                if (IsRuleDirection(direction))
                {
                    foreach (var oppositeElement in oppositeElementSet)
                    {
                        if (oppositeElement.Type == ElementType.Pointer)
                        {
                            neighbor.ApplyProperty(oppositeElement.GetPointer());

                            _isActiveRules[direction] = true;
                        }
                    }
                }
            }
        }

        public override void CancelRule((int x, int y) direction, PlaiyingElement neighbor)
        {
            if (_isActiveRules.TryGetValue(direction, out var isRuleActive) && isRuleActive)
            {
                var oppositeDirection = (-direction.x, -direction.y);
                var oppositeElementSet = _udlrNeighbors[oppositeDirection];


                if (neighbor.Type == ElementType.Pointer)
                {
                    foreach (var oppositeElement in oppositeElementSet)
                    {
                        oppositeElement.CancelProperty(neighbor.GetPointer());
                    }
                }

                else
                {
                    foreach (var oppositeElement in oppositeElementSet)
                    {
                        neighbor.CancelProperty(oppositeElement.GetPointer());
                    }
                }

                _isActiveRules[direction] = false;
            }
        }
    }
}