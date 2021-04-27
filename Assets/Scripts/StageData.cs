using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour {

    [SerializeField]
    public int stageNo;

    [SerializeField]
    public Transform restartPoint;

    [SerializeField]
    public FlowerBase[] flowers;

    [SerializeField]
    public ActionBase[] actions;

    [SerializeField]
    public Vector2[] cameraClamp = new Vector2[2];
}
