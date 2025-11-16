using UnityEngine;
using System.Collections.Generic;

namespace FTL.Core.Layout
{
    public class GridCell
    {
        public Vector2Int Position { get; private set; }
        public bool IsEnabled { get; private set; }

        public GridCell(Vector2Int position, bool isEnabled = true)
        {
            Position = position;
            IsEnabled = isEnabled;
        }

        public void SetEnabled(bool enabled)
        {
            IsEnabled = enabled;
        }

        public bool IsOccupied()
        {
            return false;
        }
    }
}

