using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
public class UpgradeScript : MonoBehaviour
{
    public List<Button> BTNList;
    public GameObject playerHUD;
    public GameObject player;
    private int[] _upgradePick = new int[3];
    private playerExpirience XPScript;
    public List<Upgradecriptable> Upgrades;

    public WeaponKind _upgradeWeapon;

    
    
    // Start is called before the first frame update
    void Start()
    {
        XPScript = player.GetComponent<playerExpirience>();
        this.gameObject.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpgrades()
    {
        for (int i = 0; i < BTNList.Count; i++)
        {
            if (Upgrades.Count > 0)
            {
                _upgradePick[i] = (Random.Range(0, Upgrades.Count));             
                BTNList[i].GetComponentInChildren<TMP_Text>().text = Upgrades[_upgradePick[i]].description;
            }
            else
            {
                BTNList[i].GetComponentInChildren<TMP_Text>().text = "Todos Armas desbloqueadas";
            }

            }
        }

    public void FirstUpgrade()
    {
        Debug.Log("UP Apllied");
        Time.timeScale = 1;
        this.gameObject.SetActive(false);

        if (Upgrades.Count > 0) //(upgradePick < Upgrades.Count)
        {
            Upgrades[_upgradePick[0]].ApplyUpgrade(player);
            Debug.Log($"Upgrade aplicado: {Upgrades[_upgradePick[0]].description}");
            Upgrades.Remove(Upgrades[_upgradePick[0]]);            
        }

        playerHUD.SetActive(true);
        //XPScript.LevelUped = false;
        
    }

    public void SecondUpgrade()
    {
        Debug.Log("UP Apllied");
        Time.timeScale = 1;
        this.gameObject.SetActive(false);

        if (Upgrades.Count > 0) //(upgradePick < Upgrades.Count)
        {
            Upgrades[_upgradePick[1]].ApplyUpgrade(player);
            Debug.Log($"Upgrade aplicado: {Upgrades[_upgradePick[1]].description}");
            Upgrades.Remove(Upgrades[_upgradePick[1]]);

        }

        playerHUD.SetActive(true);
        //XPScript.LevelUped = false;

    }

    public void ThirdUpgrade()
    {
        Debug.Log("UP Apllied");
        Time.timeScale = 1;
        this.gameObject.SetActive(false);

        if (Upgrades.Count > 0) //(upgradePick < Upgrades.Count)
        {
            Upgrades[_upgradePick[2]].ApplyUpgrade(player);
            Debug.Log($"Upgrade aplicado: {Upgrades[_upgradePick[2]].description}");
            Upgrades.Remove(Upgrades[_upgradePick[2]]);

        }

        playerHUD.SetActive(true);
        //XPScript.LevelUped = false;

    }
}
