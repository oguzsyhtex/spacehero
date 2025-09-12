using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    private Rigidbody2D connectedBody;
    private HingeJoint2D hingeJoint2D;

    private GameObject playerBullet;

    public void Setup(Rigidbody2D rigidbody2D,GameObject playerBullet)
    {
        connectedBody = rigidbody2D;
        this.playerBullet = playerBullet;



        hingeJoint2D = GetComponent<HingeJoint2D>();
        hingeJoint2D.connectedBody = connectedBody;

        if (hingeJoint2D!=null&&connectedBody!=null)
        {
            hingeJoint2D.connectedBody = connectedBody;
        }
        else
        {
            Debug.LogError("HingleJoint2D veya connectedBody null!", this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        var enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy !=null)
        {
            Instantiate(playerBullet, transform.position, Quaternion.identity).GetComponent<PlayerBulletScript>().Setup(enemy.transform.transform);
        }


    }

}
