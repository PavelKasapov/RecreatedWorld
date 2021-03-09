using UnityEngine;
using Zenject;

public class TileSelector : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite tileSelectorSprite;
    public Sprite characterSelectorSprite;
    public WorldTile SelectedTile { get; private set; }

    private WorldMapMaganer _worldMapManager;
    private Grid _worldGrid;

    [Inject]
    public void Construct(WorldMapMaganer worldMapMaganer, Grid worldGrid)
    {
        _worldMapManager = worldMapMaganer;
        _worldGrid = worldGrid;
    }
    public void SelectTile(Vector3Int gridCoords)
    {
        SelectedTile = _worldMapManager.WorldMap.Find(tile => tile.Coords == gridCoords);
        Vector3 centerOfSelectedTile = _worldGrid.GetCellCenterWorld(SelectedTile.Coords);
        gameObject.transform.position = centerOfSelectedTile;
        transform.parent = null;
        spriteRenderer.sprite = tileSelectorSprite;
    }
    
    public void SelectPlayer(Player player)
    {
        transform.parent = player.transform;
        transform.position = player.transform.position;
        spriteRenderer.sprite = characterSelectorSprite;
    }

    private void MoveSelector(Vector3Int selectedTileCoords)
    {
        Vector3 centerOfSelectedTile = _worldGrid.GetCellCenterWorld(selectedTileCoords);
        gameObject.transform.position = centerOfSelectedTile;
    }


}
