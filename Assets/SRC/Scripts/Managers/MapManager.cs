using UnityEngine;
using Unity.VisualScripting;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planes;
    private Vector3 spawnPosition;
    [SerializeField] private string SpawnDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        if (other.tag == "Player")
        {
            Debug.Log("Player Collide");
            if(SpawnDir == "right")
            {
               spawnPosition = new Vector3(54,-7,0);
            }
            if(SpawnDir == "left")
            {
                spawnPosition = new Vector3(-54,-7,0);
            }
            if(SpawnDir == "up")
            {
                spawnPosition = new Vector3(0,-7,54);
            }
            if(SpawnDir == "down")
            {
                spawnPosition = new Vector3(0,-7,-54);
            }
            var newPlane = Instantiate(planes[0], transform.position + spawnPosition, Quaternion.identity);
            GameObject parent = this.transform.parent.GameObject();
            Destroy(this.gameObject);
        } 

        
    }
}
