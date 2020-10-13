using UnityEngine;

public class Chunk : MonoBehaviour
{

    void FixedUpdate()
    {
        if (GameManager.instance.State == GameState.Playing)
            transform.Translate(ChunkMAN.instance.GetDirection() * ChunkMAN.instance.GetSpeed() * Time.deltaTime);
        else
            transform.Translate(ChunkMAN.instance.GetDirection() * ChunkMAN.instance.GetSpeed() / 5 * Time.deltaTime);

        if (transform.position.z < ChunkMAN.instance.GetRenderDistance())
            ChunkMAN.instance.DestroyChunk(this);
    }

}
