using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePass : MonoBehaviour {

    [SerializeField]
    private TriggerArea triggerArea;
    [SerializeField]
    private Transform destinationTransform;
    [SerializeField]
    private int destinationStageId;

    private void Awake() {
        triggerArea.enter += OnEnter;
    }


    private void OnEnter(Collider2D collision) {
        if(collision.GetComponent<PlayerController>() == null)
            return;
        StageController.Change(destinationStageId, destinationTransform);
    }
}
