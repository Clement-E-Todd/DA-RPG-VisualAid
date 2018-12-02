using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class BattleMapPropPanel : MonoBehaviour
{
    public Text selectedPropNameText;
    public Toggle visibleToggle;

    public Dropdown addMenuCategoryDropdown;
    public Dropdown addMenuPropDropdown;

    static Dictionary<string, Dictionary<string, string>> _addMenuOptions;
    static Dictionary<string, Dictionary<string, string>> addMenuOptions
    {
        get
        {
            if (_addMenuOptions == null)
            {
                _addMenuOptions = new Dictionary<string, Dictionary<string, string>>();

                Dictionary<string, string> playerCharacters = new Dictionary<string, string>();
                playerCharacters.Add("Margherita", "Sprites/PropsAndCharacters/Player Characters/Margherita");
                playerCharacters.Add("Eden-Amari", "Sprites/PropsAndCharacters/Player Characters/Eden-Amari");
                playerCharacters.Add("MT", "Sprites/PropsAndCharacters/Player Characters/MT");
                playerCharacters.Add("Albert-Alexandre", "Sprites/PropsAndCharacters/Player Characters/Albert-Alexandre");
                playerCharacters.Add("Albert-Alexandre (No Mask)", "Sprites/PropsAndCharacters/Player Characters/Albert-Alexandre (No Mask)");
                playerCharacters.Add("TEST", "Sprites/PropsAndCharacters/Player Characters/TEST");
                _addMenuOptions.Add("Player Characters", playerCharacters);

                Dictionary<string, string> qunari = new Dictionary<string, string>();
                qunari.Add("Qunari (Male)", "Sprites/PropsAndCharacters/Qunari/Qunari-Male");
                qunari.Add("Qunari (Female)", "Sprites/PropsAndCharacters/Qunari/Qunari-Female");
                qunari.Add("Saarebas", "Sprites/PropsAndCharacters/Qunari/Qunari-Saarebas");
                _addMenuOptions.Add("Qunari", qunari);

                Dictionary<string, string> clanCispia = new Dictionary<string, string>();
                clanCispia.Add("Caenum (Cloak)", "Sprites/PropsAndCharacters/Cispia/Caenum-Cloaked");
                _addMenuOptions.Add("Clan Cispia", clanCispia);

                Dictionary<string, string> markings = new Dictionary<string, string>();
                markings.Add("X", "Sprites/PropsAndCharacters/Markings/X");
                markings.Add("Circle", "Sprites/PropsAndCharacters/Markings/circle");
                markings.Add("Exclamation Mark", "Sprites/PropsAndCharacters/Markings/exclamationMark");
                markings.Add("Question Mark", "Sprites/PropsAndCharacters/Markings/questionMark");
                markings.Add("Arrow (Up)", "Sprites/PropsAndCharacters/Markings/arrowUp");
                markings.Add("Arrow (Down)", "Sprites/PropsAndCharacters/Markings/arrowDown");
                markings.Add("Arrow (Left)", "Sprites/PropsAndCharacters/Markings/arrowLeft");
                markings.Add("Arrow (Right)", "Sprites/PropsAndCharacters/Markings/arrowRight");
                _addMenuOptions.Add("Markings", markings);

                Dictionary<string, string> chess = new Dictionary<string, string>();
                chess.Add("Pawn", "Sprites/PropsAndCharacters/Chess/pawn");
                chess.Add("King", "Sprites/PropsAndCharacters/Chess/king");
                chess.Add("Queen", "Sprites/PropsAndCharacters/Chess/queen");
                chess.Add("Knight", "Sprites/PropsAndCharacters/Chess/knight");
                chess.Add("Rook", "Sprites/PropsAndCharacters/Chess/rook");
                chess.Add("Bishop", "Sprites/PropsAndCharacters/Chess/bishop");
                _addMenuOptions.Add("Chess", chess);
            }

            return _addMenuOptions;
        }
    }

    private void Awake()
    {
        addMenuCategoryDropdown.ClearOptions();
        addMenuCategoryDropdown.AddOptions(addMenuOptions.Keys.ToArray().ToList());
        OnAddMenuCategoryChanged();
    }

    public void OnAddMenuCategoryChanged()
    {
        addMenuPropDropdown.ClearOptions();
        addMenuPropDropdown.AddOptions(addMenuOptions[addMenuCategoryDropdown.captionText.text].Keys.ToArray().ToList());
        addMenuPropDropdown.value = 0;
    }

    public void OnAddButtonPressed()
    {
        string propName = addMenuPropDropdown.captionText.text;
        string spritePath = addMenuOptions[addMenuCategoryDropdown.captionText.text][addMenuPropDropdown.captionText.text];

        BattleMapProp prop = BattleMapProp.Create(propName, spritePath);

        BattleMapProp.selectedProp = prop;
        OnPropSelected();
    }

    public void OnDeleteButtonPressed()
    {
        if (BattleMapProp.selectedProp)
        {
            Destroy(BattleMapProp.selectedProp.gameObject);
            BattleMapProp.selectedProp = null;
            OnPropSelected();
        }
    }

    public void OnPropSelected()
    {
        if (!BattleMapProp.selectedProp)
        {
            selectedPropNameText.text = "NONE";
            return;
        }

        selectedPropNameText.text = BattleMapProp.selectedProp.name;
        visibleToggle.isOn = BattleMapProp.selectedProp.visibleToPlayers;
    }

    public void OnVisibleToggled()
    {
        if (BattleMapProp.selectedProp)
        {
            BattleMapProp.selectedProp.SetVisibleToPlayers(visibleToggle.isOn);
        }
    }
}
