using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    [Header("MainData")]
    [SerializeField] private EnemyScriptable brain;
    [SerializeField] private GameObject XPPrefab;

    [Header("MainData")]
    [SerializeField] private Transform playerTransform;

    [Header("TransformData")]
    [SerializeField] private Transform GFXTransform;


    [Header("Scripts References")]
    private IAStates IAStatesScript;
    private IAMovement IAMovimentScript;
    private IAKombat IAKombatScript;
    private CharacterStatusManager IAStatusManager;
    private DamageHandler IADamegeHandler;

    [Header("References Check")]
    [SerializeField] private bool referencesOK;
    
    private void Start()
    {
              
    }

    public void Init(EnemyScriptable pBrain)
    {
        referencesOK = false;

        IAStatesScript = GetComponent<IAStates>();
        IAMovimentScript = GetComponent<IAMovement>();
        IAKombatScript = GetComponent<IAKombat>();
        IAStatusManager = GetComponent<CharacterStatusManager>();
        IADamegeHandler = GetComponent<DamageHandler>();

        brain = pBrain;
        
        IAKombatScript.Init(brain);
        IAMovimentScript.Init(brain);
        IAStatusManager.InitStatus(brain.Status);

        InstantiateGraphics(); 
        FindPlayerReference();

        IADamegeHandler.Init();

        referencesOK = true;
    }

    private void Update()
    {
        if (referencesOK == false) return;
        if(playerTransform == null) return;

        if(IAStatesScript.States == IAStateType.CHASING)
        {
            ChaseBehavior();
            return;
        }

        if(IAStatesScript.States == IAStateType.ATTACKING)
        {
            AttackBehavior();
            return;
        }
    }

    void ChaseBehavior()
    {
        if (playerTransform == null) return;
        if (IAMovimentScript == null) return;


        var sucess = IAMovimentScript.Chase(playerTransform);

        if(sucess == false)
        {
            IAStatesScript.ChangeToState(IAStateType.ATTACKING);
        }

    }

    void AttackBehavior()
    {
        var sucess = IAKombatScript.checkAndAttack(playerTransform);

        if(sucess == false)
        {
            IAStatesScript.ChangeToState(IAStateType.CHASING);
        }
    }

    void InstantiateGraphics()
    {
        var gfx = Instantiate(brain.GFX, GFXTransform);
        gfx.transform.parent = this.transform; 
    }

    void FindPlayerReference()
    {
        var playerReference = GameObject.FindGameObjectWithTag("Player");

        if(playerReference == null) return;

        playerTransform = playerReference.transform;
    }

    public void IADeath()
    {
        Instantiate(XPPrefab, transform.position, Quaternion.identity);
    }
}
