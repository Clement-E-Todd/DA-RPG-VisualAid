﻿using UnityEngine;
using System.Collections.Generic;

public class GMBattleMap : BattleMap
{
    private Transform cursorTransform;
    private Transform cursorTopTransform;
    private int cursorHeight = 1;
    private int cursorSlopeIndex = 0;
    private BattleMapData.TileData.Type cursorTileType = BattleMapData.TileData.Type.Grass;
    private bool firstUpdateComplete = false;

    List<GameObject> cursorColumns = new List<GameObject>();
    private static GameObject cursorColumnPrefab;

    GameObject[] topSpriteObjects;

    protected override void Awake()
    {
        base.Awake();
        cursorTransform = transform.Find("Cursor");
        cursorTopTransform = cursorTransform.Find("Top");

        topSpriteObjects = new GameObject[] {
            cursorTopTransform.Find("Flat").gameObject,
            cursorTopTransform.Find("Up").gameObject,
            cursorTopTransform.Find("Up-Right").gameObject,
            cursorTopTransform.Find("Down-Right").gameObject,
            cursorTopTransform.Find("Down").gameObject,
            cursorTopTransform.Find("Down-Left").gameObject,
            cursorTopTransform.Find("Up-Left").gameObject,
            cursorTopTransform.Find("Obstacle").gameObject
        };

        if (cursorColumnPrefab == null)
        {
            cursorColumnPrefab = Resources.Load<GameObject>("Prefabs/CursorColumn");
        }
    }

    private void Update()
    {
        UpdateCursorSettings();
        SnapCursorToMouse();
        firstUpdateComplete = true;
    }

    private void UpdateCursorSettings()
    {
        // Press the plus or minus keys to change the cursor height
        int previousCursorHeight = cursorHeight;

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
        {
            cursorHeight++;
        }
        else if (Input.GetKeyDown(KeyCode.Minus) && cursorHeight > 0)
        {
            cursorHeight--;
        }

        if (cursorHeight != previousCursorHeight || !firstUpdateComplete)
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

        // Use letter keys to change the cursor slope
        int previousTopSpriteIndex = cursorSlopeIndex;
        if (Input.GetKeyDown(KeyCode.P))
        {
            cursorSlopeIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            cursorSlopeIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            cursorSlopeIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            cursorSlopeIndex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            cursorSlopeIndex = 4;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            cursorSlopeIndex = 5;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            cursorSlopeIndex = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            cursorSlopeIndex = 7;
        }

        if (cursorSlopeIndex != previousTopSpriteIndex || !firstUpdateComplete)
        {
            topSpriteObjects[previousTopSpriteIndex].SetActive(false);
            topSpriteObjects[cursorSlopeIndex].SetActive(true);
        }

        // Change the tile type with the number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cursorTileType = BattleMapData.TileData.Type.Grass;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cursorTileType = BattleMapData.TileData.Type.Dirt;
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
                BattleMapData.TileData tile = new BattleMapData.TileData(hexCoords[0], hexCoords[1], cursorHeight, cursorTileType, cursorSlopeIndex);
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
