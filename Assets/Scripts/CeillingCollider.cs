using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeillingCollider : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    private void OnCollisionEnter2D(Collision2D collision) {
        myRigidbody.velocity =  new Vector2(myRigidbody.velocity.x, 0f);
    }
}
