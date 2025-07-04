using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeScreen;
    // Start is called before the first frame update
    
    public void SelectUpgrade()
    {
        _upgradeScreen.SetActive(false);
    }
}
