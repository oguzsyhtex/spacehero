using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    Transform playerTransform;

    [SerializeField]
    float speed = 5;

    [SerializeField]
    GameObject bullet;

    private float bulletDuration = 0.5f;
    private float bulletTimer = 0;

    private float MaxHp = 100;
    private float currentHp = 100;

    [SerializeField]
    private Image imgHp;

    
    private ScoreManager scoreManager;

    [SerializeField]
    private GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        scoreManager = GameObject.FindGameObjectWithTag("scoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()

    {
        if (bulletTimer>=bulletDuration)
        {
            bulletTimer = 0;
            Shoot();
        }
        bulletTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        rb.AddForce((playerTransform.position - transform.position) * speed);
    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHp -= damage;
        imgHp.fillAmount = currentHp / MaxHp;
        if (currentHp <= 0)
        {
            Destroy(this.gameObject);
            scoreManager.UpdateScore(10);

            Instantiate(destroyEffect, transform.position, Quaternion.identity);

        }
    }

    private void GameOver()
    {
        //RestartGame

    }




}
