using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    public bool showFoldalt;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FSMExample fsm = (FSMExample)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if(fsm.stateMachine.CurrentState != null) { 
            EditorGUILayout.LabelField("Current State:", fsm.stateMachine.CurrentState.ToString());
            
        }

        showFoldalt = EditorGUILayout.Foldout(showFoldalt, "Available States");
        
        if(showFoldalt)
        {   
            if(fsm.stateMachine.dictionaryState.Keys != null) { 
            var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
            var vals = fsm.stateMachine.dictionaryState.Values.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
            }
            }
        }
    }
}
