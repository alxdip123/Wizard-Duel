using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player1 : MonoBehaviour {

    //public Rigidbody bullet;
    public projectile bullet;
    public pillar morir;
    public GameObject p1;
    public Collider2D col;
    public Rigidbody2D rb;

    public int hp = 100;
    public int inc;
    public int ohp = 100;

    SpriteRenderer draw1;
    SpriteRenderer draw2;
    SpriteRenderer draw3;

    public Sprite noHeart;
    public Sprite halfHeart;
    public Sprite fullHeart;

    public GameObject h1;
    public GameObject h2;
    public GameObject h3;

    Animator anim;

    static public bool pillarExists = false;

    bool isDead = false;

    public Player1()
    {

    }
    //Vector3 v = new Vector3(0, 0);
    //Rigidbody newProjectile;
    //projectile newProjectile;

    // Use this for initialization
    void Start () {

        this.transform.tag = "p1";
        ohp = hp;
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        p1 = GetComponent<GameObject>();
        anim = GetComponent<Animator>();

        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;
        rotation.y = 180;
        position.x = 1;
        position.y = .66f;
        this.transform.position = position;
        this.transform.rotation = rotation;

        draw1 = GetComponent<SpriteRenderer>();
        draw2 = GetComponent<SpriteRenderer>();
        draw3 = GetComponent<SpriteRenderer>();

        draw1 = h1.AddComponent<SpriteRenderer>();
        draw2 = h2.AddComponent<SpriteRenderer>();
        draw3 = h3.AddComponent<SpriteRenderer>();

        draw1.sprite = fullHeart;
        draw2.sprite = fullHeart;
        draw3.sprite = fullHeart;
    }
	
	// Update is called once per frame
	void Update () {
       // var activeProjectiles;
        //activeProjectiles = GameObject.FindGameObjectsWithTag("p1.projectile");
        float yShift = 0.66f;
        //Transform tip = this.transform;

        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.A) && this.transform.position.x > 0)
            {
                Vector3 position = this.transform.position;
                position.x--;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 2)// variables may be changed if area grab is implemented
            {
                Vector3 position = this.transform.position;
                position.x++;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.W) && transform.position.y < yShift * 2)
            {
                Vector3 position = this.transform.position;
                position.y += yShift;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.S) && this.transform.position.y > 0)
            {
                Vector3 position = this.transform.position;
                position.y -= yShift;
                this.transform.position = position;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                projectile newProjectile = Instantiate(bullet, this.transform.position, Quaternion.identity) as projectile;//Rigidbody;
                newProjectile.velocity.Set(0.1f, 0.0f, 0.0f);
                newProjectile.owner = 1; //player 1 is owner
                newProjectile.transform.tag = "p1.projectile";
                anim.SetTrigger("playerattk");
                //projectile.v
                //Projectile.rb.velocity = new Vector3(0.1f, 0f, 0f);
            }
            if (Input.GetKeyDown(KeyCode.B) && !pillarExists) // On pillar spawn
            {
                pillar death = Instantiate(morir, this.transform.position, Quaternion.identity) as pillar;
                death.velocity.Set(0.005f, 0.0f, .0f);
                death.owner = 1;
                death.transform.tag = "p1.pillar";
                anim.SetTrigger("playerattk");
                pillarExists = true;
            }
        }
      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }

        HealthBar();
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.transform.tag == "p2.projectile" || hit.transform.tag == "projectile")
        {
            //Destroy(this.p1);
            //Destroy(hit.gameObject);
            this.hp -= 5;
            //Debug.Log("HIT");
        }
        else if (hit.transform.tag == "p2.pillar")
        {
            this.hp = 0;
        }
    }

    void HealthBar()
    {

        if (hp == ohp)
        {
            return;
        }
        else if (hp >= (4 * ohp / 5))
        {
            draw1.sprite = halfHeart;
        }
        else if (hp >= (3 * ohp / 5))
        {
            draw1.sprite = noHeart;
        }
        else if (hp >= (2 * ohp / 5))
        {
            draw1.sprite = noHeart;
            draw2.sprite = halfHeart;
        }
        else if (hp >= (ohp / 5))
        {
            draw1.sprite = noHeart;
            draw2.sprite = noHeart;
        }
        else if (hp < (ohp / 5) && hp > 0)
        {
            draw1.sprite = noHeart;
            draw2.sprite = noHeart;
            draw3.sprite = halfHeart;
        }
        else if (hp <= 0)
        {
            draw1.sprite = noHeart;
            draw2.sprite = noHeart;
            draw3.sprite = noHeart;
            isDead = true; 
            anim.SetBool("playerDeath", isDead);
            //DestroyObject(this.gameObject);
        }
        return;
    }

    public void changeBool(bool target)
    {
        pillarExists = target;
    }
}
