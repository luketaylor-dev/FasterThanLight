using UnityEngine;
using FTL.Core.Layout;

namespace FTL.Core.Components
{
    /// <summary>
    /// MonoBehaviour component for visualizing and managing a ship layout in the Unity editor.
    /// </summary>
    public class ShipLayoutComponent : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField] private Vector2 cellSize = new Vector2(32f, 32f);
        [SerializeField] private float cellGap = 1f;
        [SerializeField] private float pixelsPerUnit = 1f;

        [Header("Gizmo Settings")]
        [SerializeField] private bool showGrid = true;
        [SerializeField] private bool showCellBounds = false;
        [SerializeField] private Color gridColor = Color.white;
        [SerializeField] private Color disabledCellColor = Color.red;
        [SerializeField] private Color enabledCellColor = Color.green;

        private ShipLayout layout;

        public ShipLayout Layout
        {
            get
            {
                if (layout == null || layout.GridSize != gridSize)
                {
                    InitializeLayout();
                }
                return layout;
            }
        }

        private void OnValidate()
        {
            // Ensure grid size is valid
            gridSize.x = Mathf.Max(1, gridSize.x);
            gridSize.y = Mathf.Max(1, gridSize.y);

            // Ensure cell size is valid
            cellSize.x = Mathf.Max(1f, cellSize.x);
            cellSize.y = Mathf.Max(1f, cellSize.y);

            // Ensure gap is non-negative
            cellGap = Mathf.Max(0f, cellGap);

            // Reinitialize layout if settings changed
            if (layout != null && (layout.GridSize != gridSize ||
                layout.CellSize != cellSize ||
                Mathf.Abs(layout.CellGap - cellGap) > 0.001f))
            {
                InitializeLayout();
            }
        }

        private void Awake()
        {
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            Vector2 origin = new Vector2(transform.position.x, transform.position.y);
            layout = new ShipLayout(gridSize, cellSize, cellGap, pixelsPerUnit, origin);
        }

        private void OnDrawGizmos()
        {
            if (!showGrid) return;

            if (!Application.isPlaying)
            {
                InitializeLayout();
            }

            var currentLayout = Layout;
            if (currentLayout == null) return;

            DrawGrid(currentLayout);
        }

        private void DrawGrid(ShipLayout layout)
        {
            float cellSpacing = cellSize.x + cellGap;

            Gizmos.color = gridColor;

            for (int x = 0; x <= gridSize.x; x++)
            {
                float worldX = transform.position.x + x * cellSpacing;
                float startY = transform.position.y;
                float endY = transform.position.y - gridSize.y * cellSpacing;

                Vector3 start = new Vector3(worldX, startY, transform.position.z);
                Vector3 end = new Vector3(worldX, endY, transform.position.z);
                Gizmos.DrawLine(start, end);
            }

            for (int y = 0; y <= gridSize.y; y++)
            {
                float worldY = transform.position.y - y * cellSpacing;
                float startX = transform.position.x;
                float endX = transform.position.x + gridSize.x * cellSpacing;

                Vector3 start = new Vector3(startX, worldY, transform.position.z);
                Vector3 end = new Vector3(endX, worldY, transform.position.z);
                Gizmos.DrawLine(start, end);
            }

            if (showCellBounds)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    for (int y = 0; y < gridSize.y; y++)
                    {
                        Vector2Int cellPos = new Vector2Int(x, y);
                        GridCell cell = layout.GetCell(cellPos);

                        if (cell != null)
                        {
                            Gizmos.color = cell.IsEnabled ? enabledCellColor : disabledCellColor;

                            Vector2 worldPos = layout.CellToWorldPosition(cellPos);
                            Vector3 center = new Vector3(
                                worldPos.x + cellSize.x * 0.5f,
                                worldPos.y - cellSize.y * 0.5f,
                                transform.position.z
                            );

                            Vector3 size = new Vector3(cellSize.x, cellSize.y, 0.1f);
                            Gizmos.DrawWireCube(center, size);
                        }
                    }
                }
            }
        }

        public ShipLayout GetLayout()
        {
            return Layout;
        }

        public void SetCellEnabled(Vector2Int position, bool enabled)
        {
            Layout.SetCellEnabled(position, enabled);
        }

        public bool IsCellEnabled(Vector2Int position)
        {
            return Layout.IsCellEnabled(position);
        }
    }
}

