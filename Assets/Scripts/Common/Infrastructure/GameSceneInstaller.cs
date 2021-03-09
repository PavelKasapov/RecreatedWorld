using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public CharacterManager characterManager;
    public GlobalStateManager globalStateManager;
    public WorldMapMaganer worldMapMaganer;
    public GameObject CharacterPrefab;
    public Grid worldGrid;
    public Tilemap groundTileMap;
    public TileSelector tileSelector;

    public override void InstallBindings()
    {
        Container.Bind<CharacterManager>().FromInstance(characterManager).AsSingle();
        Container.Bind<GlobalStateManager>().FromInstance(globalStateManager).AsSingle();
        Container.Bind<WorldMapMaganer>().FromInstance(worldMapMaganer).AsSingle();
        Container.Bind<Grid>().FromInstance(worldGrid).AsSingle();
        Container.Bind<Tilemap>().FromInstance(groundTileMap).AsSingle();
        Container.Bind<TileSelector>().FromInstance(tileSelector).AsSingle();
        Container.Bind<WorldMapGeneratorService>().AsSingle();
        Container.Bind<WorldMapRenderService>().AsSingle();
        Container.Bind<SaveService>().AsSingle();
        Container.Bind<PlayerFactory>().AsSingle();
        Container.Bind<PathfinderService>().AsSingle();
    }
}
