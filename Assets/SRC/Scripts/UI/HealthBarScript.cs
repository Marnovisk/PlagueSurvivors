using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider HealthBar;
    public float maxHealth = 100;
    public float Health;
    public GameObject player;
    public GameObject DeathScreen;
    // Start is called before the first frame update
    void Start()
    {
        DeathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Health = player.GetComponent<CharacterStatusManager>().status.Health;
        if(HealthBar.value != Health)
        {
            HealthBar.value = Health;
        }

        if(HealthBar.value <= 0)
        {
            DeathScreen.SetActive(true);
            Destroy(player);
        }
        
    }
}
