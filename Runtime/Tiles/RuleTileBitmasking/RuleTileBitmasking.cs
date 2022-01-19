using System;
using UnityEngine.Tilemaps;

namespace UnityEngine
{
    /// <summary>
    /// Generic visual tile for creating different tilesets like terrain, pipeline, random or animated tiles.
    /// </summary>
    [Serializable]
    public class RuleTileBitmasking : TileBase
    {
        /// <summary>
        /// The Sprites used for randomizing output.
        /// </summary>
        [SerializeField]
        public Sprite[] m_Sprites;

        /// <summary>
        /// List of bitmasking values.
        /// </summary>
        private readonly static ushort[] m_BitmaskingValues = new ushort[]
        {
            47, 0, 1, 0, 0, 0, 0, 0, 2, 0, 3, 4, 0, 0, 0, 0, 5, 0, 6, 0, 0, 0, 7, 0, 8, 0, 9, 10, 0, 0,
            11, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 13, 0, 14, 0, 0, 0, 0, 0, 15, 0, 16, 17, 0, 0, 0, 0, 18, 0, 19, 0, 0, 0, 20, 0,
            21, 0, 22, 23, 0, 0, 24, 25, 0, 0, 0, 0, 0, 0, 0, 0, 26, 0, 27, 28, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 29, 0, 30, 31, 0, 0, 32, 33, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 34, 0, 35, 0, 0, 0, 36, 0, 37, 0, 38, 39, 0, 0, 40, 41, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 42, 0, 43, 44, 0, 0, 45, 46
        };

        /// <summary>
        /// Retrieves any tile rendering data from the scripted tile.
        /// </summary>
        /// <param name="position">Position of the Tile on the Tilemap.</param>
        /// <param name="tilemap">The Tilemap the tile is present on.</param>
        /// <param name="tileData">Data to render the tile.</param>
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);

            int index = (GetBitmaskingValue(position, tilemap));

            if ((m_Sprites != null) && (m_Sprites.Length > 0))
            {
                tileData.sprite = m_Sprites[index];
            }
        }

        /// <summary>
        /// Get the bitmasking value for the given position.
        /// </summary>
        /// <param name="position">Position of the Tile on the Tilemap.</param>
        /// <param name="tilemap">The Tilemap the tile is present on.</param>
        /// <returns>The bitmasking value.</returns>
        public int GetBitmaskingValue(Vector3Int position, ITilemap tileMap)
        {
            bool northTile = TileValue(GetOffsetPosition(position, new Vector3Int(0, 1, 0)), tileMap);
            bool southTile = TileValue(GetOffsetPosition(position, new Vector3Int(0, -1, 0)), tileMap);
            bool westTile = TileValue(GetOffsetPosition(position, new Vector3Int(-1, 0, 0)), tileMap);
            bool eastTile = TileValue(GetOffsetPosition(position, new Vector3Int(1, 0, 0)), tileMap);
            bool northWestTile = TileValue(GetOffsetPosition(position, new Vector3Int(-1, 1, 0)), tileMap) && westTile && northTile;
            bool northEastTile = TileValue(GetOffsetPosition(position, new Vector3Int(1, 1, 0)), tileMap) && northTile && eastTile;
            bool southWestTile = TileValue(GetOffsetPosition(position, new Vector3Int(-1, -1, 0)), tileMap) && southTile && westTile;
            bool southEastTile = TileValue(GetOffsetPosition(position, new Vector3Int(1, -1, 0)), tileMap) && southTile && eastTile;

            int index = 1 * ((northWestTile) ? 1 : 0) +
                        2 * ((northTile) ? 1 : 0) +
                        4 * ((northEastTile) ? 1 : 0) +
                        8 * ((westTile) ? 1 : 0) +
                        16 * ((eastTile) ? 1 : 0) +
                        32 * ((southWestTile) ? 1 : 0) +
                        64 * ((southTile) ? 1 : 0) +
                        128 * ((southEastTile) ? 1 : 0);

            return m_BitmaskingValues[index];
        }

        private bool TileValue(Vector3Int position, ITilemap tileMap)
        {
            TileBase tile = tileMap.GetTile(position);
            return (tile != null && tile == this);
        }

        /// <summary>
        /// Get the offset for the given position with the given offset.
        /// </summary>
        /// <param name="position">Position to offset.</param>
        /// <param name="offset">Offset for the position.</param>
        /// <returns>The offset position.</returns>
        public virtual Vector3Int GetOffsetPosition(Vector3Int position, Vector3Int offset)
        {
            return position + offset;
        }
    }
}