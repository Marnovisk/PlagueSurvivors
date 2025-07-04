using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void ReturnToGame()
    {
        player.GetComponent<PlayerScript>().PauseManager();
        this.gameObject.SetActive(false);
    }
}
