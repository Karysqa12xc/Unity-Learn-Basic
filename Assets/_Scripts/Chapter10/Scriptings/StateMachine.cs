using System;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.BehaviorSimulationAndAi
{

    public class StateMachine
    {
        public class State
        {
            public string name;
            public Action onFrame;
            public Action onEnter;
            public Action onExit;
            public override string ToString()
            {
                return name;
            }
        }
        Dictionary<string, State> states = new Dictionary<string, State>();
        public State currentStates{get; private set;}
        public State initState;
        public State CreateState(string name)   
        {
            var newState = new State();
            newState.name = name;
            if(states.Count == 0){
                initState = newState;
            }
            states[name] = newState;
            return newState;
        }
        public void Update()
        {   
            if(states.Count == 0 || initState == null){
                Debug.LogFormat("State machine has no states!");
                return;
            }
            if(currentStates == null){
                TransitionTo(initState);
            }
            if(currentStates.onFrame != null){
                currentStates.onFrame();
            }
        }
        public void TransitionTo(State newState){
            if(newState == null){
                Debug.LogErrorFormat("Cannot transition to a null state!");
                return;
            }
            if(currentStates != null && currentStates.onExit != null){
                currentStates.onExit();
            }
            Debug.LogFormat("Transition from '{0}' to '{1}'", currentStates, newState);
            currentStates = newState;
            if(newState.onEnter != null){
                newState.onEnter(); 
            }
        }
        public void TransitionTo(string name){
            if(states.ContainsKey(name)== false){
                Debug.LogErrorFormat("State machine doesn't contain a state" + "named {0}", name);
                return;
            }
            var newState = states[name];
            TransitionTo(newState);
        }
    }
}
