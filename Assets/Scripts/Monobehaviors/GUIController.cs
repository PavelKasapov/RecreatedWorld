using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GUIController : MonoBehaviour
{
    public Image GUIBackground;
    public Text pauseButtonText;
    public Text characterNameInput;
    public Slider mapSizeXInput;
    public Slider mapSizeYInput;

    private ActiveUIWindow _activeUIWindow = ActiveUIWindow.MainMenu;
    private GlobalStateManager _globalStateManager;
    private CharacterManager _characterManager;
    private PlayerFactory _playerFactory;
    private WorldMapRenderService _renderService;
    private WorldMapGeneratorService _generatorService;

    [Inject]
    public void Construct(GlobalStateManager globalStateManager, CharacterManager characterManager,
        PlayerFactory playerFactory, WorldMapRenderService renderService, WorldMapGeneratorService generatorService)
    {
        _globalStateManager = globalStateManager;
        _characterManager = characterManager;
        _playerFactory = playerFactory;
        _renderService = renderService;
        _generatorService = generatorService;
    }

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
        _globalStateManager.IsGlobalMapPaused = !_globalStateManager.IsGlobalMapPaused;
        pauseButtonText.text = _globalStateManager.IsGlobalMapPaused ? "|| Continue" : "> Stop";
    }
    public void onNewGameSubmit()
    {
        _generatorService.GenerateMap((int)mapSizeXInput.value, (int)mapSizeYInput.value);
        _renderService.RenderMap();
        Player newPlayer = _playerFactory
            .Create(characterNameInput.text, true, "Sprites/PortraitSample", new Vector3Int(0, 0, 0), new Vector3(0, 0, 0));
        _characterManager.PlayerList.Add(newPlayer);
    }

    public void onSaveGameSubmit(string saveName)
    {

    }

    public void onSaveGameSubmit()
    {

    }

}
