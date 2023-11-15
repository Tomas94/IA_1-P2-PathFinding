using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State _currentState = null;

    Dictionary<AgentStates, State> _allStates = new Dictionary<AgentStates, State>();
    
    public void Update()
    {
        _currentState?.OnUpdate();
    }

    public void AddState(AgentStates name, State state)
    {
        if (!_allStates.ContainsKey(name))
            _allStates.Add(name, state);
        else
            _allStates[name] = state;

        state.fsm = this;
    }

    public void ChangeState(AgentStates state)
    {
        _currentState?.OnExit();

        if (_allStates.ContainsKey(state))
            _currentState = _allStates[state];
        _currentState.OnEnter();
        Debug.Log("cambiando a estado " + state);
    }

}
