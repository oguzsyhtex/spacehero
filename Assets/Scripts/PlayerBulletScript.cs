using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    [SerializeField]
    float speed = 50;



    public void Setup(Transform target)
    {
        this.target = target;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (target !=null)
        {
            rb.AddForce((target.position - transform.position) * speed);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }


}
