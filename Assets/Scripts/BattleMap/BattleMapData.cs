using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class BattleMapData
{
    [System.Serializable]
    public class TileData
    {
        public int x, y, height, slope;
        public Type tileType;

        public TileData(int x, int y, int height, Type tileType, int slope)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.tileType = tileType;
            this.slope = slope;
        }

        public enum Type
        {
            Grass,
            Dirt
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

    public TileData[] GetAllTileData()
    {
        List<TileData> allTileData = new List<TileData>();

        foreach (Dictionary<int, TileData> column in tiles.Values)
        {
            foreach (TileData tile in column.Values)
            {
                allTileData.Add(tile);
            }
        }

        return allTileData.ToArray();
    }

    public void Save(string mapName)
    {
        EnsureDirectoryExists();

        string filePath = Application.persistentDataPath + "/BattleMaps/" + mapName + ".battleMap";
        List<string> fileData = new List<string>();

        TileData[] allTileData = GetAllTileData();
        foreach (TileData tile in allTileData)
        {
            fileData.Add(
                "x" + tile.x +
                "y" + tile.y +
                "h" + tile.height +
                "t" + (int)tile.tileType +
                "s" + tile.slope
            );
        }

        File.WriteAllLines(filePath, fileData.ToArray());
    }

    public void Load(string mapName)
    {
        EnsureDirectoryExists();

        string filePath = Application.persistentDataPath + "/BattleMaps/" + mapName + ".battleMap";

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Can't load map; path does not exist: " + filePath);
            return;
        }

        string[] fileData = File.ReadAllLines(filePath);

        tiles.Clear();
        foreach (string tileString in fileData)
        {
            string[] data = new string[5];
            int dataIndex = -1;

            for (int i = 0; i < tileString.Length; i++)
            {
                if (char.IsDigit(tileString[i]) || tileString[i] == '-')
                {
                    data[dataIndex] += tileString[i];
                }
                else
                {
                    dataIndex++;
                }
            }

            AddTile(new TileData(
                        int.Parse(data[0]),
                        int.Parse(data[1]),
                        int.Parse(data[2]),
                        (TileData.Type)int.Parse(data[3]),
                        int.Parse(data[4])));
        }
    }

    private void EnsureDirectoryExists()
    {
        string directoryPath = Application.persistentDataPath + "/BattleMaps/";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}