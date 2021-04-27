using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerLight : FlowerBase {

    [SerializeField]
    private new Light light;
    [SerializeField]
    private MeshRenderer lightOrb;
    [SerializeField]
    private Vector2 timeRange;
    [SerializeField]
    private Material[] materials = new Material[2];

    private bool isActive = false;

    protected override void OnInit() {

    }
    protected override void OnUpdate() {

    }
    public override void OnRestart() {

    }

    public override void OnAttack() {
        LightOn();
    }
    public override void OnTriggerAreaEnter(PlayerController player) {
        LightOn();       
    }

    private void LightOn() {
        if (isActive) { return; }
        AudioManager.instance.PlaySfx("Lamp");
        lightOrb.material = materials[1];
        light.enabled = true;
        isActive = true;
    }
}
