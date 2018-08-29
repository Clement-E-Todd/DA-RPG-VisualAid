using UnityEngine;
using System.Collections.Generic;

public class GMBattleMap : BattleMap
{
    private Transform cursorTransform;
    private Transform cursorTopTransform;
    private int cursorHeight = 0;

    List<GameObject> cursorColumns = new List<GameObject>();
    private static GameObject cursorColumnPrefab;

    protected override void Awake()
    {
        base.Awake();
        cursorTransform = transform.Find("Cursor");
        cursorTopTransform = cursorTransform.Find("Top");

        if (cursorColumnPrefab == null)
        {
            cursorColumnPrefab = Resources.Load<GameObject>("Prefabs/CursorColumn");
        }
    }

    private void Update()
    {
        UpdateCursorHeight();
        SnapCursorToMouse();
    }

    private void UpdateCursorHeight()
    {
        int previousCursorHeight = cursorHeight;

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
        {
            cursorHeight++;
        }
        else if (Input.GetKeyDown(KeyCode.Minus) && cursorHeight > 0)
        {
            cursorHeight--;
        }

        if (cursorHeight != previousCursorHeight)
        {
            cursorTopTransform.localPosition = new Vector3(
                cursorTopTransform.localPosition.x,
                BattleMapTileView.columnHeight * cursorHeight,
                cursorTopTransform.localPosition.z);

            while (cursorColumns.Count < cursorHeight)
            {
                GameObject newColumn = Instantiate(cursorColumnPrefab);
                newColumn.transform.SetParent(cursorTopTransform);
                newColumn.transform.localPosition = new Vector3(0, BattleMapTileView.columnHeight * -cursorColumns.Count, 0);
                cursorColumns.Add(newColumn);
            }

            while (cursorColumns.Count > cursorHeight)
            {
                Destroy(cursorColumns[cursorColumns.Count - 1].gameObject);
                cursorColumns.RemoveAt(cursorColumns.Count - 1);
            }
        }
    }

    private void SnapCursorToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localMousePosition = transform.InverseTransformPoint(mousePosition);

        int[] hexCoords = GetHexCoordNearPosition(localMousePosition);

        cursorTransform.position = GetLocalHexPosition(hexCoords[0], hexCoords[1]);

        if (Input.GetMouseButton(0))
        {
            if (data.GetTileAt(hexCoords[0], hexCoords[1]) == null)
            {
                BattleMapData.TileData tile = new BattleMapData.TileData(hexCoords[0], hexCoords[1], cursorHeight, BattleMapData.TileData.Type.Grass);
                data.AddTile(tile);
                AddViewForTile(tile);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            BattleMapData.TileData tile = data.GetTileAt(hexCoords[0], hexCoords[1]);

            if (tile != null)
            {
                RemoveViewForTile(tile);
                data.RemoveTileAt(hexCoords[0], hexCoords[1]);
            }
        }
    }
}
