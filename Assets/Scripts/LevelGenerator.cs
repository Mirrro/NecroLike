using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGenerator
{
    private static GameObject[] tilePrefabs;
    public static void Init(GameObject[] tilePrefabs)
    {
        LevelGenerator.tilePrefabs = tilePrefabs;
    }
    public static void GenerateLevel(Transform level)
    {
        int width = 10;
        int[] heightMap = new int[width];
        for(int x = width; x--!=0;)
            heightMap[x] = Random.Range((-x + width) * (x) / (width / 2), ((-x + width) * (x) / (width / 2)) * 2);

        for (int x = width; x-- != 0;)
        {
            for (int y = heightMap[x]; y-- != 0;)
                CreateTile(heightMap, x, y, level);
        }
    }

    private static void CreateTile(int[] heightMap, int x, int y, Transform level)
    {
        float tileSize = 1.6f;
        float offset;
        if (x % 2 > 0)
            offset = 0.5f;
        else
            offset = 0;
        int height = heightMap[x];
        GameObject tilePrefab = tilePrefabs[0];
        float yOffset = 0;
        /* FIX THIS CHECK FOR BORDER TILES FOR BEACH !!!*/
        if (x == 0 || x == heightMap.Length - 1 || y == 0 || y == height - 1)
        {
            tilePrefab = tilePrefabs[1];
            yOffset = -0.5f;
        }
        else if(x>0 && heightMap[x-1] / 2 < y-1)
        {
            tilePrefab = tilePrefabs[1];
            yOffset = -0.5f;
        }
        else if(x<heightMap.Length && heightMap[x+1]/2<y-1)
        {
            tilePrefab = tilePrefabs[1];
            yOffset = -0.5f;
        }

        Object.Instantiate(tilePrefab, new Vector3(x * tileSize, yOffset, (y + offset - height / 2) * tileSize), Quaternion.identity, level);
    }
}

