using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class InfiniteTerrain : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public List<TileBase> groundTiles;
    public int tileChangeFrequency = 5;
    private int currentTileIndex = 0;
    private List<TileBase> currentSectionTiles;
    public float verticalSpacing = 3f;  // Separaci�n vertical entre filas
    public float scrollSpeed = 2f;  // Velocidad de desplazamiento
    public int tilesPerSection = 20;  // N�mero de tiles por secci�n
    public int maxVisibleColumns = 30;  // M�ximo n�mero de columnas visibles a la vez

    void Start()
    {
        int[,] map = GenerateArray(100, 2, false);  // Ahora creamos el doble de filas
        currentSectionTiles = new List<TileBase>();

        // Llamamos directamente a AddTerrainToMap para las tres filas iniciales
        AddTerrainToMap(map);

        // Llamamos a la funci�n GenerateTerrain cada 2 segundos
        InvokeRepeating("GenerateTerrain", 0f, 2f);
    }

    void Update()
    {
        MoveTerrain();
    }

    void GenerateTerrain()
    {
        int[,] newMap = GenerateArray(20, 2, false);  // Ahora creamos el doble de filas
        AddTerrainToMap(newMap);
    }

    void AddTerrainToMap(int[,] newMap)
    {
        int mapWidth = terrainTilemap.size.x;
        int mapHeight = terrainTilemap.size.y;

        for (int i = 0; i < 3; i++)  // Iteramos tres veces para cada fila
        {
            for (int x = 0; x < newMap.GetUpperBound(0); x++)
            {
                for (int y = 0; y < newMap.GetUpperBound(1); y++)
                {
                    if (newMap[x, y] == 1)
                    {
                        int combinedX = x + mapWidth;
                        TileBase currentTile = GetTile(currentTileIndex);

                        // Calculamos la posici�n vertical ajustada considerando el espaciado
                        float yPos = (i * newMap.GetUpperBound(1) + y) * verticalSpacing;

                        // Colocamos el tile en la posici�n correspondiente
                        terrainTilemap.SetTile(new Vector3Int(combinedX, Mathf.FloorToInt(yPos), 0), currentTile);
                    }
                }
            }

            // Limpiamos la lista para la siguiente secci�n
            currentSectionTiles.Clear();

            // Incrementamos o cambiamos currentTileIndex seg�n sea necesario
            currentTileIndex++;
            if (currentTileIndex >= groundTiles.Count)
            {
                currentTileIndex = 0;
            }
        }
    }

    void MoveTerrain()
    {
        Vector3 currentPosition = terrainTilemap.transform.position;
        currentPosition.x -= scrollSpeed * Time.deltaTime;
        terrainTilemap.transform.position = currentPosition;

        // Eliminar los tiles m�s all� del l�mite
        int firstVisibleColumn = Mathf.FloorToInt(-currentPosition.x / terrainTilemap.cellSize.x);
        int lastColumnToRemove = Mathf.Max(0, firstVisibleColumn - maxVisibleColumns);

        for (int i = lastColumnToRemove; i < firstVisibleColumn; i++)
        {
            for (int j = 0; j < terrainTilemap.size.y; j++)
            {
                terrainTilemap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }

    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }

    public void RenderMap(int[,] map)
    {
        terrainTilemap.ClearAllTiles();

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (map[x, y] == 1)
                {
                    int tileIndex = x / tileChangeFrequency;  // Calcular el �ndice de la baldosa seg�n la frecuencia de cambio
                    TileBase currentTile = GetTile(tileIndex);
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), currentTile);
                }
            }
        }
    }

    TileBase GetTile(int index)
    {
        // Aseg�rate de que la lista de baldosas no est� vac�a
        if (groundTiles.Count == 0)
        {
            Debug.LogError("La lista de baldosas est� vac�a. Asigna baldosas en el Inspector.");
            return null;
        }

        // Ajusta el �ndice para que no exceda el tama�o de la lista de baldosas
        index = Mathf.Clamp(index, 0, groundTiles.Count - 1);

        return groundTiles[index];
    }
}