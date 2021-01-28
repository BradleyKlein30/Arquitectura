using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateBehaviour
{
    //Jump State Class deriving from State
    public class JumpState : State
    {
        #region Variables
        private float jumpForce = 19;
        private Animator anim;
        private Rigidbody2D rb;
        private CharacterChecks checks;
        private Vector2 newVelocity;
        private Vector2 newForce;
        #endregion

        public JumpState(float jumpForce, Rigidbody2D rb, Animator anim, CharacterChecks checks)
        {
            this.jumpForce = jumpForce;
            this.rb = rb;
            this.anim = anim;
            this.checks = checks;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Jump State");

            newVelocity.Set(0.0f, 0.0f);
            rb.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse);

            checks.isJumping = true;

            //Jump Animation
            anim.SetBool("isJumping", checks.isJumping);
        }

        public override void UpdateState()
        {
            PlayerStateMachine.PSM.ComputeStateChange();
        }

        public override void ExitState()
        {
            Debug.Log("Exiting Jump State");
        }
    }
}


