using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameCore
{
    public sealed class TilesCalculations : MonoBehaviour,
        IGridCalculations
    {
        public Vector2Int MapSize => _mapSize;

        [SerializeField]
        private Tilemap _backgroundTiles;

        private Vector2Int _mapSize = new();

        private void Awake()
        {
            _mapSize = (Vector2Int)_backgroundTiles.size;
        }

        public Vector3Int GetTileIndex(Vector3 position)
        {
            return _backgroundTiles.WorldToCell(position);
        }

        public Vector2 GetTileCoordinates(Vector3 position)
        {
            var cellPos = GetTileIndex(position);

            return _backgroundTiles.CellToWorld(cellPos);
        }
    }
}