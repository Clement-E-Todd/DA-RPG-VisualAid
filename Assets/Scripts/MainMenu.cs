using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GMBattleMap battleMap;
    public CharacterEditor characterEditor;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B))
        {
            ToggleBattleMap();
        }
    }

    public void ToggleBattleMap()
    {
        battleMap.ToggleHidden();

        if (!battleMap.hidden)
        {
            battleMap.SetNoMode();
        }

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
