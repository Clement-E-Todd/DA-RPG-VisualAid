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
            Dirt,
            Stone
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

    public void Save(string mapName, Transform mapTransform, BattleMapProp[] props)
    {
        EnsureDirectoryExists();

        string filePath = Application.persistentDataPath + "/BattleMaps/" + mapName + ".battleMap";
        List<string> fileData = new List<string>();

        // Save map transform...
        fileData.Add(
            "x" + mapTransform.localPosition.x +
            "y" + mapTransform.localPosition.y +
            "s" + mapTransform.localScale.x
        );

        // Save tile data...
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

        // Svae prop data...
        if (props.Length > 0)
        {
            fileData.Add("PROPS");

            foreach (BattleMapProp prop in props)
            {
                fileData.Add(
                    "n'" + prop.name + "'" +
                    "p'" + prop.spritePath + "'" +
                    "x" + prop.transform.localPosition.x +
                    "y" + prop.transform.localPosition.y +
                    "v" + (prop.visibleToPlayers ? '1' : '0')
                );
            }
        }

        File.WriteAllLines(filePath, fileData.ToArray());
    }

    public void Load(string mapName, Transform mapTransform, bool loadProps)
    {
        EnsureDirectoryExists();

        string filePath = Application.persistentDataPath + "/BattleMaps/" + mapName + ".battleMap";

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Can't load map; path does not exist: " + filePath);
            return;
        }

        string[] fileData = File.ReadAllLines(filePath);
        bool mapTransformLoaded = false;
        bool loadingProps = false;

        tiles.Clear();
        foreach (string dataEntry in fileData)
        {
            // The very first entry is the map's transform info...
            if (!mapTransformLoaded)
            {
                string[] data = new string[3];
                int dataIndex = -1;

                for (int i = 0; i < dataEntry.Length; i++)
                {
                    if (char.IsDigit(dataEntry[i]) || dataEntry[i] == '-' || dataEntry[i] == '.')
                    {
                        if (dataIndex >= data.Length)
                        {
                            Debug.LogError("Error");
                        }

                        data[dataIndex] += dataEntry[i];
                    }
                    else
                    {
                        dataIndex++;
                    }
                }

                mapTransform.localPosition = new Vector3(
                    float.Parse(data[0]),
                    float.Parse(data[1]),
                    0f
                );

                float zoom = float.Parse(data[2]);
                mapTransform.localScale = new Vector3(zoom, zoom, zoom);

                mapTransformLoaded = true;
                continue;
            }

            // Load data for a tile...
            if (!loadingProps)
            {
                // Switch to prop mode if prompted
                if (dataEntry == "PROPS")
                {
                    if (loadProps)
                    {
                        loadingProps = true;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    string[] data = new string[5];
                    int dataIndex = -1;

                    for (int i = 0; i < dataEntry.Length; i++)
                    {
                        if (char.IsDigit(dataEntry[i]) || dataEntry[i] == '-')
                        {
                            data[dataIndex] += dataEntry[i];
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

            // Load data for a prop and add it to the map...
            else
            {
                string[] data = new string[5];
                int dataIndex = -1;
                bool stringMode = false;

                for (int i = 0; i < dataEntry.Length; i++)
                {
                    if ((!stringMode && (char.IsDigit(dataEntry[i]) || dataEntry[i] == '-' || dataEntry[i] == '.')) ||
                        (stringMode && dataEntry[i] != '\''))
                    {
                        data[dataIndex] += dataEntry[i];
                    }
                    else if (dataEntry[i] == '\'')
                    {
                        stringMode = !stringMode;
                    }
                    else
                    {
                        dataIndex++;
                    }
                }

                BattleMapProp prop = BattleMapProp.Create(data[0], data[1]);
                prop.transform.localPosition = new Vector3(
                    float.Parse(data[2]),
                    float.Parse(data[3]),
                    0f);
                prop.SetVisibleToPlayers(data[4] == "1");
            }
        }
    }

    public void Clear()
    {
        tiles.Clear();
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