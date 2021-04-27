using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBridge : ActionBase {
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float waitTime;

    private bool open = false;

    public override void OnRestart() {
        open = false;
        animator.SetTrigger("close");
    }

    public override void OnAction() {
        if(open)
            return;

        animator.SetTrigger("open");
        open = true;
        StartCoroutine(CloseBridge());
    }

    private IEnumerator CloseBridge() {
        yield return new WaitForSeconds(waitTime);
        animator.SetTrigger("close");
        open = false;
    }
}
