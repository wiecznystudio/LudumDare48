using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour {

    [SerializeField]
    private TriggerArea triggerArea;
    [SerializeField]
    private Transform diamond;
    [SerializeField]
    private Vector3 startingPosition;

    private void Awake() {
        triggerArea.enter += OnPlayerEnter;
        startingPosition = transform.localPosition;
    }
    private void Update() {
        diamond.localRotation = Quaternion.Euler(diamond.localRotation.eulerAngles + new Vector3(0, 30f, 0) * Time.deltaTime);
        Vector3 newPos = startingPosition;
        newPos.y += Mathf.Sin(Time.fixedTime) / 5f;
        diamond.localPosition = newPos;
    }

    private void OnPlayerEnter(Collider2D collision) {
        StageController.EndGame();
    }



}
