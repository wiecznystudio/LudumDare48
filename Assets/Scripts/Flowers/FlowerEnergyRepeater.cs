using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnergyRepeater : FlowerEnergyBase {

    [Header("Flower Energy Repeater")]

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

        energyUseLight.intensity -= 5f * Time.deltaTime;
    }
    public override void OnRestart() {
        EnergyDisable();
        StopAllCoroutines();
    }

    protected override void EnergyActive() {
        if(isActive)
            return;

        lightOrb.material = activeMaterial;
        light.color = activeColor;
        energyUseLight.intensity = 10f;
        energyUseLight.enabled = true;
        energyUseArea.enabled = true;
        isActive = true;
        particles.Play();
        AudioManager.instance.PlaySfx("Electricity");
        StartCoroutine(ActiveEnergyTimer());
    }
    protected override void EnergyDisable() {
        base.EnergyDisable();
        isActive = false;
        energyUseArea.enabled = false;
        energyUseLight.enabled = false;
        lightOrb.material = disableMaterial;
        light.color = disableColor;
    }

    IEnumerator ActiveEnergyTimer() {
        yield return new WaitForSeconds(2f);
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


