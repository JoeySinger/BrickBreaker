using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 828f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    Vector3 paddleToBallVec;
    bool hasStarted = false;

    //Cached component ref's
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVec = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    private void LockBallToPaddle()
    {
        Vector3 paddlePos = new Vector3(paddle1.transform.position.x, paddle1.transform.position.y, paddle1.transform.position.z);
        transform.position = paddlePos + paddleToBallVec;
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //Debug.Log(collision.collider.gameObject.name);
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0, randomFactor));
        if (hasStarted) 
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            if (collision.collider.gameObject.name == "Left Wall" || collision.collider.gameObject.name == "Right Wall")
            {
                //Debug.Log("Left or right wall");
                myRigidBody2D.velocity += velocityTweak;
            }
        } 
    }

    public bool HasStarted()
    {
        return hasStarted;
    }
    

}
