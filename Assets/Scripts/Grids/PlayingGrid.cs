using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public sealed class PlayingGrid : MonoBehaviour
    {
        public int MaxX => _gameElements.GetLength(0);

        public int MaxY => _gameElements.GetLength(1);

        [SerializeField]
        private TilesCalculations _gridCalculations;

        [SerializeField]
        private Transform _parentTransform;

        private PlaiyingElement[,] _gameElements;

        private HashSet<PlaiyingElement>[,] _plaiyingElements;

        private Vector2Int _shiftXYIndex;

        private void AssignGrid()
        {
            _gameElements = new PlaiyingElement[
                _gridCalculations.MapSize.x,
                _gridCalculations.MapSize.y];

            _plaiyingElements = new HashSet<PlaiyingElement>[
                _gridCalculations.MapSize.x,
                _gridCalculations.MapSize.y];

            for (int i = 0; i < _plaiyingElements.GetLength(0); i++)
            {
                for (int j = 0; j < _plaiyingElements.GetLength(1); j++)
                {
                    _plaiyingElements[i, j] = new();
                }
            }

            _shiftXYIndex = new Vector2Int(
                _gridCalculations.MapSize.x / 2,
                _gridCalculations.MapSize.y / 2
                );
        }

        private void FillGrid()
        {
            var elements = _parentTransform.GetComponentsInChildren<PlaiyingElement>();

            foreach (var obj in elements)
            {
                var objIndex = FromPositionToIndex(obj.transform.position);

                obj.SetCurrentIndex(objIndex);

                _gameElements[objIndex.x, objIndex.y] = obj;

                AddElement(new Vector2Int(objIndex.x, objIndex.y), obj);
            }

            FillNeighbors(elements);
        }

        private void FillNeighbors(PlaiyingElement[] elements)
        {
            foreach (var obj in elements)
            {
                obj.InitNeighbors();
            }
        }

        private void Start()
        {
            AssignGrid();

            FillGrid();
        }

        public void AddElement(Vector2Int index, PlaiyingElement element)
        {
            _plaiyingElements[index.x, index.y].Add(element);
        }

        public HashSet<PlaiyingElement> GetElementsSet(Vector2Int index)
        {
            if (!(index.x < 0 || index.x >= _gameElements.GetLength(0) ||
                index.y < 0 || index.y >= _gameElements.GetLength(1)))
            {
                return _plaiyingElements[index.x, index.y];
            }

            return null;
        }

        public void RemoveElement(Vector2Int index, PlaiyingElement element)
        {
            if (_plaiyingElements[index.x, index.y].Contains(element))
            {
                _plaiyingElements[index.x, index.y].Remove(element);
            }
        }

        public bool TryGetElement_Index(Vector2Int index, out HashSet<PlaiyingElement> elementsSet)
        {
            elementsSet = null;

            if (index.x < 0 || index.x >= _gameElements.GetLength(0) ||
                index.y < 0 || index.y >= _gameElements.GetLength(1))
            {
                return false;
            }

            elementsSet = _plaiyingElements[index.x, index.y];
            return true;
        }

        public Vector2Int FromPositionToIndex(Vector3 position)
        {
            var cellIndex = _gridCalculations.GetTileIndex(position);

            return FromCellToObjectsIndex(cellIndex);
        }

        private Vector2Int FromCellToObjectsIndex(Vector3Int cellIndex)
        {
            return new Vector2Int(cellIndex.x + _shiftXYIndex.x,
                cellIndex.y + _shiftXYIndex.y);
        }
    }
}