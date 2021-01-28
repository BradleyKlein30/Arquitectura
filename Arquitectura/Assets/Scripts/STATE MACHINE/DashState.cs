using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateBehaviour
{
    //Dash State Class deriving from State
    public class DashState : State
    {
        #region Variables
        private float dashSpeed;
        private float dashDuration;
        private float dashCooldown;
        public int amountOfAirDashes;
        public int amountOfAirDashesLeft;

        private Animator anim;
        private Rigidbody2D rb;

        private CharacterChecks checks;
        #endregion

        public DashState(float dashSpeed, float dashDuration, float dashCooldown, 
            int amountOfAirDashes, int amountOfAirDashesLeft, Animator anim, Rigidbody2D rb, CharacterChecks checks)
        {
            this.dashSpeed = dashSpeed;
            this.dashDuration = dashDuration;
            this.dashCooldown = dashCooldown;
            this.amountOfAirDashes = amountOfAirDashes;
            this.amountOfAirDashesLeft = amountOfAirDashesLeft;
            this.anim = anim;
            this.rb = rb;
            this.checks = checks;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Dash State");
            amountOfAirDashesLeft = amountOfAirDashes;

            // Set direction for dash
            float dirX = Input.GetAxis("Horizontal");
            float dirY = Input.GetAxis("Vertical");

            if (dirY == 0)
            {
                // Case: Dashing only left/right
                if (checks.facingRight) dirX = 1;
                else dirX = -1;

                dirY = 0;
            }

            Vector2 dir = new Vector2(dirX, dirY);


            if (checks.canDash && checks.isGrounded)
            {
                PlayerStateMachine.PSM.StartCoroutine(PlayerStateMachine.PSM.Dash(dashDuration, dir));
                amountOfAirDashesLeft = 0;

                //Dash Animation
                PlayerStateMachine.PSM.anim.SetBool("isDashing", checks.isDashing);
            }
            else if (checks.canAirDash && !checks.isGrounded && amountOfAirDashesLeft > 0)
            {
                PlayerStateMachine.PSM.StartCoroutine(PlayerStateMachine.PSM.Dash(dashDuration, dir));
                amountOfAirDashesLeft--;

                //Air Dash Animation
                PlayerStateMachine.PSM.anim.SetBool("isDashing", checks.isDashing);
            }
        }

        public override void UpdateState()
        {
            PlayerStateMachine.PSM.ComputeStateChange();
        }

        public override void ExitState()
        {
            Debug.Log("Exiting Dash State");
        }
    }
}