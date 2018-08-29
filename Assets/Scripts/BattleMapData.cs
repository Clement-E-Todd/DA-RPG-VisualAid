using System.Collections.Generic;

public class BattleMapData
{
    public class TileData
    {
        public int x, y;

        public TileData(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    Dictionary<int, Dictionary<int, TileData>> tiles = new Dictionary<int, Dictionary<int, TileData>>();

    public TileData GetTileAt(int x, int y)
    {
        if (tiles.ContainsKey(x))
        {
            if (tiles[x].ContainsKey(y))
            {
                return tiles[x][y];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void AddTile(TileData tile)
    {
        if (!tiles.ContainsKey(tile.x))
        {
            tiles.Add(tile.x, new Dictionary<int, TileData>());
        }

        if (tiles[tile.x].ContainsKey(tile.y))
        {
            tiles[tile.x][tile.y] = tile;
        }
        else
        {
            tiles[tile.x].Add(tile.y, tile);
        }
    }

    public void RemoveTileAt(int x, int y)
    {
        if (tiles.ContainsKey(x))
        {
            if (tiles[x].ContainsKey(y))
            {
                tiles[x].Remove(y);

                if (tiles[x].Count == 0)
                {
                    tiles.Remove(x);
                }
            }
        }
    }
}