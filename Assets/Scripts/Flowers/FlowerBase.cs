using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowerBase : MonoBehaviour {

    #region variables

    [SerializeField]
    private TriggerArea triggerArea;

    #endregion

    #region methods

    private void Awake() {
        if(triggerArea == null)
            return;

        triggerArea.enter += TriggerAreaEnter;
        triggerArea.stay += TriggerAreaStay;
        triggerArea.exit += TriggerAreaExit;
    }
    private void Start() {
        OnInit();
    }
    private void Update() {
        OnUpdate();
    }

    private void TriggerAreaEnter(Collider2D collision) {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player == null)
            return;
        OnTriggerAreaEnter(player);
    }
    private void TriggerAreaStay(Collider2D collision) {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player == null)
            return;
        OnTriggerAreaStay(player);
    }
    private void TriggerAreaExit(Collider2D collision) {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player == null)
            return;
        OnTriggerAreaExit(player);
    }

    #endregion

    #region virtual methods
    protected abstract void OnInit();
    protected abstract void OnUpdate();
    public abstract void OnRestart();


    public virtual void OnAttack() {
  
    }
    public virtual void OnUse(PlayerController player) {

    }
    public virtual void OnTriggerAreaEnter(PlayerController player) {

    }
    public virtual void OnTriggerAreaStay(PlayerController player) {

    }
    public virtual void OnTriggerAreaExit(PlayerController player) {

    }

    #endregion
}
