using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnergySource : FlowerBase {

    [Header("Flower Energy Source")]

    [SerializeField]
    private MeshRenderer lightOrb;
    [SerializeField]
    private Light energyUseLight;
    [SerializeField]
    private Collider2D energyUseArea;
    [SerializeField]
    private Material disableMaterial;
    [SerializeField]
    private Material activeMaterial;

    [SerializeField]
    private TriggerArea activeTriggerArea;
    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private new Light light;
    [SerializeField]
    private Color disableColor;
    [SerializeField]
    private Color activeColor;

    private bool isActive = false;

    protected override void OnInit() {
        activeTriggerArea.enter += OnActiveTriggerEnter;
    }
    protected override void OnUpdate() {
        if(!isActive)
            return;

        energyUseLight.intensity -= (10f * Time.deltaTime);
    }
    public override void OnRestart() {
        EnergyDisable();
        StopAllCoroutines();
    }

    public override void OnAttack() {
        EnergyActive();
    }
    public override void OnUse(PlayerController player) {
        EnergyActive();
    }
    private void EnergyActive() {
        if(isActive)
            return;

        lightOrb.material = activeMaterial;
        light.color = activeColor;
        energyUseLight.intensity = 10f;
        energyUseLight.enabled = true;
        energyUseArea.enabled = true;
        particles.Play();
        isActive = true;
        AudioManager.instance.PlaySfx("Electricity");
        StartCoroutine(ActiveEnergyTimer());
    }
    private void EnergyDisable() {
        isActive = false;
        energyUseArea.enabled = false;
        energyUseLight.intensity = 0;
        energyUseLight.enabled = false;
        lightOrb.material = disableMaterial;
        light.color = disableColor;
    }


    IEnumerator ActiveEnergyTimer() {
        yield return new WaitForSeconds(1f);
        EnergyDisable();
    }


    private void OnActiveTriggerEnter(Collider2D collider) {
        FlowerEnergyBase flowerEnergy = collider.GetComponent<FlowerEnergyBase>();
        if(flowerEnergy == null)
            return;
        StartCoroutine(ActiveEnergyOnFlower(flowerEnergy));
    }

    IEnumerator ActiveEnergyOnFlower(FlowerEnergyBase flowerEnergy) {
        yield return new WaitForSeconds(0.5f);
        flowerEnergy.OnEnergyActive(flowerEnergy);
    }

}


