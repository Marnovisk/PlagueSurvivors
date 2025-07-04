using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusHUD : MonoBehaviour
{
    //private CharacterStatusManager playerInfo;
    //public Scrollbar scrollbar;
    [SerializeField] private GameObject _GameOverScreen;

    private float pLife;
    
    // Start is called before the first frame update
    void Start()
    {
        //playerInfo = GetComponent<CharacterStatusManager>();
        //scrollbar.size = 1; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            HudLife(15);
        }
    }

    public void OnDeath()
    {
        _GameOverScreen.SetActive(true);
        Time.timeScale = 0;
        //this.gameObject.SetActive(false);
    }

    public void UpdateHudLife(float LifeValue)
    {
        pLife = LifeValue * 0.01f;
        //scrollbar.size = pLife;
        Debug.Log(pLife);
    }

    public void HudLife(float LifeValue)
    {
        //scrollbar.size = 0.05f;        
    }
}