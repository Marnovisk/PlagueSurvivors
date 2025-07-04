using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSScript : MonoBehaviour
{
    public TMP_Text _FpsText;
    private float fps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    
    public void GetFPS()
    {
        fps = (int)(1f /Time.unscaledDeltaTime);
        _FpsText.text = fps.ToString();
    }
}
