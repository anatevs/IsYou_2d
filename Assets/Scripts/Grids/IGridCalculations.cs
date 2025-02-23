using UnityEngine;

namespace GameCore
{
    public interface IGridCalculations
    {
        public Vector2Int MapSize { get; }

        public Vector3Int GetTileIndex(Vector3 position);
    }
}