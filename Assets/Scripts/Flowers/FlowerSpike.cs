using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpike : FlowerBase {

    [SerializeField]
    private Transform gfx;

    protected override void OnInit() {

    }
    protected override void OnUpdate() {

    }
    public override void OnRestart() {
        gfx.gameObject.SetActive(true);
    }

    public override void OnAttack() {
        gfx.gameObject.SetActive(false);
    }
    public override void OnTriggerAreaEnter(PlayerController player) {
        StageController.Restart();
    }
}
