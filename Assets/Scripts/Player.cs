using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private Sense playerSense;
    private Collider[] senseRadiusColliders;

    [SerializeField]
    FloatingHealthBar playerHealthbar;
    [HideInInspector]
    public Gun playerGun;

    private Finger movementFinger;
    private Vector2 movementAmount;
    private Vector2 anchoredFingerPos;

    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;

    public float fireRate = 1f;
    public float health = 10f;
    public float maxHealth = 10f;

    private float turnSmoothVelocity;
    private bool shouldFire = false;
    private bool isFiring = false;
    private bool hasWon = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        playerGun = transform.Find("Player Stickman/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder" +
            "/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/Gun").GetComponent<Gun>();
    }

    private void Update()
    {
        playerHealthbar.UpdateHealthBar(health, maxHealth);
        senseRadiusColliders = playerSense.Check();
        if (health > 0 && !hasWon)
        {
            Move();
            if (shouldFire == true && isFiring == false)
            {
                StartCoroutine(PlayerShoot());
            }
        }
    }

    private void Move()
    {
        Vector3 direction = new Vector3(movementAmount.x, 0f, movementAmount.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            shouldFire = false;
            animator.SetBool("isFiring", false);
            animator.SetBool("isWalking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
            if (senseRadiusColliders.Length > 0)
            {
                gameObject.transform.LookAt(senseRadiusColliders[0].transform);
                animator.SetBool("isFiring", true);
                shouldFire = true;
            }
            else
            {
                shouldFire = false;
                animator.SetBool("isFiring", false);
            }
        }
    }

    IEnumerator PlayerShoot()
    {
        isFiring = true;
        while (shouldFire)
        {
            playerGun.Shoot();
            yield return new WaitForSeconds(1 / fireRate);
        }
        isFiring = false;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLostFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLostFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == movementFinger)
        {
            ETouch.Touch currentTouch = movedFinger.currentTouch;
            movementAmount = currentTouch.screenPosition - anchoredFingerPos;
        }
    }
    private void HandleLostFinger(Finger lostFinger)
    {
        if (lostFinger == movementFinger)
        {
            movementFinger = null;
            movementAmount = Vector2.zero;
            anchoredFingerPos = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger touchedFinger)
    {
        if (movementFinger == null)
        {
            movementFinger = touchedFinger;
            movementAmount = Vector2.zero;
            anchoredFingerPos = touchedFinger.screenPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBig" || collision.gameObject.tag == "EnemySmall")
        {
            health -= 1;
            if (health == 0)
            {
                gameController.PlayerDie();
                animator.SetBool("isDead", true);
            }
        }
    }
    
    public void WinAnim()
    {
        hasWon = true;
        animator.SetBool("HasWon", true);
    }
}
