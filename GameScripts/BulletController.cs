using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet = false;

    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    // Start is called before the first frame update

    //set bulletsize according to GameController Values.
    void Start()
    {  
        StartCoroutine(DeathDelay()); 
        if(!isEnemyBullet)
        { 
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }
    }

    //set enemy bullet to aim at player and shoot.
    //Destroys after certain range.
    void Update() 
    {
        if(isEnemyBullet)
        {   
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
            if(curPos == lastPos)
            {   
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
    }

    //Sets player position value.
    public void GetPlayer(Transform player) 
    {
        playerPos = player.position;
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    //If Player shoots enemy, the Enemy dies.
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy" && !isEnemyBullet)
        {  
            col.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        //If Player shoots Boss, the Boss dies.
        if (col.tag == "Boss" && !isEnemyBullet)
        {  
            col.gameObject.GetComponent<EnemyController>().BossDeath();
            Destroy(gameObject);
        }

        //If Enemy shoots player, they are damaged.
        if (col.tag == "Player" && isEnemyBullet)
        {  

            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}
