using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateBehaviour
{
    //Move State Class deriving from State
    public class MoveState : State
    {
        #region Variables
        private float moveInput;
        private float speed;
        private Animator anim;
        private Rigidbody2D rb;
        private CharacterChecks checks;
        private int facingDirection = 1;
        private Transform transform;
        #endregion

        public MoveState(float moveInput, float speed, Animator anim, Rigidbody2D rb, CharacterChecks checks, Transform transform)
        {
            this.moveInput = moveInput;
            this.speed = speed;
            this.anim = anim;
            this.rb = rb;
            this.checks = checks;
            this.transform = transform;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Move State");
        }

        public override void UpdateState()
        {
            if (checks.canMove)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

                //Run Animation
                anim.SetFloat("Speed", Mathf.Abs(moveInput));

                //flips player
                if ((moveInput < 0 && !checks.facingRight) || moveInput > 0 && checks.facingRight)
                {
                    facingDirection *= -1;
                    checks.facingRight = !checks.facingRight;
                    transform.Rotate(0f, 180f, 0f);
                }

                //checks is player is moving or not
                if (rb.velocity == Vector2.zero)
                    checks.isMoving = false;
                else
                    checks.isMoving = true;
                
                PlayerStateMachine.PSM.ComputeStateChange();
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exiting Move State");
        }
    }
}
