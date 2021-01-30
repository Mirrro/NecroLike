using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class LevelGenerator
{
    public static Vector3 Origin = new Vector3(-20, 2 , 0);
        
    private static GameObject[] tilePrefabs;
    private static GameObject[] enemyPrefabs;
    public static void Init(GameObject[] tilePrefabs, GameObject[] enemyPrefabs)
    {
        LevelGenerator.tilePrefabs = tilePrefabs;
        LevelGenerator.enemyPrefabs = enemyPrefabs;
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
        level.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private static void CreateTile(int[] heightMap, int x, int y, Transform level)
    {
        float tileSize = 3.2f;
        float offset;
        if (x % 2 > 0)
            offset = 0.5f;
        else
            offset = 0;
        int height = heightMap[x];
        GameObject tilePrefab = tilePrefabs[0];
        float yOffset = -0;
        if (x == 0 || x == heightMap.Length - 1 || y == 0 || y == height - 1)
        {
            tilePrefab = tilePrefabs[1];
            yOffset = -0.5f;
        }
        /* FIX THIS CHECK FOR BORDER TILES FOR BEACH !!!
        else if (x>0 && heightMap[x-1] / 2 < y-1)
        {
            tilePrefab = tilePrefabs[1];
            yOffset = -0.5f;
        }
        else if(x<heightMap.Length && heightMap[x+1]/2<y-1)
        {
            tilePrefab = tilePrefabs[1];
            yOffset = -0.5f;
        }
        */
        Vector3 pos = new Vector3(x * tileSize, yOffset, (y + offset - height / 2) * tileSize) + Origin;
        Object.Instantiate(tilePrefab, pos, Quaternion.identity, level);

        int type = Random.Range(0, 15);
        if(type<enemyPrefabs.Length)
            Object.Instantiate(enemyPrefabs[type], pos + Vector3.up*2.5f , Quaternion.identity, level);

    }
}

