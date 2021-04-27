using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour {
    [Header("Stats")]
    [SerializeField] private float designatedMoveSpeed = 0f;
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private float jumpForce = 10f;
    [Header("Refferences")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private TriggerArea useTriggerArea;
    [SerializeField] private TriggerArea daggerTriggerArea;
    [SerializeField] private Dagger dagger;
    [SerializeField] private Transform daggerPos;
    [SerializeField] private Transform useText;
    [SerializeField] private BoxCollider2D headCollider;
    [SerializeField] private ParticleSystem speedBuffParticles;
    private float moveSpeed = 0f;
    private float jumpSpeed = 7f;
    private Vector2 moveInput;
    private Vector2 finalVector = Vector2.zero;
    private Vector2 currentVector;
    private bool isGrounded;
    private bool facingLeft = true;
    private bool isUsed = false;
    private float speedTimer = 0f;
    private bool firstDaggCollision = false;
    private FlowerBase flowerToUse = null;

    private void Awake() {
        useTriggerArea.enter = OnUseTriggerAreaEnter;
        useTriggerArea.exit = OnUseTriggerAreaExit;
        daggerTriggerArea.enter = OnDaggerTriggerAreaEnter;
        daggerTriggerArea.gameObject.SetActive(true);
        SetDaggerPos();
        moveSpeed = designatedMoveSpeed;
    }
    private void Update() {
        if(speedTimer <= 0f)
            return;

        speedTimer -= Time.deltaTime;
        if(speedTimer <= 0f) {
            moveSpeed = designatedMoveSpeed;
            speedBuffParticles.Stop();
        }
    }
    private void FixedUpdate() {
        MovementDetection();
    }

    private void MovementDetection() {
        finalVector.x = moveInput.x;
        isGrounded = myCollider.IsTouchingLayers(groundMask);
        if(isGrounded && finalVector.y <= 0f) {
            finalVector.y = 0f;
        } else { finalVector.y += -fallSpeed; }
        currentVector.x = Mathf.Lerp(currentVector.x, finalVector.x, Time.deltaTime * 17f);
        currentVector.y = finalVector.y;
        myRigidbody.velocity = new Vector2(currentVector.x * moveSpeed, currentVector.y * jumpSpeed);
    }

    public void Move(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        if(moveInput.x != 0) {
            facingLeft = (moveInput.x < 0f);
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x) * -1f, 1f, 1f);
        }
        if(isGrounded && moveInput.y > 0.5f) {
            finalVector.y = jumpForce;
        }
    }
    public void Attack(InputAction.CallbackContext context) {
        if(!firstDaggCollision) { return; }
        dagger.Attack();
    }
    public void Throw(InputAction.CallbackContext context) {
        StartCoroutine(ActivateTriggerArea());
        if(!firstDaggCollision) { return; }
        dagger.ParentMe(null);
        dagger.Throw(facingLeft);
    }
    IEnumerator ActivateTriggerArea() {
        yield return new WaitForSeconds(1f);
        daggerTriggerArea.gameObject.SetActive(true);
    }
    public void Use(InputAction.CallbackContext context) {
        if(context.ReadValue<float>() > 0.5f) {
            if(isUsed)
                return;
            isUsed = true;
        } else {
            if(isUsed) {
                isUsed = false;
                return;
            }
        }

        if(flowerToUse == null)
            return;

        flowerToUse.OnUse(this);
    }
    public void Speed(float percent, float time) {
        moveSpeed = designatedMoveSpeed * percent;
        if(moveSpeed > designatedMoveSpeed)
            speedBuffParticles.Play();
        else
            speedBuffParticles.Stop();
        speedTimer = time;
    }
    public void Restart() {
        if(dagger != null || !firstDaggCollision) {
            SetDaggerPos();
        }
    }
    private void SetDaggerPos() {
        if(!firstDaggCollision) { return; }
        dagger.ParentMe(transform);
        dagger.ResetDagger();
        dagger.transform.position = daggerPos.transform.position;
        dagger.transform.rotation = daggerPos.transform.rotation;
        dagger.transform.localScale = daggerPos.transform.localScale;
    }
    private void OnUseTriggerAreaEnter(Collider2D collision) {
        FlowerBase flower = collision.GetComponent<FlowerBase>();
        if(flower == null)
            return;

        flowerToUse = flower;
        Vector3 newPos = collision.transform.position;
        newPos.y += 1f;
        useText.transform.position = newPos;
        useText.gameObject.SetActive(true);
    }
    private void OnUseTriggerAreaExit(Collider2D collision) {
        FlowerBase flower = collision.GetComponent<FlowerBase>();
        if(flower != null && flower == flowerToUse) {
            flowerToUse = null;
            useText.gameObject.SetActive(false);
        }
    }
    private void OnDaggerTriggerAreaEnter(Collider2D collision) {
        Dagger daggerDetect = collision.GetComponent<Dagger>();
        if(daggerDetect != null) {
            firstDaggCollision = true;
            daggerTriggerArea.gameObject.SetActive(false);
            SetDaggerPos();
            AudioManager.instance.PlaySfx("Slash4");
        }
    }
}