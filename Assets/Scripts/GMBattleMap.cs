using UnityEngine;

public class GMBattleMap : MonoBehaviour
{
    private Transform cursorTransform;

    const float hexOffsetX = 1.5f;
    const float hexOffsetY = 1f;

    private void Awake()
    {
        cursorTransform = transform.Find("EditCursor");
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localMousePosition = transform.InverseTransformPoint(mousePosition);

        int[] hexCoords = GetHexCoordNearPosition(localMousePosition);

        cursorTransform.position = GetLocalHexPosition(hexCoords[0], hexCoords[1]);
    }

    private Vector2 GetLocalHexPosition(int hexX, int hexY)
    {
        return new Vector2(
            hexX * hexOffsetX,
            hexY * hexOffsetY + (hexX % 2 == 0 ? hexOffsetY / 2 : 0f)
        );
    }

    private int[] GetHexCoordNearPosition(Vector2 position)
    {
        int hexX = Mathf.RoundToInt(position.x / hexOffsetX);

        if (hexX % 2 == 0)
        {
            position.y -= hexOffsetY / 2;
        }

        int hexY = Mathf.RoundToInt(position.y / hexOffsetY);

        return new int[] { hexX, hexY };
    }
}
