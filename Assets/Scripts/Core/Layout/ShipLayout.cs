using UnityEngine;
using System.Collections.Generic;

namespace FTL.Core.Layout
{
    public class ShipLayout
    {
        public Vector2Int GridSize { get; private set; }
        public Dictionary<Vector2Int, GridCell> Cells { get; private set; }

        public Vector2 CellSize { get; private set; }
        public float CellGap { get; private set; }
        public float PixelsPerUnit { get; private set; }
        public Vector2 GridOrigin { get; private set; }

        private const float DEFAULT_CELL_SIZE = 32f;
        private const float DEFAULT_CELL_GAP = 1f;
        private const float DEFAULT_PIXELS_PER_UNIT = 1f;

        public ShipLayout(Vector2Int gridSize, Vector2 cellSize = default, float cellGap = DEFAULT_CELL_GAP, float pixelsPerUnit = DEFAULT_PIXELS_PER_UNIT, Vector2 gridOrigin = default)
        {
            GridSize = gridSize;
            CellSize = cellSize == default ? new Vector2(DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE) : cellSize;
            CellGap = cellGap;
            PixelsPerUnit = pixelsPerUnit;
            GridOrigin = gridOrigin;

            Cells = new Dictionary<Vector2Int, GridCell>();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    Cells[position] = new GridCell(position, true);
                }
            }
        }

        public GridCell GetCell(Vector2Int position)
        {
            return Cells.TryGetValue(position, out GridCell cell) ? cell : null;
        }

        public GridCell GetCell(int x, int y)
        {
            return GetCell(new Vector2Int(x, y));
        }

        public bool IsValidPosition(Vector2Int position)
        {
            return position.x >= 0 && position.x < GridSize.x &&
                   position.y >= 0 && position.y < GridSize.y;
        }

        public bool IsCellEnabled(Vector2Int position)
        {
            var cell = GetCell(position);
            return cell != null && cell.IsEnabled;
        }

        public void SetCellEnabled(Vector2Int position, bool enabled)
        {
            var cell = GetCell(position);
            if (cell != null)
            {
                cell.SetEnabled(enabled);
            }
        }

        public Vector2 CellToWorldPosition(Vector2Int cellPos)
        {
            if (!IsValidPosition(cellPos))
            {
                Debug.LogWarning($"Invalid cell position: {cellPos}");
                return GridOrigin;
            }

            float cellSpacing = CellSize.x + CellGap;
            float worldX = GridOrigin.x + cellPos.x * cellSpacing;
            float worldY = GridOrigin.y - cellPos.y * cellSpacing;

            return new Vector2(worldX, worldY);
        }

        public Vector2Int WorldToCellPosition(Vector2 worldPos)
        {
            float cellSpacing = CellSize.x + CellGap;
            float relativeX = worldPos.x - GridOrigin.x;
            float relativeY = GridOrigin.y - worldPos.y;

            int cellX = Mathf.FloorToInt(relativeX / cellSpacing);
            int cellY = Mathf.FloorToInt(relativeY / cellSpacing);

            return new Vector2Int(cellX, cellY);
        }

        public Vector2 GetWorldSize()
        {
            float cellSpacing = CellSize.x + CellGap;
            float width = GridSize.x * cellSpacing - CellGap;
            float height = GridSize.y * cellSpacing - CellGap;
            return new Vector2(width, height);
        }

        public Bounds GetCellBounds(Vector2Int cellPos)
        {
            Vector2 worldPos = CellToWorldPosition(cellPos);
            Vector2 center = new Vector2(
                worldPos.x + CellSize.x * 0.5f,
                worldPos.y - CellSize.y * 0.5f
            );
            return new Bounds(center, CellSize);
        }
    }
}

