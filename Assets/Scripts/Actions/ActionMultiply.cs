using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMultiply : ActionBase {

    [SerializeField]
    private ActionBase[] actions;

    public override void OnAction() {
        for(int i = 0; i < actions.Length; i++) {
            actions[i].OnAction();
        }
    }
    public override void OnRestart() {

    }

}
