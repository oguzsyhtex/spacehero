using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    FixedJoystick fixedJoystick;

    Vector2 moveVector;

    private float maxHp = 100;
    
    private float currentHp = 100;

    [SerializeField]
    private Image imgHP;

    [SerializeField]
    private GameObject playerBullet;
    private float bulletDuration = 0.5f;
    private float bulletTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fixedJoystick.JoystickPoinerDown)
        {
            Attack();
        }
        moveVector.x = fixedJoystick.Horizontal;
        moveVector.y = fixedJoystick.Vertical;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }



    private void HandleMovement()
    {
        if (fixedJoystick.JoystickPoinerDown)
        {
            rb.AddForce(moveVector * 15);
        }

    }

    private void HandleRotation()
    {
        float hAxis = fixedJoystick.Horizontal;
        float vAxis = fixedJoystick.Vertical;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0,0,-zAxis);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHp -= damage;
        imgHP.fillAmount = currentHp / maxHp;
        if (currentHp<= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        //Restart game
    }

    private void Attack()
    {
        if (bulletTimer>=bulletDuration)
        {
            bulletTimer = 0;
            Instantiate(playerBullet, transform.position, Quaternion.identity).GetComponent<PlayerBulletScript>().Setup(GameObject.FindGameObjectWithTag("Enemy").transform);
        }
        bulletTimer += Time.deltaTime;
    }

}
