using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGate : ActionBase {

    [SerializeField]
    private Animator animator;

    private bool open = false;

    public override void OnRestart() {
        open = false;
        animator.SetTrigger("close");
    }

    public override void OnAction() {
        if(open) {
            animator.SetTrigger("close");
        } else {
            animator.SetTrigger("open");
        }
        open = !open;
    }

}
