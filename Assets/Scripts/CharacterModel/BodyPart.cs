using UnityEngine;
using UnityEngine.UI;

public class BodyPart : MonoBehaviour
{
    public SpriteRenderer mainSprite;
    public SpriteRenderer outfitSprite;

    public ColorPanel outfitColorPanel;

    private void Awake()
    {
        mainSprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        outfitSprite = transform.Find("Outfit").GetComponent<SpriteRenderer>();

        OnOutfitColorPanelUpdated();
    }

    public void OnOutfitColorPanelUpdated()
    {
        outfitSprite.color = new Color(outfitColorPanel.redSlider.value, outfitColorPanel.greenSlider.value, outfitColorPanel.blueSlider.value);
    }
}
