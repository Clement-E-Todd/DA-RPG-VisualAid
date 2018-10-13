using UnityEngine;
using System.Collections.Generic;

public class BattleMap : MonoBehaviour
{
    protected BattleMapData data = new BattleMapData();

    protected const float hexOffsetX = 1.4f;
    protected const float hexOffsetY = 0.825f;

    protected Dictionary<BattleMapData.TileData, BattleMapTileView> tileViews = new Dictionary<BattleMapData.TileData, BattleMapTileView>();

    private static GameObject tileViewPrefab;

    public bool hidden = true;

    protected virtual void Awake()
    {
        if (tileViewPrefab == null)
        {
            tileViewPrefab = Resources.Load<GameObject>("Prefabs/BattleMapTile");
        }
    }

    protected virtual void Update()
    {
        Vector3 destination = new Vector3(
            hidden ? 20 : 0,
            transform.parent.position.y,
            transform.parent.position.z
        );

        if (Mathf.Abs(destination.x - transform.parent.position.x) > 0.01)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, destination, Time.deltaTime * 4f);
        }
        else
        {
            transform.parent.position = destination;
        }

        if (hidden && transform.parent.position.x > 19)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void ToggleHidden()
    {
        hidden = !hidden;

        if (!hidden && !transform.parent.gameObject.activeSelf)
        {
            transform.parent.gameObject.SetActive(true);
        }
    }

    protected Vector2 GetLocalHexPosition(int hexX, int hexY)
    {
        return new Vector2(
            hexX * hexOffsetX,
            hexY * hexOffsetY + (hexX % 2 == 0 ? hexOffsetY / 2 : 0f)
        );
    }

    protected int[] GetHexCoordNearPosition(Vector2 localPosition)
    {
        int hexX = Mathf.RoundToInt(localPosition.x / hexOffsetX);

        if (hexX % 2 == 0)
        {
            localPosition.y -= hexOffsetY / 2;
        }

        int hexY = Mathf.RoundToInt(localPosition.y / hexOffsetY);

        return new int[] { hexX, hexY };
    }

    public void AddViewForTile(BattleMapData.TileData tile)
    {
        GameObject tileViewObject = Instantiate(tileViewPrefab);
        Helpers.SetLayerRecursively(tileViewObject, gameObject.layer);

        tileViewObject.transform.SetParent(transform);
        tileViewObject.transform.localPosition = GetLocalHexPosition(tile.x, tile.y);
        tileViewObject.transform.localScale = Vector3.one;

        BattleMapTileView tileView = tileViewObject.GetComponent<BattleMapTileView>();
        tileView.ShowData(tile);

        tileViews.Add(tile, tileView);
    }

    public void RemoveViewForTile(BattleMapData.TileData tile)
    {
        if (tileViews.ContainsKey(tile))
        {
            Destroy(tileViews[tile].gameObject);
            tileViews.Remove(tile);
        }
    }

    public void RefreshTileViews()
    {
        // Remove all existing views
        foreach (BattleMapTileView view in tileViews.Values)
        {
            Destroy(view.gameObject);
        }
        tileViews.Clear();

        // Add a view for each tile in the map's data
        BattleMapData.TileData[] allTileData = data.GetAllTileData();
        foreach (BattleMapData.TileData tileData in allTileData)
        {
            AddViewForTile(tileData);
        }
    }
}
