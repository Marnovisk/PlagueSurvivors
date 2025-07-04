using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStatusManager : MonoBehaviour, IDamagable
{
    public Status status;
    public event Action OnTakeDamage;
    private PlayerStatusHUD statusHUD;

    private void Start()
    {
        if(this.gameObject.tag == "Player")
            statusHUD = GetComponent<PlayerStatusHUD>();
    }

    public void InitStatus(Status pStatus)
    {
        status.Health = pStatus.Health;
        status.Armor = pStatus.Armor;
        status.MagicResist = pStatus.MagicResist;
    }

    private void Update()   
    {
        // if(Input.GetKeyDown(KeyCode.T))
        // {
        //     TakeDamage(Random.Range(1,5));
        // }
    }
    public void TakeDamage(int amount)
    {
        status.Health -= amount;

        OnTakeDamage?.Invoke();

        //Anima Dano
        //Sangue
        //Num Dnao

        if(status.Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(this.gameObject.tag == "Player")
        {
            statusHUD.OnDeath();
            
        }
        else
        {
            GetComponent<IAController>().IADeath();
            Destroy(this.gameObject);
        }
    }
}
