using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class playerMove : Subject
{
    [Header("Ref")]
    [SerializeField] private bl_Joystick joystick;
    [SerializeField] private ChargedBar chargedBar;

    [Header("Check")]
    [SerializeField] bool isCharging = false;

    [SerializeField] bool isGrounded;
    [SerializeField] bool wasGrounded;
    [SerializeField] bool isFacingRight = true;
   
    Animator anim;
    Rigidbody2D rb;

    float horizontalInput;

    [Header("Setting")]
    public float jumpForce;
    public float maxJumpForce;
    [SerializeField] float defaultJumpForce;    
    [SerializeField] float chargeSpeed;    
    [SerializeField] float sideJumpForce;    
    [SerializeField] float speed;
    [SerializeField] float defaultSpeed;    
       

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //set 
        defaultSpeed = speed;
        defaultJumpForce = jumpForce;
    }

    private void FixedUpdate()
    {
        anim.SetFloat("xVelocity", Math.Abs(joystick.Horizontal));
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isCharging", isCharging);
    }

    //Update is called once per frame
    void Update()
    {       

        horizontalInput = joystick.Horizontal * speed * Time.deltaTime; ;

        transform.Translate(horizontalInput, 0, 0);

        FlipSprite();        
    }

    // for flipping character's sprite when turn to left or right
    void FlipSprite()
    {
        if (isCharging)
            return;
        if (isFacingRight && joystick.Horizontal < 0f || !isFacingRight && joystick.Horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            //tempDir = isFacingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = tempScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {            
            isGrounded = true;
            anim.SetBool("isJumping", !isGrounded);

            if (!wasGrounded)
            {
                NotifyObserver(PlayerAction.Landed); // play landing sound
                print("LAND");                
            }            
        }
    }  

    // This function will be attach to the circle button
    public void StartHolding()
    {
        if (!isGrounded) // if player is not on the ground, prevent player from charging jumpForce
            return;
        isCharging = true;        
        StartCoroutine(Charging());
    }


    // holding button to charge
    private IEnumerator Charging()
    {
        while (isCharging)
        {
            chargedBar.ShowChargedBar(); // show charge bar ui while player is charging
            speed = 0;  //prevent player from moving during charging            
            if (jumpForce < maxJumpForce) 
            {
                jumpForce += (Time.deltaTime * chargeSpeed);
            }
            else // if player's jumpForce reach max value then make player stop charging and reset all the value to default one
            {
                isCharging = false;
                ResetValue();
            }
            chargedBar.UpdateChargedBar(jumpForce, maxJumpForce); // while player is charging, always update charge bar ui
            yield return null;
        }
        chargedBar.HideChargedBar(); // hide charge bar ui after player finish chargiging
    }    

    // releasing button to jump
    public void Jump()
    {
        // if player is not on the ground or jumpForce is still not enough, prevent player from jumping
        if (!isGrounded || jumpForce <= 6)
        {            
            return;
        }                 

        isCharging = false;
        anim.SetBool("isCharging", isCharging);
        isGrounded = false;
        anim.SetBool("isJumping", !isGrounded);
        wasGrounded = false;

        Vector2 jumpDirection = Vector2.up * jumpForce;

        if (isFacingRight) // if player is facing right direction, then jump to right side, vice versa
        {            
            jumpDirection += Vector2.right * sideJumpForce;            
        }
        else
        {
            jumpDirection += Vector2.left * sideJumpForce;            
        }        
        rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        chargedBar.UpdateChargedBar(jumpForce, maxJumpForce); // update charge bar ui
        ResetValue(); // reset value after player jumped
        NotifyObserver(PlayerAction.Jump); // play jump sound   
    }

    // reset all value to default 
    void ResetValue()
    {
        jumpForce = defaultJumpForce;
        speed = defaultSpeed;
        //jumpHolding = false;
    }
}

