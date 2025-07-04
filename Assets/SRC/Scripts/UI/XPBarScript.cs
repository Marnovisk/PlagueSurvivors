using UnityEngine.UI;
using UnityEngine;

public class XPBarScript : MonoBehaviour
{

    public Slider XpBar;
    public float maxXP = 100;
    public float XP;
    public GameObject player;
    private playerExpirience XPScript;
    public GameObject playerHUD;

    public GameObject upgradeHUD;
    // Start is called before the first frame update
    void Start()
    {
        XPScript = player.GetComponent<playerExpirience>();
    }

    // Update is called once per frame
    void Update()
    {
        XP = XPScript.currentExpirience;
        if(XpBar.value != XP)
        {
            XpBar.value = XP;
            XpBar.maxValue = XPScript.NeededExpirience;
        }
        if(XPScript.LevelUped)
        {
            upgradeHUD.gameObject.SetActive(true);
            upgradeHUD.GetComponent<UpgradeScript>().SetUpgrades();
            playerHUD.SetActive(false);
        }      
    }
}