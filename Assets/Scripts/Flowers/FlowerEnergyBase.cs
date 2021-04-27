using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowerEnergyBase : FlowerBase {

    [Header("Flower Energy Base")]

    [SerializeField]
    private float activeDelay = 0.5f;

    private List<FlowerEnergyBase> parentFlowers = new List<FlowerEnergyBase>();


    public void OnEnergyActive(FlowerEnergyBase flower) {
        if(parentFlowers.Contains(flower))
            return;
        parentFlowers.Add(flower);
        EnergyActive();
    }
    protected abstract void EnergyActive();

    protected virtual void EnergyDisable() {
        parentFlowers.Clear();
    }

}


