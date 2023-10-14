using System.Collections.Generic;


namespace GABFERR.statemachine
{
public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionaryState;
    private StateBase _currentState;
    public float timeToStartGame = 1f;

    public StateBase CurrentState
    {
        get { return _currentState; }
    }
    
        public void Init()
    {
         dictionaryState = new Dictionary<T, StateBase>();
        }
    public void RegisterStates(T typeEnum,StateBase state)
    { 
        dictionaryState.Add(typeEnum, state);
    }
    public void SwitchStates(T state, params object[] objs)
    {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryState[state];
        _currentState.OnStateEnter(objs);
    }
    public void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }
    }
}