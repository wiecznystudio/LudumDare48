using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTriggerAreaDelegate(Collider2D collision);

public class TriggerArea : MonoBehaviour {

    public OnTriggerAreaDelegate enter;
    public OnTriggerAreaDelegate stay;
    public OnTriggerAreaDelegate exit;

    public void OnTriggerEnter2D(Collider2D collision) {
        enter?.Invoke(collision);
    }
    public void OnTriggerStay2D(Collider2D collision) {
        stay?.Invoke(collision);
    }
    public void OnTriggerExit2D(Collider2D collision) {
        exit?.Invoke(collision);
    }

}
