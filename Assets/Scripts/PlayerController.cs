using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    FixedJoystick fixedJoystick;

    Vector2 moveVector;
    private readonly int moveSpeedMultiplier=10;
    private float moveSpeed = 20f;

    private static float maxHp = 5000;
    
    private float currentHp = maxHp;

    [SerializeField]
    private Image imgHP;

    [SerializeField]
    private GameObject playerBullet;
    private float bulletDuration = 0.5f;
    private float bulletTimer = 0f;

    [SerializeField]
    private List<GameObject> tails;
    [SerializeField]
    private GameObject tail;

    [SerializeField]
    private Text txtLevel;


    private int level = 0;

    public int Level { get => level; set => level = value; }

    [SerializeField]
    private GameObject destroyEffect;

    [SerializeField]
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var tailObj = Instantiate(tail, transform.position, Quaternion.identity);
        tailObj.GetComponent<TailController>().Setup(rb, playerBullet);
        tails.Add(tailObj);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fixedJoystick.JoystickPoinerDown)
        {
            if (bulletTimer>=bulletDuration)
            {
                bulletTimer = 0;
                Attack();
                TailAttack();
            }
            bulletTimer += Time.deltaTime;
        }
        
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
            rb.AddForce(fixedJoystick.Direction * moveSpeed);
        }

    }

    private void HandleRotation()
    {
        float hAxis = fixedJoystick.Horizontal;
        float vAxis = fixedJoystick.Vertical;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0,0,-zAxis);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemyBullet"))
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
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            GameOver();
        }
    }

    private void GameOver()
    {
        scoreManager.SetHighScore();
        SceneManager.LoadScene(0);
    }

    private void Attack()
    {
        var enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            Instantiate(playerBullet, transform.position, Quaternion.identity).GetComponent<PlayerBulletScript>().Setup(enemy.transform.transform);
        }

    }

    private void TailAttack()
    {
        foreach (GameObject tailObj in tails)
        {
            tailObj.GetComponent<TailController>().Attack();
        }
    }

    public void UpdateLevel()
    {

        var prevTail = tails[level];
        var tailObj = Instantiate(tail, transform.position, Quaternion.identity);
        tailObj.GetComponent<TailController>().Setup(rb,playerBullet);
        tails.Add(tailObj);

        level++;
        moveSpeed = level * moveSpeedMultiplier;
        txtLevel.text = "Level: " + level;
    }



}
