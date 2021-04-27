using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnergyReceiver : FlowerEnergyBase {

    [Header("Flower Energy Receiver")]

    [SerializeField]
    private ActionBase activeAction;
    [SerializeField]
    private ActionBase disableAction;

    [SerializeField]
    private MeshRenderer lightOrb;
    [SerializeField]
    private Material disableMaterial;
    [SerializeField]
    private Material activeMaterial;

    [SerializeField]
    private new Light light;

    [SerializeField]
    private Color disableColor;
    [SerializeField]
    private Color activeColor;

    protected override void OnInit() {

    }
    protected override void OnUpdate() {

    }
    public override void OnRestart() {
        base.EnergyDisable();
        lightOrb.material = disableMaterial;
        light.color = disableColor;
        StopAllCoroutines();
    }

    protected override void EnergyActive() {
        activeAction?.OnAction();
        lightOrb.material = activeMaterial;
        light.color = activeColor;
        AudioManager.instance.PlaySfx("ReverseElectro");
        StartCoroutine(ActiveEnergyTimer());
    }
    protected override void EnergyDisable() {
        base.EnergyDisable();
        lightOrb.material = disableMaterial;
        light.color = disableColor;
        disableAction?.OnAction();
    }

    IEnumerator ActiveEnergyTimer() {
        yield return new WaitForSeconds(5f);
        EnergyDisable();
    }

}


