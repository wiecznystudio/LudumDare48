using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBuff : FlowerBase {

    private enum BuffType {
        Speed
    }

    [SerializeField]
    private ActionBase action;
    [SerializeField]
    private BuffType buff;
    [SerializeField]
    private float buffSize;
    [SerializeField]
    private float buffTime;

    protected override void OnInit() {

    }
    protected override void OnUpdate() {

    }
    public override void OnRestart() {

    }

    public override void OnUse(PlayerController player) {
        GetBuff(player);
        AudioManager.instance.PlaySfx("Bite2");
    }

    private void GetBuff(PlayerController player) {
        switch(buff) {
            case BuffType.Speed:
                player.Speed(buffSize, buffTime);
                break;
        }
    }

}
