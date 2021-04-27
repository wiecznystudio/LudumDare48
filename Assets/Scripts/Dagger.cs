using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DaggerState { Flying, Attacked, Held, Colided, PickedUp };
public class Dagger : MonoBehaviour {
    [Header("Stats")]
    [SerializeField] private float flightSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float stabSpeed;
    [SerializeField] private float fallSpeed;

    [Header("Refferences")]
    [SerializeField] private CircleCollider2D myCollider;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Color[] daggerEmission = new Color[2];


    public DaggerState daggerState;
    private Vector2 flightVector;
    private float flightTime = 0f;


    private void Awake() {
        flightVector = new Vector2(0f, 0f);
        daggerState = DaggerState.Colided;
        renderer.sharedMaterial.SetColor("_EmissionColor", daggerEmission[0]);
    }
    private void Update() {
        if(daggerState != DaggerState.Flying)
            return;

        flightTime += Time.deltaTime / 1f;
        flightTime = Mathf.Clamp(flightTime, 0f, 1f);
        Color color = Color.Lerp(daggerEmission[0], daggerEmission[1], flightTime);
        renderer.sharedMaterial.SetColor("_EmissionColor", color);
    }
    private void FixedUpdate() {
        myRigidbody.velocity = flightVector;
        if(daggerState == DaggerState.Colided) {
            flightVector = new Vector2(0f, 0f);
            myAnimator.speed = 0f;
            myCollider.enabled = true;
        }
    }
    public void Attack() {
        if(daggerState == DaggerState.Colided) { return; }
        myAnimator.SetBool("Attack", true);
        myAnimator.speed = 1f;
        daggerState = DaggerState.Attacked;
    }
    public void Throw(bool flightDirection) {
        if(daggerState == DaggerState.Colided) { return; }
        if(flightDirection) {
            flightVector = Vector2.left * flightSpeed;
        } else { flightVector = Vector2.right * flightSpeed; }
        daggerState = DaggerState.Flying;
        myAnimator.SetBool("Flight", true);
        myAnimator.speed = 1f;
    }
    public void ParentMe(Transform newParent) {
        transform.SetParent(newParent);
    }
    public void ResetDagger() {
        myAnimator.SetBool("Flight", false);
        myCollider.enabled = false;
        daggerState = DaggerState.Held;
        flightVector = new Vector2(0f, 0f);
        myAnimator.speed = 1f;        
        flightTime = 0f;
        renderer.sharedMaterial.SetColor("_EmissionColor", daggerEmission[0]);
    }
    private void StopAttackAnim() {
        myAnimator.SetBool("Attack", false);
    }
    private void PlayAttackSFX() {

        AudioManager.instance.PlaySfx("Slash");
    }
    private void PlayThrowSfx() {
        AudioManager.instance.PlaySfx("Slash1");
    }
    private void OnTriggerEnter2D(Collider2D collision) {       
        if(collision.tag == "Player") { return; }
        if (daggerState == DaggerState.Flying) {
            flightVector = new Vector2(myRigidbody.velocity.x, -fallSpeed);
            AudioManager.instance.PlaySfx("Collide");
            if(collision.tag == "Ground") {
                daggerState = DaggerState.Colided;
                AudioManager.instance.PlaySfx("Hit");
            }
        } else if(daggerState == DaggerState.Colided) {
            flightVector = new Vector2(0f, 0f);
            myAnimator.speed = 0f;
        }
        FlowerBase flower = collision.GetComponentInParent<FlowerBase>();
        if(flower != null) {
            flower.OnAttack();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        daggerState = DaggerState.Colided;
        if(daggerState == DaggerState.Colided) {
            flightVector = new Vector2(0f, 0f);
            myAnimator.speed = 0f;
        }
    }
}