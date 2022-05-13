using System.Collections;
using System;
using UnityEngine;

public class ChunkFactory : MonoBehaviour
{

    [Header("Components")]
    public Chunk chunkPrototype;
    public Material material;
    public Camera camera;
    public GameObject rock;

    //Dimensions
    public int CHUNK_WIDTH { get; private set; } = 16;
    public int CHUNK_HEIGHT { get; private set; } = 128;

    [Header("Generation Settings")]
    public long seed = 0;
    public float amplification = 10;
    public float heightAmplification = 1;
    public float caveThreshold = -3;
    public float scale = 0.05F;
    public float heightScale = 0.01F;
    public int rockAmount = 50;
    public int rockDeviation = 30;
    private int actualRockAmount = 0;

    public WorldSize worldsize;
    public bool IsGenerated { get; private set; } = false;

    public event Action TheWorldHasFinishedGenerating;
    public enum WorldSize
    {
        Tiny = 4,
        Small = 8,
        Medium = 16,
        Large = 32,
        Huge = 64
    }
    private int worldSizeCalculated;

    //Offsets
    private int offsetX = 0;
    private int offsetY = 0;
    private int offsetZ = 0;

    private int chunkCounterX;
    private int chunkCounterZ;


    private void Start()
    {
        chunkPrototype.material = material;

        chunkPrototype.width = CHUNK_WIDTH;
        chunkPrototype.height = CHUNK_HEIGHT;

        chunkPrototype.seed = UnityEngine.Random.Range(int.MinValue,int.MaxValue);
        chunkPrototype.heightAmplification = heightAmplification;
        chunkPrototype.amplification = amplification;
        chunkPrototype.caveThreshold = caveThreshold;
        chunkPrototype.scale = scale;
        chunkPrototype.heightScale = heightScale;
        actualRockAmount = UnityEngine.Random.Range(rockAmount-30,rockAmount+30);
        worldSizeCalculated = (int)worldsize * CHUNK_WIDTH;

        float halfWorldSize = (worldSizeCalculated - (int)worldsize) * 0.5F;
        camera.transform.position = new Vector3(halfWorldSize, CHUNK_HEIGHT, halfWorldSize);
        camera.orthographicSize = halfWorldSize;

        StartCoroutine(GenerateWorld());
    }

    IEnumerator GenerateWorld()
    {
        while (chunkCounterZ < (int)worldsize)
        {
            chunkPrototype.amplification = amplification;
            chunkPrototype.scale = scale;
            chunkPrototype.heightScale = heightScale;

            Instantiate(chunkPrototype, new Vector3(offsetX - chunkCounterX, offsetY, offsetZ - chunkCounterZ), Quaternion.identity,this.transform);
            chunkCounterX++;

            if (chunkCounterX >= (int)worldsize)
            {
                chunkCounterZ++;
                chunkCounterX = 0;
                offsetZ = chunkCounterZ * CHUNK_WIDTH;
            }
            offsetX = chunkCounterX * CHUNK_WIDTH;
            yield return null;
        }
        TheWorldHasFinishedGenerating?.Invoke();

        for (int i = 0; i < actualRockAmount; i++)
        {

            float x = UnityEngine.Random.Range(0, worldSizeCalculated);
            float z = UnityEngine.Random.Range(0, worldSizeCalculated);

            

            RaycastHit hit;
            if (Physics.Raycast(new Vector3(x, camera.transform.position.y, z), Vector3.down, out hit, CHUNK_HEIGHT))
            {
                GameObject rockInstance = Instantiate(rock, hit.point, Quaternion.identity);
            }

            yield return null;
        }

    }
}
