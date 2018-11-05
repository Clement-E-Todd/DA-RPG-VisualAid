using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapProp : MonoBehaviour
{
    public static BattleMapProp selectedProp = null;

    Vector3 propDragStartPos;
    Vector3 mouseDragStartPos;

    SpriteRenderer _spriteRenderer;
    SpriteRenderer spriteRenderer
    {
        get
        {
            if (!_spriteRenderer)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            return _spriteRenderer;
        }
    }

    private void OnMouseDown()
    {
        if (GMBattleMap.currentInstance.currentMode != GMBattleMap.Mode.Prop || spriteRenderer.color.a < 0.25f)
        {
            return;
        }

        selectedProp = this;
        propDragStartPos = transform.position;
        mouseDragStartPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (GMBattleMap.currentInstance.currentMode != GMBattleMap.Mode.Prop || selectedProp != this)
        {
            return;
        }
        transform.position = propDragStartPos + (Input.mousePosition - mouseDragStartPos) / 96f;
    }
}
