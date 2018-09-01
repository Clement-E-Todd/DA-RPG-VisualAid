using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public BattleMap battleMap;

    public void ToggleBattleMap()
    {
        battleMap.ToggleHidden();
    }
}
