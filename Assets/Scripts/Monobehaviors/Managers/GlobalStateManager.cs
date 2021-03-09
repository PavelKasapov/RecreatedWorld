using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public Player ControlledPlayer { get; set; }
    public bool IsMenuOpened { get; set; } = true;
    public bool IsGlobalMapPaused { get; set; } = true;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
