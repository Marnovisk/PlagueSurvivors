using UnityEngine;
using TMPro;

public class HordeScript : MonoBehaviour
{
    public TMP_Text HordaTotal;
    public TMP_Text HordaAtual;
    public GameObject hordasController;
    private hordeSystem HordaScript;

    // Start is called before the first frame update
    void Start()
    {
        HordaScript = hordasController.GetComponent<hordeSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(HordaTotal.text != HordaScript.totalHorde.ToString())
        // {
        //     HordaTotal.SetText(HordaScript.totalHorde.ToString());
        // }
        
        // if(HordaAtual.text != HordaScript.CurrentHorde.ToString())
        // {
        //     HordaAtual.SetText(HordaScript.CurrentHorde.ToString());
        // }
    }
}
