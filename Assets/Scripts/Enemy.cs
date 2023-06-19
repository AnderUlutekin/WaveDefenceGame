using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Transform> waypoints;
    private GameController gameController;

    [SerializeField]
    private FloatingHealthBar healthBar;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float moveSpeed;

    private int waypointIndex = 0;

    private Animator anim;

    private void Awake()
    {
        waypoints = new List<Transform>();
        Transform spawnpoint;

        if (gameObject.tag == "EnemyBig") {
            spawnpoint = GameObject.Find("spawnBig").transform;
            moveSpeed = 4f;
            health = 10f;
            maxHealth = 10f;
        }
        else {
            spawnpoint = GameObject.Find("spawnSmall").transform;
            moveSpeed = 5f;
            health = 6f;
            maxHealth = 6f;
        }

        waypoints.Add(spawnpoint);

        Transform path = GameObject.Find("FollowPath").transform;

        foreach (Transform child in path)
        {
            waypoints.Add(child);
        }

        anim = GetComponentInChildren<Animator>();
        gameController = FindObjectOfType<GameController>();
    }

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        anim.SetBool("isWalking", true);
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {

            transform.position = Vector3.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            transform.LookAt(waypoints[waypointIndex].transform);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                if (waypointIndex == waypoints.Count)
                {
                    anim.SetBool("isWalking", false);
                }
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health = health - damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameController.GetKillMoney(gameController.mobValue);
        gameController.RemoveEnemy(gameObject.GetComponent<Enemy>());
        Destroy(gameObject);
    }
}
