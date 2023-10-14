using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GABFERR.statemachine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        STATE_ONE, STATE_TWO, STATE_THREE
    }
    public StateMachine<ExampleEnum> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.STATE_ONE, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.STATE_TWO, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.STATE_THREE, new StateBase());
    }
}
