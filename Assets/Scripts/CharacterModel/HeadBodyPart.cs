using UnityEngine;
using UnityEngine.UI;

public class HeadBodyPart : BodyPart
{
    public SpriteRenderer eyeColorSprite;
    public ColorPanel eyeColorPanel;

    public SpriteRenderer hairSprite;
    public ColorPanel hairColorPanel;
    public OutfitCategory hairStyles;
    public Text hairIndexText;

    private Outfit hairStyle
    {
        get
        {
            return hairStyles.selectedIndex >= 0 ? hairStyles.outfits[hairStyles.selectedIndex] : null;
        }
    }

    public SpriteRenderer specialSprite;
    public ColorPanel specialColorPanel;
    public OutfitCategory specialStyles;
    public Text specialIndexText;

    private Outfit specialStyle
    {
        get
        {
            return specialStyles.selectedIndex >= 0 ? specialStyles.outfits[specialStyles.selectedIndex] : null;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        hairIndexText.text = (hairStyles.selectedIndex + 1).ToString();
        OnHairIndexUpdated();

        specialIndexText.text = (specialStyles.selectedIndex + 1).ToString();
        OnSpecialIndexUpdated();
    }

    public void OnEyeColorPanelUpdated()
    {
        eyeColorSprite.color = new Color(
            eyeColorPanel.redSlider.value,
            eyeColorPanel.greenSlider.value,
            eyeColorPanel.blueSlider.value
        );
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

    public void OnSpecialColorPanelUpdated()
    {
        specialSprite.color = new Color(
            specialColorPanel.redSlider.value,
            specialColorPanel.greenSlider.value,
            specialColorPanel.blueSlider.value
        );
    }

    public void IncrementSpecialIndex()
    {
        specialStyles.selectedIndex += 1;
        if (specialStyles.selectedIndex >= specialStyles.outfits.Length)
        {
            specialStyles.selectedIndex -= specialStyles.outfits.Length + 1;
        }
        specialIndexText.text = (specialStyles.selectedIndex + 1).ToString();

        OnSpecialIndexUpdated();
    }

    public void DecrementSpecialIndex()
    {
        specialStyles.selectedIndex -= 1;
        if (specialStyles.selectedIndex < -1)
        {
            specialStyles.selectedIndex += specialStyles.outfits.Length + 1;
        }
        specialIndexText.text = (specialStyles.selectedIndex + 1).ToString();

        OnSpecialIndexUpdated();
    }

    public void OnSpecialIndexUpdated()
    {
        if (specialStyles.outfits.Length > 0 && specialStyles.selectedIndex >= 0)
        {
            if (specialStyle.sprite == null)
            {
                specialStyle.sprite = Resources.Load<Sprite>(specialStyle.spritePath);
            }

            specialSprite.sprite = specialStyle.sprite;
        }
        else
        {
            specialSprite.sprite = null;
        }
    }
}
