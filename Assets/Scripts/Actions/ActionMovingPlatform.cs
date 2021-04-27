using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMovingPlatform : ActionBase {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float platformMoveSpeed = 0f;
    [SerializeField]
    private Vector3[] destinationPoints;
    private int currentPoint = -1;

    private bool active = false;
    Vector3 move = Vector3.zero;

    private void Update() {
        if(!active)
            return;

        transform.Translate(move * Time.deltaTime);
        if(Vector3.Distance(transform.localPosition, destinationPoints[currentPoint]) < 0.1f) {
            move = Vector3.zero;
            active = false;
        }
    }
    public override void OnRestart() {
        active = false;
        currentPoint = -1;
    }

    public override void OnAction() {
        if(active)
            return;
        currentPoint = (currentPoint + 1) % destinationPoints.Length;
        float distanceX = Mathf.Abs(destinationPoints[currentPoint].x - transform.localPosition.x) > 0.1f ? Mathf.Sign(destinationPoints[currentPoint].x - transform.localPosition.x) : 0f;
        float distanceY = Mathf.Abs(destinationPoints[currentPoint].y - transform.localPosition.y) > 0.1f ? Mathf.Sign(destinationPoints[currentPoint].y - transform.localPosition.y) : 0f;
        move = new Vector3(platformMoveSpeed * distanceX, platformMoveSpeed * distanceY, 0f);
        active = true;
    }

}
