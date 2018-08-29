using UnityEngine;
using System.Collections.Generic;

public class BattleMap : MonoBehaviour
{
    protected BattleMapData data = new BattleMapData();

    protected const float hexOffsetX = 1.5f;
    protected const float hexOffsetY = 1f;

    protected Dictionary<BattleMapData.TileData, BattleMapTileView> tileViews = new Dictionary<BattleMapData.TileData, BattleMapTileView>();

    private static GameObject tileViewPrefab;

    protected virtual void Awake()
    {
        if (tileViewPrefab == null)
        {
            tileViewPrefab = Resources.Load<GameObject>("Prefabs/BattleMapTile");
        }
    }

    protected Vector2 GetLocalHexPosition(int hexX, int hexY)
    {
        return new Vector2(
            hexX * hexOffsetX,
            hexY * hexOffsetY + (hexX % 2 == 0 ? hexOffsetY / 2 : 0f)
        );
    }

    protected int[] GetHexCoordNearPosition(Vector2 position)
    {
        int hexX = Mathf.RoundToInt(position.x / hexOffsetX);

        if (hexX % 2 == 0)
        {
            position.y -= hexOffsetY / 2;
        }

        int hexY = Mathf.RoundToInt(position.y / hexOffsetY);

        return new int[] { hexX, hexY };
    }

    public void AddViewForTile(BattleMapData.TileData tile)
    {
        GameObject tileView = Instantiate(tileViewPrefab);
        tileView.transform.SetParent(transform);
        tileView.transform.localPosition = GetLocalHexPosition(tile.x, tile.y);

        tileViews.Add(tile, tileView.GetComponent<BattleMapTileView>());
    }

    public void RemoveViewForTile(BattleMapData.TileData tile)
    {
        if (tileViews.ContainsKey(tile))
        {
            Destroy(tileViews[tile].gameObject);
            tileViews.Remove(tile);
        }
    }
}
