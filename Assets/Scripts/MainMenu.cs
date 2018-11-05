using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GMBattleMap battleMap;
    public CharacterEditor characterEditor;

    public void ToggleBattleMap()
    {
        battleMap.ToggleHidden();

        if (!battleMap.playerMap.hidden)
        {
            battleMap.playerMap.ToggleHidden();
        }
    }

    public void ToggleCharacterEditor()
    {
        characterEditor.gameObject.SetActive(!characterEditor.gameObject.activeSelf);
    }
}
