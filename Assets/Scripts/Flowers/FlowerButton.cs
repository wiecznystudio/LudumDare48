using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerButton : FlowerBase {

    [SerializeField]
    private ActionBase action;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float buttonTime;

    private bool used = false;

    protected override void OnInit() {

    }
    protected override void OnUpdate() {

    }
    public override void OnRestart() {
        animator.SetTrigger("disable");
        used = false;
    }

    public override void OnAttack() {
        StageController.Restart();
    }
    public override void OnUse(PlayerController player) {
        if(used)
            return;

        animator.SetTrigger("active");
        action?.OnAction();       
        used = true;
        AudioManager.instance.PlaySfx("Click1");
        StartCoroutine(ButtonTimer());
    }

    private IEnumerator ButtonTimer() {
        yield return new WaitForSeconds(buttonTime);
        animator.SetTrigger("disable");
        action?.OnAction();
        AudioManager.instance.PlaySfx("Click2");
        used = false;
    }



}


