using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStates : MonoBehaviour
{
    public IAStateType States;

    public void ChangeToState(IAStateType state)
    {
        if (States == state) return;

        //Animas de troca de estado
        //Coisas referente a troca de estado
        States = state;
    }
}
