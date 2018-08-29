using UnityEngine;

public class GMBattleMap : BattleMap
{
    private Transform cursorTransform;

    protected override void Awake()
    {
        base.Awake();
        cursorTransform = transform.Find("EditCursor");
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localMousePosition = transform.InverseTransformPoint(mousePosition);

        int[] hexCoords = GetHexCoordNearPosition(localMousePosition);

        cursorTransform.position = GetLocalHexPosition(hexCoords[0], hexCoords[1]);

        if (Input.GetMouseButton(0))
        {
            if (data.GetTileAt(hexCoords[0], hexCoords[1]) == null)
            {
                BattleMapData.TileData tile = new BattleMapData.TileData(hexCoords[0], hexCoords[1]);
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
