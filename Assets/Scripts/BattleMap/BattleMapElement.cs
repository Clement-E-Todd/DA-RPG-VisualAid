using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapElement : MonoBehaviour
{
    private BattleMap map;
    private SpriteRenderer spriteRenderer;

    public float alphaMultiplier = 1.0f;

    float innerFadeDistanceX = 4.25f;
    float outerFadeDistanceX = 4.75f;

    float innerFadeDistanceY = 2.5f;
    float outerFadeDistanceY = 3f;

    private void Update()
    {
        // Try to find the Battle Map that this element belongs to
        if (map == null)
        {
            for (Transform parent = transform.parent; parent != null; parent = parent.parent)
            {
                map = parent.GetComponent<BattleMap>();
                if (map != null)
                {
                    break;
                }
            }
        }

        if (map == null)
        {
            return;
        }

        // If this elemts is too far from the center of the view, fade it out
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            Vector3 mapPos = map.transform.parent.InverseTransformPoint(transform.position);

            float alphaX = 1 - Mathf.Clamp01((Mathf.Abs(mapPos.x) - innerFadeDistanceX) / (outerFadeDistanceX - innerFadeDistanceX));
            float alphaY = 1 - Mathf.Clamp01((Mathf.Abs(mapPos.y) - innerFadeDistanceY) / (outerFadeDistanceY - innerFadeDistanceY));

            Color color = spriteRenderer.color;
            color.a = Mathf.Min(alphaX, alphaY) * alphaMultiplier;
            spriteRenderer.color = color;
        }
    }
}
