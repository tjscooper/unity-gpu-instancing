using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Terrain_DirtGenerator : MonoBehaviour
{
    [SerializeField] BitmapToPrefab bitmap = null;

    public Mesh terrainMesh;
    public Material terrainMaterial;

    private List<List<TerrainData>> batches = new List<List<TerrainData>>();

    GameObject objToSpawnTopLeft;
    GameObject objToSpawnTopRight;
    GameObject objToSpawnBottomLeft;
    GameObject objToSpawnBottomRight;

    void Start()
    {
        int gridSize = bitmap.map.width;
        int objectsPerBlock = 4;
        int batchIndexNum = 0;
        List<TerrainData> currBatch = new List<TerrainData>();

        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.sharedMesh = null;
        meshc.sharedMesh = terrainMesh;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                AddObj(currBatch, x, y);
                batchIndexNum += objectsPerBlock;
                if (batchIndexNum >= 1000)
                {
                    batches.Add(currBatch);
                    currBatch = BuildNewBatch();
                    batchIndexNum = 0;
                }
            }
        }
    }

    private void AddObj(List<TerrainData> currBatch, int x, int y)
    {
        Color pixelColor = bitmap.map.GetPixel(x, y);
        float positionOffset = 0.25f;
        float scaleValueXY = 0.5f;
        float scaleValueZ = 0.5f;

        // Dirt
        if (bitmap.TerrainColor.Equals(pixelColor))
        {
            // Add game objects with collider and tag
            Vector3 topLeftPosition = new Vector3(x - positionOffset, y + positionOffset, 0);
            objToSpawnTopLeft = new GameObject("Dirt");
            objToSpawnTopLeft.tag = "Dirt";
            objToSpawnTopLeft.transform.position = topLeftPosition;
            objToSpawnTopLeft.AddComponent<BoxCollider>();
            currBatch.Add(new TerrainData(topLeftPosition, new Vector3(scaleValueXY, scaleValueXY, scaleValueZ), Quaternion.identity));

            Vector3 topRightPosition = new Vector3(x + positionOffset, y + positionOffset, 0);
            objToSpawnTopRight = new GameObject("Dirt");
            objToSpawnTopRight.tag = "Dirt";
            objToSpawnTopRight.transform.position = topRightPosition;
            objToSpawnTopRight.AddComponent<BoxCollider>();
            currBatch.Add(new TerrainData(topRightPosition, new Vector3(scaleValueXY, scaleValueXY, scaleValueZ), Quaternion.identity));

            Vector3 bottomLeftPosition = new Vector3(x - positionOffset, y - positionOffset, 0);
            objToSpawnBottomLeft = new GameObject("Dirt");
            objToSpawnBottomLeft.tag = "Dirt";
            objToSpawnBottomLeft.transform.position = bottomLeftPosition;
            objToSpawnBottomLeft.AddComponent<BoxCollider>();
            currBatch.Add(new TerrainData(bottomLeftPosition, new Vector3(scaleValueXY, scaleValueXY, scaleValueZ), Quaternion.identity));

            Vector3 bottomRightPosition = new Vector3(x + positionOffset, y - positionOffset, 0);
            objToSpawnBottomRight = new GameObject("Dirt");
            objToSpawnBottomRight.tag = "Dirt";
            objToSpawnBottomRight.transform.position = bottomRightPosition;
            objToSpawnBottomRight.AddComponent<BoxCollider>();
            currBatch.Add(new TerrainData(bottomRightPosition, new Vector3(scaleValueXY, scaleValueXY, scaleValueZ), Quaternion.identity));
        }

    }

    private List<TerrainData> BuildNewBatch()
    {
        return new List<TerrainData>();
    }

    void Update()
    {
        RenderBatches();
    }

    private void RenderBatches()
    {
        foreach (var batch in batches)
        {
            Graphics.DrawMeshInstanced(terrainMesh, 0, terrainMaterial, batch.Select((a) => a.matrix).ToList());
        }
    }
}
