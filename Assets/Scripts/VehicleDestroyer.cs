using UnityEngine;

public class VehicleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            Destroy(other.gameObject);
        }
    }
}
