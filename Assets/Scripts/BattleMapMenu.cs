using UnityEngine;
using UnityEngine.UI;

public class BattleMapMenu : MonoBehaviour
{
    public BattleMap playerMap;
    public Text showPlayerMapText;

    public void TogglePlayerMapButton()
    {
        playerMap.ToggleHidden();
        showPlayerMapText.text = playerMap.hidden ? "Show Map To Players" : "Hide Map From Players";
    }
}
