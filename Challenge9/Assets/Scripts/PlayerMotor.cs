using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private Animator anim;
    private const float LANE_DISTANCE = 3.0f;
    private CharacterController characterController;
    private float jumpForce = 9.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    
    public int desiredLane = 1; //0 = Left, 1 = middle, 2 = right

    //Speed stuffs
    private float speed = 7.0f;
    private float speedIncreaseLastTick ;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.0f;

    public AudioSource audioSource;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerScript.isGameStarted)
        {
            return;
        }

        if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speed += speedIncreaseAmount;
            speedIncreaseLastTick = Time.time;
            GameManagerScript.instance.modifierScore = (speed - 7.0f) + 1.0f;
            GameManagerScript.instance.updateScores();
        }

        //Get input on where we should be
        if (MobileInput.instance.SwipeLeft)
        {
            MoveLane(-1);
        }
        if (MobileInput.instance.SwipeRight)
        {
            MoveLane(+1);
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if(desiredLane == 0) // Note that this check works because you must be in desiredLane of 1.
                               // You can't jump from 2 to 0 and vice versa
        { 
            targetPosition += Vector3.left * LANE_DISTANCE; //same as x += (-1 * 3)
        }else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE; //same as x += (1 * 3)
        }

        //Let's calculate our move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded",isGrounded);
        //Calculate Y
        if (isGrounded) //grounded
        {
            verticalVelocity = 0.1f;
            if (MobileInput.instance.SwipeUp)
            {
                //Jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }else
            if (MobileInput.instance.SwipeDown)
            {
                //slide
                StartSliding();
                Invoke("StopSliding", 1.0f);
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            if (MobileInput.instance.SwipeDown)
            {
                //Jump
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //Move the pengu
        characterController.Move(moveVector * Time.deltaTime);

        //Rotate the Penguin to where he is going
        Vector3 dir = characterController.velocity;
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.05f) ;


    }

    private void StartSliding()
    {
        anim.SetBool("isSliding", true);
        characterController.height /= 2;
        characterController.center = new Vector3(characterController.center.x, characterController.center.y / 2, characterController.center.z);
    }

    private void StopSliding()
    {
        anim.SetBool("isSliding", false);
        characterController.height *= 2;
        characterController.center = new Vector3(characterController.center.x, characterController.center.y * 2, characterController.center.z);
    }

    private void MoveLane(int direction)
    {
        desiredLane += direction;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(
            new Vector3(
                characterController.bounds.center.x,
                (characterController.bounds.center.y - characterController.bounds.extents.y) + 0.2f,
                characterController.bounds.center.z),
            Vector3.down
            );
        //Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 1.0f) ;
       
        return Physics.Raycast(ray, 0.2f + 0.1f);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                DeadMode();
                break;

        }
    }

    private void DeadMode()
    {
        audioSource.PlayOneShot(deathSound);
        anim.SetTrigger("Death");
        GameManagerScript.instance.OnDeath();
    }
}
