using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemy;

    [SerializeField] private float minWait;
    [SerializeField] private float maxWait;

    [SerializeField] private float wait;
    [SerializeField] private float pushForce;
    
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitUntil(() => GameManager.instance.State == GameState.Playing);
        while (GameManager.instance.State == GameState.Playing)
        {
            wait = Random.Range(minWait, maxWait);
            int randomCar = Random.Range(0, enemy.Length);
            int randomX = Random.Range(-1, 2);

            GameObject e = Instantiate(enemy[randomCar]);
            if (e.name.Contains("Car") || e.name.Contains("Policecar"))
            {
                e.transform.position = new Vector3(randomX*3, 0.475f, transform.position.z);
            }
                
            else if (e.name.Contains("Bus"))
            { 
                e.transform.position = new Vector3(randomX * 3, 1.167f, transform.position.z); 
            }
            else if (e.name.Contains("Truck_2"))
            {
                e.transform.position = new Vector3(randomX * 3, 1.45f, transform.position.z);
            }
            else
            {
                e.transform.position = new Vector3(randomX * 3, 1.643f, transform.position.z);
            }
            e.transform.rotation = Quaternion.Euler(0, 90, 0);
            e.GetComponent<Rigidbody>().AddForce(Vector3.back * pushForce);
            yield return new WaitForSeconds(wait);
        }
    }

}
