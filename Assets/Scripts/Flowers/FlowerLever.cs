using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerLever : FlowerBase {

    [SerializeField]
    private ActionBase action;
    [SerializeField]
    private Transform leverTransform;
    [SerializeField]
    private Vector3[] leverPositions = new Vector3[2];
    [SerializeField]
    private Vector3[] leverRotations = new Vector3[2];

    private bool used = false;

    protected override void OnInit() {

    }
    protected override void OnUpdate() {

    }
    public override void OnRestart() {
        ChangeTransform(0);
        used = false;
    }

    public override void OnAttack() {
        StageController.Restart();
    }
    public override void OnUse(PlayerController player) {
        AudioManager.instance.PlaySfx("Click3");
        if(used) {
            ChangeTransform(0);
            action?.OnAction();
        } else {
            ChangeTransform(1);
            action?.OnAction();
        }
        used = !used;
    }

    private void ChangeTransform(int state) {
        leverTransform.localPosition = leverPositions[state];
        leverTransform.localRotation = Quaternion.Euler(leverRotations[state]);
    }

}


