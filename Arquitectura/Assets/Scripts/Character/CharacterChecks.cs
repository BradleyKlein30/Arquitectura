using System.Collections;
using UnityEngine;
using StateBehaviour;

public class CharacterChecks : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    //private CharacterInput input;
    //private CharacterController controller;
    private CharacterChecks checks;
    private CharacterCollisions collisions;
    private PlayerStateMachine PSM;

    public int amountOfAirDashesLeft;
    public int amountOfAirDashes;


    [Header("Ability Checks")]
    public bool canMove = true;
    public bool canJump = true;
    public bool canDash;
    public bool canAirDash;
    public bool canShoot;

    [Header("Debug Checks")]
    public bool isGrounded;
    public bool facingRight = true;
    public bool isJumping;
    public bool isMoving;
    public bool isDashing;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //controller = GetComponent<CharacterController>();
        //input = GetComponent<CharacterInput>();
        collisions = GetComponent<CharacterCollisions>();
        PSM = GetComponent<PlayerStateMachine>();
        checks = this;

        //controller.amountOfAirDashesLeft = controller.amountOfAirDashes;
    }

    void Update()
    {
        //input.HandleInput();
        CheckJump();
        CheckAirDash();
    }

    private void CheckJump()
    {
        if (isGrounded && !isJumping)
        {
            PSM.anim.SetBool("isJumping", isJumping = false);
            canJump = true;
        }
        if (isGrounded && PSM.rb.velocity.y <= 0)
        {
            PSM.anim.SetBool("isJumping", isJumping = false);
            isJumping = false;
        }
        if (!isGrounded && isJumping)
        {
            canJump = false;
        }
    }

    private void CheckAirDash()
    {
        if (isGrounded && PSM.rb.velocity.y <= 0)
        {
            canDash = true;
            // PSM.currentState = new DashState(25, 0.25f, 0.2f, amountOfAirDashes, amountOfAirDashesLeft, anim, rb, checks, PSM);
            //amountOfAirDashesLeft = amountOfAirDashes;
        }
        else
        {
            canDash = false;
        }
    }
}
