using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool thrusting;
    private float turnDirection;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public Bullet bulletPrefab;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
      
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustSpeed); 
        }
        if (turnDirection != 0.0f)
        {
            rb.AddTorque(turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0.0f;

            gameObject.SetActive(false); 
            FindObjectOfType<GameManager>().PlayerDied();          
        }
    }

    private void OnEnable()
    {
       
       
    }
}
