using UnityEngine;
using System;
using System.Collections;



public class projectile : MonoBehaviour
{

    public Vector3 velocity;
    public int owner; // 1 is player 1 ; 2 is player 2 ; 0 is neutral
    public Collider2D hit;
    public GameObject kineticObj;
    public static int damage = 10;



    void Start()
    {
        owner = 0;
        hit = GetComponent<Collider2D>();
        kineticObj = GetComponent<GameObject>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        position.x += this.velocity.x;
        position.y += this.velocity.y;
        this.transform.position = position;

        if (position.x > 6 || position.x < -1 || position.y > 4 || position.y < -4)
        {
            DestroyObject(this.gameObject);
        }
  
        return;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (this.transform.tag == "projectile") // If the bullet is neutral
        {
            if (obj.transform.tag == "p1" || obj.transform.tag == "p2") // Then destroy only if it hits the player
            {
                Destroy(this.gameObject);
            }
            else if (obj.transform.tag == "p1.pillar" || obj.transform.tag == "p2.pillar") // If runs into pillar
            {
                Destroy(this.gameObject);
            }
        }
        else if (obj.transform.tag == "projectile") ; // If the object was spawned by the player
        else if (obj.transform.tag[1] != this.transform.tag[1]) // Otherwise, if it collides with anything other than itself.
        {
            Destroy(this.gameObject);
            //Debug.Log(player.transform.tag);
            //Debug.Log(this.transform.tag);
        }
    }
    
}