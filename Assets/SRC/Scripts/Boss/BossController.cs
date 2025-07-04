using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("MainData")]
    [SerializeField] private EnemyScriptable brain;
    //[SerializeField] private GameObject XPPrefab;

    [Header("MainData")]
    [SerializeField] private Transform playerTransform;


    [Header("Scripts References")]
    private IAStates IAStatesScript;
    private BossMovement BossMovimentScript;
    private BossKombat BossKombatScript;
    private CharacterStatusManager BossStatusManager;

    [Header("References Check")]
    [SerializeField] private bool referencesOK;

    public void Init(EnemyScriptable pBrain)
    {
        referencesOK = false;

        IAStatesScript = GetComponent<IAStates>();
        BossMovimentScript = GetComponent<BossMovement>();
        BossKombatScript = GetComponent<BossKombat>();
        BossStatusManager = GetComponent<CharacterStatusManager>();

        brain = pBrain;

        BossKombatScript.Init(brain);
        BossMovimentScript.Init(brain);
        BossStatusManager.InitStatus(brain.Status);

        FindPlayerReference();
        
        referencesOK = true;
    }
    void Update()
    {
        if (referencesOK == false || playerTransform == null) return;
        

        switch (IAStatesScript.States)
        {
            case IAStateType.CHASING:
                MoveBehavior();
                break;

            case IAStateType.ATTACKING:
                AttackBehavior();
                break;
        }
    }

    void MoveBehavior()
    {
        var sucess = BossMovimentScript.BossMove(playerTransform);

        if(sucess == false)
        {
            IAStatesScript.ChangeToState(IAStateType.ATTACKING);
        }

    }

    void AttackBehavior()
    {
        var sucess = BossKombatScript.checkAndAttack(playerTransform);

        if(sucess == false)
        {
            IAStatesScript.ChangeToState(IAStateType.CHASING);
        }
    }

    void FindPlayerReference()
    {
        var playerReference = GameObject.FindGameObjectWithTag("Player");

        if(playerReference == null) return;

        playerTransform = playerReference.transform;
    }
}
