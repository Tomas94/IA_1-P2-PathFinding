using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    FiniteStateMachine _fSM; 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum AgentStates
{
    Patrol,
    Chase,
    Rest
}