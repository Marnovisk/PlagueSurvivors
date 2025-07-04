using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
   //Piscar

   [Header("ScriptReferences")]
   private CharacterStatusManager StatusScript;
   private MeshRenderer myRenderer; 

   private void Start()
   {
        if(gameObject.tag != "Enemy")
        {
            Init();
        }
   }

   public void Init()
   {
        StatusScript = GetComponent<CharacterStatusManager>();
        myRenderer = GetComponentInChildren<MeshRenderer>();
        SubscribeToEvent();
   }

   private void SubscribeToEvent()
   {
        //Debug.Log("Se inscrevendo");

        StatusScript.OnTakeDamage += BlinkMAterial;
   }

   private void OnDisable()
   {
        //Debug.Log("Se desinscrevendo");

        StatusScript.OnTakeDamage -= BlinkMAterial;
   }

   private void OnDestroy()
   {
    
   }

   public void BlinkMAterial()
   {
        if(StatusScript == null) 
        { 
            Debug.LogError("MIssingStatusScript");
            return;
        } 
        
        if(myRenderer == null)
        { 
            Debug.LogError("MIssingSRenderer");
            return;
        } 

        StartCoroutine(BlinkMaterialRoutine());    
   }

   IEnumerator BlinkMaterialRoutine()
    {
        var myMaterial = myRenderer.materials[0];
        var myMaterialColor = myMaterial.color;

        myMaterial.color = Color.white;
        yield return new WaitForSeconds(0.2f);

        myMaterial.color = myMaterialColor;
    }

}
