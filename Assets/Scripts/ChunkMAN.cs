using UnityEngine;

public class ChunkMAN : MonoBehaviour
{
    [SerializeField] private float renderDistance;
    [SerializeField] private Vector3 direction = Vector3.back;
    [SerializeField] private float speed;

    public static ChunkMAN instance;

    void Awake()
    {
        instance = this;
    }

    public float GetSpeed ()
    {
        return speed;
    }

    public void DestroyChunk(Chunk chunk)
    {
        Vector3 newPos = ChunkGEN.instance.GetLastChunk().transform.position;
        newPos.z += ChunkGEN.instance.GetChunkSize();

        ChunkGEN.instance.SetLastChunk(chunk, newPos);
        
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
    public float GetRenderDistance()
    {
        return renderDistance;
    }

}
