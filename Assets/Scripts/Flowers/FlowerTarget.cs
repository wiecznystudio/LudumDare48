using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTarget : FlowerBase {

    [SerializeField]
    private ActionBase action;

    private bool active = false;

    protected override void OnInit() {

    }
    protected override void OnUpdate() {

    }
    public override void OnRestart() {
        active = false;
    }


    public override void OnAttack() {
        if(active)
            return;
        active = true;
        action?.OnAction();
    }

}
