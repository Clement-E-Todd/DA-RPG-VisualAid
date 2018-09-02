using UnityEngine;

public class BattleMapTileView : MonoBehaviour
{
    public const float columnHeight = 0.5f;

    private static GameObject columnPrefab;

    private Sprite topSprite;
    private Sprite columnSprite;

    public void ShowData(BattleMapData.TileData data)
    {
        if (columnPrefab == null)
        {
            columnPrefab = Resources.Load<GameObject>("Prefabs/TileColumn");
        }

        LoadSpritesForTile(data.tileType, data.slope);

        Transform topTransform = transform.Find("Top");
        topTransform.localPosition = new Vector3(0, columnHeight * data.height, 0);

        int sortingOrder = -(data.y * 2) + (data.x % 2 == 0 ? -1 : 0);

        SpriteRenderer topRenderer = topTransform.GetComponent<SpriteRenderer>();
        topRenderer.sprite = topSprite;
        topRenderer.sortingOrder = sortingOrder;

        for (int i = 0; i < data.height; i++)
        {
            GameObject newColumn = Instantiate(columnPrefab);
            Helpers.SetLayerRecursively(newColumn, gameObject.layer);

            newColumn.transform.SetParent(topTransform);
            newColumn.transform.localPosition = new Vector3(0, columnHeight * -i, 0);
            newColumn.transform.localScale = Vector3.one;

            SpriteRenderer columnRenderer = newColumn.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            columnRenderer.sprite = columnSprite;
            columnRenderer.sortingOrder = sortingOrder;
        }
    }

    private void LoadSpritesForTile(BattleMapData.TileData.Type tileType, int slope)
    {
        switch (tileType)
        {
            case BattleMapData.TileData.Type.Grass:
                topSprite = Resources.Load<Sprite>("Sprites/BattleMapTiles/Hex" + slope + "_Grass");
                columnSprite = Resources.Load<Sprite>("Sprites/BattleMapTiles/Column_Dirt");
                break;

            case BattleMapData.TileData.Type.Dirt:
                topSprite = Resources.Load<Sprite>("Sprites/BattleMapTiles/Hex" + slope + "_Dirt");
                columnSprite = Resources.Load<Sprite>("Sprites/BattleMapTiles/Column_Dirt");
                break;
        }
    }
}
