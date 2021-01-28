using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateBehaviour
{
    //Idle State Class deriving from State
    public class IdleState : State
    {
        #region Variables
        private float moveInput;
        private Animator anim;
        private PlayerStateMachine stateMachine;

        #endregion

        public IdleState( Animator anim, PlayerStateMachine currentStateMachine, float moveInput)
        {
            this.stateMachine = currentStateMachine;
            this.anim = anim;
            this.moveInput = moveInput;
        }

        public override void EnterState()
        {
            // Debug.Log("Enter Idle State");
        }

        public override void UpdateState()
        {
            stateMachine.ComputeStateChange();
            moveInput = 0;

            //Run Animation
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        public override void ExitState()
        {
            // Debug.Log("Exiting Idle State");
        }
    }
}
