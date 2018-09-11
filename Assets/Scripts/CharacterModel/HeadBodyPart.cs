using UnityEngine;
using UnityEngine.UI;

public class HeadBodyPart : BodyPart
{
    public SpriteRenderer hairSprite;
    public ColorPanel hairColorPanel;
    public OutfitCategory hairStyles;
    public Text hairIndexText;

    public SpriteRenderer eyeColorSprite;
    public ColorPanel eyeColorPanel;

    public SpriteRenderer specialSprite;
    public OutfitCategory specialStyles;

    private Outfit hairStyle
    {
        get
        {
            return hairStyles.selectedIndex >= 0 ? hairStyles.outfits[hairStyles.selectedIndex] : null;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        hairIndexText.text = (hairStyles.selectedIndex + 1).ToString();
        OnHairIndexUpdated();
    }

    public void OnHairColorPanelUpdated()
    {
        hairSprite.color = new Color(
            hairColorPanel.redSlider.value,
            hairColorPanel.greenSlider.value,
            hairColorPanel.blueSlider.value
        );
    }

    public void IncrementHairIndex()
    {
        hairStyles.selectedIndex += 1;
        if (hairStyles.selectedIndex >= hairStyles.outfits.Length)
        {
            hairStyles.selectedIndex -= hairStyles.outfits.Length + 1;
        }
        hairIndexText.text = (hairStyles.selectedIndex + 1).ToString();

        OnHairIndexUpdated();
    }

    public void DecrementHairIndex()
    {
        hairStyles.selectedIndex -= 1;
        if (hairStyles.selectedIndex < -1)
        {
            hairStyles.selectedIndex += hairStyles.outfits.Length + 1;
        }
        hairIndexText.text = (hairStyles.selectedIndex + 1).ToString();

        OnHairIndexUpdated();
    }

    public void OnHairIndexUpdated()
    {
        if (hairStyles.outfits.Length > 0 && hairStyles.selectedIndex >= 0)
        {
            if (hairStyle.sprite == null)
            {
                hairStyle.sprite = Resources.Load<Sprite>(hairStyle.spritePath);
            }

            hairSprite.sprite = hairStyle.sprite;
        }
        else
        {
            hairSprite.sprite = null;
        }
    }

    public void OnEyeColorPanelUpdated()
    {
        eyeColorSprite.color = new Color(
            eyeColorPanel.redSlider.value,
            eyeColorPanel.greenSlider.value,
            eyeColorPanel.blueSlider.value
        );
    }
}
