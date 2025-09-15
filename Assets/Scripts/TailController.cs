using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    private Rigidbody2D connectedBody;
    private HingeJoint2D hingeJoint2D;

    [SerializeField]
    private GameObject playerBullet;

    public Transform target;
    public float followSpeed = 2f;
    private int index;



    public void Setup(Rigidbody2D rigidbody2D,GameObject playerBullet,int index)
    {
        connectedBody = rigidbody2D;
        this.playerBullet = playerBullet;

        hingeJoint2D = GetComponent<HingeJoint2D>();
        hingeJoint2D.connectedBody = connectedBody;

        if (hingeJoint2D!=null&&connectedBody!=null)
        {
            hingeJoint2D.connectedBody = connectedBody;
            hingeJoint2D.anchor = new Vector2(0, index);
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
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
            }
        }
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
