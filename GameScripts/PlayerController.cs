using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //Initialise Values.
    public float speed;
    Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    private bool facingRight;


    // Start is called before the first frame update
    //Player is facing right set to true as it is always the case from the start, Rigidbody Variable also set.
    void Start() 
    {
        facingRight = true;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    //Variables set from the values in GameController
    void Update()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        //Walking and Shooting Horizontally or Vertically, this allows us to move in directions and shoot.
        //Adds a delay to shooting using the lastFire and fireDelay Values.
        //Moving the player in direction and according to their speed.
        float horizontal = Input.GetAxis("Horizontal"); 
        Flip(horizontal);
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");
        if((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay) 
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); 
        collectedText.text = "Items Collected: " + collectedAmount;
    }

    //Shooting Positions and directions according to bulletspeed, also an audio bite to play when shooting.
    void Shoot(float x, float y)
    {
        SoundManagerScript.PlaySound("PlayerShoot");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        ); 
    }

    //This uses the horizontal values of walking and if the player is walking to the left, this switches the scale to -1 which turns them left.
    private void Flip(float horizontal) 
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;

        }
    }
}
