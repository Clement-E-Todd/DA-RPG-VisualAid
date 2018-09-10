using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BodyPart : MonoBehaviour
{
    public SpriteRenderer mainSprite;
    public SpriteRenderer outfitSprite;

    public ColorPanel outfitColorPanel;
    public Dropdown outfitCategoryDropdown;
    public Text outfitIndexText;

    [System.Serializable]
    public class Outfit
    {
        public string spritePath;
        public Vector2 position;
        public Sprite sprite { get; set; }
    }

    [System.Serializable]
    public class OutfitCategory
    {
        public string name;
        public Outfit[] outfits;
        public int selectedIndex { get; set; }
    }

    public OutfitCategory[] outfitCategories;

    private OutfitCategory outfitCategory
    {
        get
        {
            return outfitCategories[outfitCategoryIndex];
        }
    }

    private Outfit outfit
    {
        get
        {
            return outfitCategory.outfits[outfitCategory.selectedIndex];
        }
    }

    [SerializeField]
    private int outfitCategoryIndex;

    private void Awake()
    {
        mainSprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        outfitSprite = transform.Find("Outfit").GetComponent<SpriteRenderer>();

        outfitCategoryDropdown.options = new List<Dropdown.OptionData>();
        outfitCategoryDropdown.options.Add(new Dropdown.OptionData("None"));
        foreach (OutfitCategory category in outfitCategories)
        {
            outfitCategoryDropdown.options.Add(new Dropdown.OptionData(category.name));
        }
        outfitCategoryDropdown.value = outfitCategoryDropdown.options.Count > 1 ? 1 : 0;

        OnOutfitCategoryDropdownUpdated();
        OnOutfitColorPanelUpdated();
    }

    public void OnOutfitCategoryDropdownUpdated()
    {
        outfitCategoryIndex = outfitCategoryDropdown.value - 1;

        if (outfitCategoryIndex >= 0 && outfitCategory.outfits.Length > 0)
        {
            outfitIndexText.text = (outfitCategory.selectedIndex + 1).ToString();
        }
        else
        {
            outfitIndexText.text = "-";
        }

        OnOutfitIndexUIUpdated();
    }

    public void IncrementOutfitIndex()
    {
        if (outfitCategoryIndex >= 0 && outfitCategory.outfits.Length > 0)
        {
            outfitCategory.selectedIndex += 1;
            if (outfitCategory.selectedIndex >= outfitCategory.outfits.Length)
            {
                outfitCategory.selectedIndex -= outfitCategory.outfits.Length;
            }
            outfitIndexText.text = (outfitCategory.selectedIndex + 1).ToString();
        }
        else
        {
            outfitIndexText.text = "-";
        }

        OnOutfitIndexUIUpdated();
    }

    public void DecrementOutfitIndex()
    {
        if (outfitCategoryIndex >= 0 && outfitCategory.outfits.Length > 0)
        {
            outfitCategory.selectedIndex -= 1;
            if (outfitCategory.selectedIndex < 0)
            {
                outfitCategory.selectedIndex += outfitCategory.outfits.Length;
            }
            outfitIndexText.text = (outfitCategory.selectedIndex + 1).ToString();
        }
        else
        {
            outfitIndexText.text = "-";
        }

        OnOutfitIndexUIUpdated();
    }

    public void OnOutfitIndexUIUpdated()
    {
        if (outfitCategoryIndex >= 0 && outfitCategory.outfits.Length > 0)
        {
            if (outfit.sprite == null)
            {
                outfit.sprite = Resources.Load<Sprite>(outfit.spritePath);
            }

            outfitSprite.sprite = outfit.sprite;
        }
        else
        {
            outfitSprite.sprite = null;
        }
    }

    public void OnOutfitColorPanelUpdated()
    {
        outfitSprite.color = new Color(outfitColorPanel.redSlider.value, outfitColorPanel.greenSlider.value, outfitColorPanel.blueSlider.value);
    }
}
