using UnityEngine;
using System;
using System.Collections;

public class pillar : MonoBehaviour {

    public Vector3 velocity;
    public int owner; //1 is player 1 ; 2 is player 2
    public Collider2D hit;

    static Player1 p1 = new Player1();
    static Player2 p2 = new Player2();

    public projectile bullet;

    const double PI = Math.PI;

    void Start()
    {
        owner = 0;
        hit = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        position.x += this.velocity.x;
        this.transform.position = position;

        if (position.x > 6 || position.x < -1)
        {
            DestroyObject(this.gameObject);
        }

        return;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.transform.tag == "p2.pillar" || obj.transform.tag == "p1.pillar")
        {
            const double vx0 = 0.01;
            float vx;
            float vy;

            for (double angle = 0; angle < (2 * PI); angle += (PI / 10))
            {
                vx = (float)(vx0 * Math.Cos(angle));
                vy = (float)(vx0 * Math.Sin(angle));
                // Debug.Log("For angle " + (angle * 360 / (2 * PI)) + ", value of 'vy' is: " + vy + '\n');
                SpawnBullet(vx, vy);
            }
            Destroy(this.gameObject);
            if (obj.transform.tag == "p1.pillar") p1.changeBool(false);
            else if (obj.transform.tag == "p2.pillar") p2.changeBool(false);
        }
    }

    void SpawnBullet(float xVel, float yVel)
    {
        projectile debris = Instantiate(bullet, this.transform.position, Quaternion.identity) as projectile;
        debris.velocity.Set(xVel, yVel, 0);
        debris.transform.tag = "projectile";
        debris.owner = 0;
    }

}