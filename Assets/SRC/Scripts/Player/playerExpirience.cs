using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerExpirience : MonoBehaviour
{
    public PlayerWeapons WeaponsScript;
    public int NeededExpirience;
    public int currentExpirience;
    [SerializeField] private GameObject _upgradeScreen;

    public TMP_Text XPtext;
    
    public bool LevelUped;

    public void Awake()
    {
        WeaponsScript = GetComponent<PlayerWeapons>();
    }

    public void LevelUp()
    {
        currentExpirience = 0;
        NeededExpirience += 10;

        //WeaponsScript.AddWeapon();
    }

    public void IncreaseXp(int amount)
    {
        currentExpirience += amount;
        Debug.Log("XP Apllied");

        if (currentExpirience >= NeededExpirience)
        {
            Debug.Log("Has to LevelUP");
            _upgradeScreen.SetActive(true);
            Time.timeScale = 0;
            _upgradeScreen.GetComponent<UpgradeScript>().SetUpgrades();
            currentExpirience = 0;
        }

        UpdateXPHud();
    }

    public void UpdateXPHud()
    {
        XPtext.text = currentExpirience.ToString();
    }
}
