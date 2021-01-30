using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    //public InputController inputController;
    public Image GUIBackground;
    public Text pauseButtonText;
    public Text characterNameInput;
    public Slider mapSizeXInput;
    public Slider mapSizeYInput;

    private ActiveUIWindow _activeUIWindow = ActiveUIWindow.MainMenu;

    private Dictionary<ActiveUIWindow, Color> backgroundColor = new Dictionary<ActiveUIWindow, Color>
    {
        #region [ActiveUIWindow.Window] = Color
        [ActiveUIWindow.MainMenu] = new Color(0 / 255f, 70 / 255f, 90 / 255f, 255 / 255f),
        [ActiveUIWindow.PauseMenu] = new Color(0 / 255f, 0 / 255f, 0 / 255f, 180 / 255f)
        #endregion
    };

    public void changeMenuBackgroundColor(int activeUIWindowInt)
    {
        _activeUIWindow = (ActiveUIWindow)activeUIWindowInt;
        GUIBackground.color = backgroundColor[_activeUIWindow];
    }

    public void onPauseButtonClick()
    {
        GlobalStateManager.Instance.IsGlobalMapPaused = !GlobalStateManager.Instance.IsGlobalMapPaused;
        pauseButtonText.text = GlobalStateManager.Instance.IsGlobalMapPaused ? "|| Continue" : "> Stop";
    }
    public void onNewGameSubmit()
    {
        WorldMapMaganer.Instance.GenerateMap((int)mapSizeXInput.value, (int)mapSizeYInput.value);
        CharacterManager.Instance.SpawnPlayer(characterNameInput.text, true, "Sprites/PortraitSample", new Vector3(0, 0, 0), new Vector3Int(0, 0, 0));
    }

    public void onSaveGameSubmit(string saveName)
    {

    }

    public void onSaveGameSubmit()
    {

    }

}
