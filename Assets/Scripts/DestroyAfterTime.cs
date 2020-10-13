using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float time;

    private void Start()
    {
        Destroy(gameObject, time);
    }
}
