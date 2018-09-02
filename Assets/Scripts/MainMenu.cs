using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public BattleMap battleMap;
    public CharacterEditor characterEditor;

    public void ToggleBattleMap()
    {
        battleMap.ToggleHidden();
    }

    public void ToggleCharacterEditor()
    {
        characterEditor.gameObject.SetActive(!characterEditor.gameObject.activeSelf);
    }
}
