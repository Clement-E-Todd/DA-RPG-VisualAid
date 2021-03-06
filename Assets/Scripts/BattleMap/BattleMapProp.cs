﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BattleMapElement))]
public class BattleMapProp : MonoBehaviour
{
    public static BattleMapProp selectedProp = null;

    GameObject dummyProp;

    public bool visibleToPlayers { get; private set; }

    Vector3 propDragStartPos;
    Vector3 mouseDragStartPos;

    public string spritePath;

    const int DEFAULT_SORT_ORDER = 30000;

    SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer
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

    BattleMapElement _battleMapElement;
    public BattleMapElement battleMapElement
    {
        get
        {
            if (!_battleMapElement)
            {
                _battleMapElement = GetComponent<BattleMapElement>();
            }

            return _battleMapElement;
        }
    }

    static GameObject _propPrefab;
    public static GameObject propPrefab
    {
        get
        {
            if (!_propPrefab)
            {
                _propPrefab = Resources.Load<GameObject>("Prefabs/Prop");
            }

            return _propPrefab;
        }
    }

    public static BattleMapProp Create(string propName, string spritePath)
    {
        GameObject propObject = Instantiate(propPrefab);
        propObject.name = propName;

        BattleMapProp prop = propObject.GetComponent<BattleMapProp>();
        prop.transform.SetParent(GMBattleMap.currentInstance.transform);
        prop.transform.localScale = Vector3.one;

        prop.spritePath = spritePath;
        prop.spriteRenderer.sprite = Resources.Load<Sprite>(spritePath);

        return prop;
    }

    private void Start()
    {
        dummyProp = new GameObject(name);
        dummyProp.transform.SetParent(GMBattleMap.currentInstance.playerMap.transform);
        dummyProp.layer = GMBattleMap.currentInstance.playerMap.gameObject.layer;

        int order = Mathf.Clamp(DEFAULT_SORT_ORDER - (int)(transform.localPosition.y * 10f), 27233, 32767);
        spriteRenderer.sortingOrder = order;

        dummyProp.AddComponent<SpriteRenderer>();
        dummyProp.AddComponent<BattleMapElement>();

        SyncDummyToProp();
    }

    private void OnDestroy()
    {
        Destroy(dummyProp);
    }

    private void SyncDummyToProp()
    {
        if (dummyProp)
        {
            dummyProp.transform.localPosition = transform.localPosition;
            dummyProp.transform.localRotation = transform.localRotation;
            dummyProp.transform.localScale = transform.localScale;

            SpriteRenderer dummyRenderer = dummyProp.GetComponent<SpriteRenderer>();
            dummyRenderer.sprite = spriteRenderer.sprite;
            dummyRenderer.color = spriteRenderer.color;
            dummyRenderer.sortingOrder = spriteRenderer.sortingOrder;

            BattleMapElement dummyMapElement = dummyProp.GetComponent<BattleMapElement>();
            dummyMapElement.alphaMultiplier = battleMapElement.alphaMultiplier;

            dummyProp.SetActive(visibleToPlayers);
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

        BattleMapPropPanel propPanel = FindObjectOfType<BattleMapPropPanel>();
        if (propPanel)
        {
            propPanel.OnPropSelected();
        }
    }

    private void OnMouseDrag()
    {
        if (GMBattleMap.currentInstance.currentMode != GMBattleMap.Mode.Prop || selectedProp != this)
        {
            return;
        }
        transform.position = propDragStartPos + (Input.mousePosition - mouseDragStartPos) / 96f;

        int order = Mathf.Clamp(DEFAULT_SORT_ORDER - (int)(transform.localPosition.y * 10f), 27233, 32767);
        spriteRenderer.sortingOrder = order;

        SyncDummyToProp();
    }

    public void SetVisibleToPlayers(bool visible)
    {
        visibleToPlayers = visible;
        battleMapElement.alphaMultiplier = visible ? 1f : 0.65f;
        SyncDummyToProp();
    }
}
