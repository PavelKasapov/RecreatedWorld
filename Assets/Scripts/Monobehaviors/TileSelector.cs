using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite tileSelectorSprite;
    public Sprite characterSelectorSprite;
    public WorldTile SelectedTile { get; private set; }

    public void SelectTile(Vector3Int gridCoords)
    {
        SelectedTile = WorldMapMaganer.Instance.WorldMap.Find(tile => tile.Coords == gridCoords);
        MoveSelector(SelectedTile.Coords);
    }

    private void MoveSelector(Vector3Int selectedTileCoords)
    {
        Vector3 centerOfSelectedTile = WorldMapMaganer.Instance.grid.GetCellCenterWorld(selectedTileCoords);
        gameObject.transform.position = centerOfSelectedTile;
    }


}
