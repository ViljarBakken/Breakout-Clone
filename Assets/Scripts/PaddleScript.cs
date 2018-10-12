using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    
    public Collider cl;
    float PaddleSpeed = 10f;
    public GameObject ballPrefab;
    // public Rigidbody rb;

    int lives = 4;

    GameObject attachedBall = null;

    // Use this for initialization
    void Start()
    {
        SpawnBall();

    }
    public void SpawnBall()
    {
        attachedBall = (GameObject)Instantiate(ballPrefab, transform.position + new Vector3(0, .75f, 0), Quaternion.identity);
        lives--;

        GUIText guiLives = GameObject.Find("guiLives").GetComponent<GUIText>();
        guiLives.text = "Lives: " + lives;
    }
    // Update is called once per frame
    void Update()
    {
        //Left-Right movement
        transform.Translate(PaddleSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);
        //rb = GetComponent<Rigidbody>();
        // ballRigidbody = attachedBall.GetComponent<Rigidbody>();
        if (attachedBall)
        {
            Rigidbody ballRigidbody = attachedBall.GetComponent<Rigidbody>();
            ballRigidbody.position = transform.position + new Vector3(0, .75f, 0);

            if (Input.GetButtonDown( "LaunchBall"))
        {
                //Fire the ball

                ballRigidbody.isKinematic = false;
                ballRigidbody.AddForce(300f * Input.GetAxis("Horizontal"), 300f, 0);
                attachedBall = null;
            }
            }

    }
    void FixedUpdate()
    {

    }
    void OnCollisionEnter (Collision col)
    {
        cl = GetComponent<Collider>();
        
        foreach (ContactPoint contact in col.contacts)
        {
           if ( contact.thisCollider == cl )
            {
                //This is the paddle's contact point
               float english = contact.point.x - transform.position.x;
                contact.otherCollider.GetComponent<Rigidbody>().AddForce(300f * english, 0, 0);
            }
        }
    }
}
