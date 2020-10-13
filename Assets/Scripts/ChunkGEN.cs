using UnityEngine;

public class ChunkGEN : MonoBehaviour
{
    [SerializeField] private GameObject[] chunks;
    [SerializeField] private float chunkSize;
    [SerializeField] private int mapLenght;

    private GameObject lastChunk;

    public static ChunkGEN instance;


    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        for (int i = 0; i < mapLenght; i++)
        {
            
            GameObject obj = (GameObject)Instantiate(chunks[0]);

            obj.transform.position = new Vector3(0, 0, i * chunkSize);
            obj.transform.parent = transform;
            lastChunk = obj;
        }
    }

    public void SetLastChunk(Chunk chunk, Vector3 newPos)
    {
        lastChunk = chunk.gameObject;
        lastChunk.transform.position = newPos;
    }

    public GameObject GetLastChunk()
    {
        return lastChunk;
    }
    public float GetChunkSize()
    {
        return chunkSize;
    }

}
