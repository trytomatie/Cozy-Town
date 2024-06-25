using UnityEngine;

public class House : MonoBehaviour
{
    public GameObject bunnyPrefab;
    public GameObject bunnyReference;
    public Transform spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       bunnyReference = Instantiate(bunnyPrefab, spawnPosition.position, Quaternion.identity);
    }

    public void OnDeletion()
    {
        Destroy(bunnyReference);
    }
}
