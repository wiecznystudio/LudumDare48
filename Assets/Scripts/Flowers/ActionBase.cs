using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : MonoBehaviour {

    public abstract void OnAction();

    public abstract void OnRestart();
}
