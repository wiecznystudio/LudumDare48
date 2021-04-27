using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBambus : FlowerBase {
    [SerializeField] private new ParticleSystem particleSystem;

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
        particleSystem.Play();
        AudioManager.instance.PlaySfx("Liœcie");
    }
    public override void OnTriggerAreaExit(PlayerController player) {
        particleSystem.Stop();
        AudioManager.instance.PlaySfx("Liœcie2");
    }
    public override void OnTriggerAreaStay(PlayerController player) {
        player.Speed(0.2f, 0.1f);
    }

}
