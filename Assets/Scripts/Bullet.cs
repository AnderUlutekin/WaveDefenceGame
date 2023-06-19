using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float life = 3f;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void Start()
    {
        //damage = transform.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBig" || collision.gameObject.tag == "EnemySmall")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(gameObject.transform.parent.GetComponent<Gun>().damage);
        }
        Destroy(gameObject);
    }
}
