using UnityEngine;

public class BattleMapProp : MonoBehaviour
{
    public static BattleMapProp selectedProp = null;

    GameObject dummyProp;

    public bool visibleToPlayers { get; private set; }

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

    BattleMapElement _battleMapElement;
    BattleMapElement battleMapElement
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

    private void Start()
    {
        dummyProp = new GameObject(name);
        dummyProp.transform.SetParent(GMBattleMap.currentInstance.playerMap.transform);
        dummyProp.layer = GMBattleMap.currentInstance.playerMap.gameObject.layer;

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

        BattleMapPropPanel propPanel = FindObjectOfType<BattleMapPropPanel>();
        if (propPanel)
        {
            propPanel.OnPropSelected();
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
        SyncDummyToProp();
    }

    public void SetVisibleToPlayers(bool visible)
    {
        visibleToPlayers = visible;
        battleMapElement.alphaMultiplier = visible ? 1f : 0.65f;
        SyncDummyToProp();
    }
}
