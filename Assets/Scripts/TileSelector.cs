using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public WorldTile SelectedTile { get; private set; }
    private Grid _grid;
    void Awake()
    {
        _grid = WorldMapMaganer.Instance.GetComponent<Grid>();
    }

    public void SelectTile(Vector2 mousePos)
    {
        Vector3 worldCoord = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int gridCoord = _grid.WorldToCell(worldCoord);
        SelectedTile = WorldMapMaganer.Instance.WorldMap.Find(tile => tile.Coords == gridCoord);
        MoveSelector(SelectedTile.Coords);
    }

    private void MoveSelector(Vector3Int selectedTileCoords)
    {
        Vector3 centerOfSelectedTile = _grid.GetCellCenterWorld(selectedTileCoords);
        gameObject.transform.position = centerOfSelectedTile;
    }


}
