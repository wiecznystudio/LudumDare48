using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSpicyMeat : ActionBase {

    [SerializeField]
    private TriggerArea triggerArea;


    private void Start() {
        if(triggerArea == null)
            return;
        triggerArea.enter += OnTriggerAreaEnter;
    }
    public override void OnAction() {

    }
    public override void OnRestart() {

    }

    private void OnTriggerAreaEnter(Collider2D collision) {
        if(collision.GetComponent<PlayerController>() == null)
            return;

        StageController.Restart();
    }


}
