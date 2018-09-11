using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBodyPart : BodyPart
{
    public SpriteRenderer eyeColorSprite;

    public SpriteRenderer hairSprite;
    public ColorPanel hairColorPanel;
    public OutfitCategory hairStyles;

    public SpriteRenderer specialSprite;
    public OutfitCategory specialStyles;

    public void OnHairColorPanelUpdated()
    {
        hairSprite.color = new Color(
            hairColorPanel.redSlider.value,
            hairColorPanel.greenSlider.value,
            hairColorPanel.blueSlider.value
        );
    }
}
