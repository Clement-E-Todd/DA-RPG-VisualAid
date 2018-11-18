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
    public GameObject clearPanel;

    public Dropdown loadMapDropdown;

    public GameObject showHideButton;
    public GameObject editModeButton;
    public GameObject propModeButton;

    public InputField savePanelMapNameText;

    public GameObject propPanel;
    public GameObject propPanelButton;

    public void TogglePlayerMapButton()
    {
        playerMap.ToggleHidden();
        showPlayerMapText.text = playerMap.hidden ? "Show Map To Players" : "Hide Map From Players";
        showHideButton.GetComponent<Image>().color = playerMap.hidden ? Color.yellow : Color.white;
    }

    public void ShowSavePanelButton()
    {
        map.SetNoMode();

        savePanel.SetActive(true);
        loadPanel.SetActive(false);
        clearPanel.SetActive(false);
    }

    public void SavePanelSaveButton()
    {
        map.Save(savePanelMapNameText.text);
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
        clearPanel.SetActive(false);
    }

    public void LoadPanelLoadButton()
    {
        map.Clear();
        map.Load(loadMapDropdown.captionText.text);
        loadPanel.SetActive(false);
        savePanelMapNameText.text = loadMapDropdown.captionText.text;
    }

    public void LoadPanelCancelButton()
    {
        loadPanel.SetActive(false);
    }

    public void ShowClearPanelButton()
    {
        map.SetNoMode();

        savePanel.SetActive(false);
        loadPanel.SetActive(false);
        clearPanel.SetActive(true);
    }

    public void ClearPanelClearButton()
    {
        map.Clear();
        clearPanel.SetActive(false);
        savePanelMapNameText.text = string.Empty;
    }

    public void ClearPanelCancelButton()
    {
        clearPanel.SetActive(false);
    }

    public void TogglePropPanel()
    {
        propPanel.SetActive(!propPanel.activeSelf);
    }

    public void OnModeSelected()
    {
        editModeButton.GetComponent<Image>().color = map.currentMode == GMBattleMap.Mode.Edit ? Color.cyan : Color.white;
        propModeButton.GetComponent<Image>().color = map.currentMode == GMBattleMap.Mode.Prop ? Color.cyan : Color.white;

        savePanel.SetActive(false);
        loadPanel.SetActive(false);

        propPanel.SetActive(false);
        propPanelButton.SetActive(map.currentMode == GMBattleMap.Mode.Prop);
    }
}
