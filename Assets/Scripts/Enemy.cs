using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Transform> waypoints;

    private float moveSpeed;

    private int waypointIndex = 0;

    private void Awake()
    {
        waypoints = new List<Transform>();
        Transform spawnpoint;

        if (gameObject.tag == "EnemyBig") {
            spawnpoint = GameObject.Find("spawnBig").transform;
            moveSpeed = 4f;
        }
        else {
            spawnpoint = GameObject.Find("spawnSmall").transform;
            moveSpeed = 5f;
        }

        waypoints.Add(spawnpoint);

        Transform path = GameObject.Find("FollowPath").transform;

        foreach (Transform child in path)
        {
            waypoints.Add(child);
        }
    }

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
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

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}
