using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleMapMenu : MonoBehaviour
{
    public GMBattleMap map;

    public BattleMap playerMap;
    public Text showPlayerMapText;

    public GameObject savePanel;
    public GameObject loadPanel;

    public Dropdown loadMapDropdown;

    public GameObject editModeButton;
    public GameObject propModeButton;
    public GameObject initiativeModeButton;

    public Text savePanelMapNameText;

    public void TogglePlayerMapButton()
    {
        playerMap.ToggleHidden();
        showPlayerMapText.text = playerMap.hidden ? "Show Map To Players" : "Hide Map From Players";
    }

    public void ShowSavePanelButton()
    {
        map.SetNoMode();

        savePanel.SetActive(true);
        loadPanel.SetActive(false);
    }

    public void SavePanelSaveButton()
    {
        map.GetData().Save(savePanelMapNameText.text);
        savePanel.SetActive(false);
    }

    public void SavePanelCancelButton()
    {
        savePanel.SetActive(false);
    }

    public void ShowLoadPanelButton()
    {
        map.SetNoMode();

        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + "/BattleMaps/");
        FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();

        List<string> mapNames = new List<string>();
        for (int i = files.Length - 1; i >= 0; i--)
        {
            mapNames.Add(files[i].Name.Replace(".battleMap", ""));
        }

        loadMapDropdown.ClearOptions();
        loadMapDropdown.AddOptions(mapNames);

        loadPanel.SetActive(true);
        savePanel.SetActive(false);
    }

    public void LoadPanelLoadButton()
    {
        map.GetData().Load(loadMapDropdown.captionText.text);
        map.RefreshTileViews();
        loadPanel.SetActive(false);
    }

    public void LoadPanelCancelButton()
    {
        loadPanel.SetActive(false);
    }

    public void OnModeSelected()
    {
        editModeButton.GetComponent<Image>().color = map.currentMode == GMBattleMap.Mode.Edit ? Color.cyan : Color.white;
        propModeButton.GetComponent<Image>().color = map.currentMode == GMBattleMap.Mode.Prop ? Color.cyan : Color.white;
        initiativeModeButton.GetComponent<Image>().color = map.currentMode == GMBattleMap.Mode.Initiative ? Color.cyan : Color.white;

        savePanel.SetActive(false);
        loadPanel.SetActive(false);
    }
}
