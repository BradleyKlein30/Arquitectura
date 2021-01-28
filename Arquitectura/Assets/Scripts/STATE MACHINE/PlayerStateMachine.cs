using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateBehaviour;

[AddComponentMenu("Brad/StateMachine")]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterChecks))]
[RequireComponent(typeof(CharacterCollisions))]
public class PlayerStateMachine : MonoBehaviour
{
    #region Public Variables

    public static PlayerStateMachine PSM;
    public State currentState;

    private float moveInput;
    private float speed;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private int amountOfAirDashesLeft;

    public GameObject projectile;
    public Transform firePosition;
    public Animator anim;
    public Rigidbody2D rb;

    private CharacterChecks checks;
    //private CharacterCollisions collisions;
    //private CharacterStats stats;

    #endregion

    private void Awake()
    {
        PSM = PSM ? PSM : this;
    }

    public void Start()
    {
        checks = GetComponent<CharacterChecks>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentState = new IdleState(anim, this, moveInput);
    }

    public void Update()
    {
        currentState.UpdateState();
    }

    public static void TryChangeStateTo(State state)
    {
        if (PSM.currentState != null) PSM.currentState.ExitState();
        PSM.currentState = state;
        PSM.currentState.EnterState();
    }

    public void ComputeStateChange()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Dash") && !checks.isDashing)
        {
            Debug.Log(currentState);
            TryChangeStateTo(new DashState(25, 0.25f, 0.2f, 1, amountOfAirDashesLeft, anim, rb, checks));
        }
        else if (Input.GetButtonDown("Jump") && checks.canJump)
        {
            TryChangeStateTo(new JumpState(19, rb, anim, checks));
        }
        else if (Input.GetButtonDown("Shoot") && checks.canShoot && moveInput != 0)
        {
            Debug.Log(checks == null);
            TryChangeStateTo(new ShootRunState(projectile, firePosition, anim, rb, checks));
        }
        else if (moveInput != 0)
        {
            TryChangeStateTo(new MoveState(moveInput, 8, anim, rb, checks, transform));
        }
        else if (Input.GetButtonDown("Shoot") && checks.canShoot && moveInput == 0)
        {
            Debug.Log(checks == null);
            TryChangeStateTo(new ShootState(projectile, firePosition, anim, rb, checks));
        }
        else
        {
            TryChangeStateTo(new IdleState(anim, this, moveInput));
        }
    }

    public IEnumerator Dash(float dashDur, Vector2 dir)
    {
        float time = 0; //create float to store the time this coroutine is operating
        checks.isDashing = true; //set canDash to false so that we can't keep dashing while boosting

        while (dashDur > time
        ) //we call this loop every frame while our custom dashDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time
                .deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update

            rb.velocity = dir.normalized * -dashSpeed;

            yield return 0; //go to next frame
        }

        yield return new WaitForSeconds(dashCooldown); //Cooldown time for being able to dash again, if you'd like.
        checks.isDashing = false; //set back to true so that we can dash again.
        anim.SetBool("isDashing", checks.isDashing = false);

        TryChangeStateTo(new IdleState(anim, this, moveInput));
    }
}