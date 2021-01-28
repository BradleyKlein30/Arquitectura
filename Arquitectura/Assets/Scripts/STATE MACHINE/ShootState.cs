using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateBehaviour
{
    //Shoot State Class deriving from State
    public class ShootState : State
    {
        #region Variables
        private GameObject projectile;
        private Transform firePosition;

        private Animator anim;
        private Rigidbody2D rb;

        private CharacterChecks checks;
        #endregion

        public ShootState(GameObject projectile, Transform firePosition, Animator anim, Rigidbody2D rb, CharacterChecks checks)
        {
            this.projectile = projectile;
            this.firePosition = firePosition;
            this.anim = anim;
            this.rb = rb;
            this.checks = checks;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Shoot State");

            GameObject obj = BulletPool.current.GetPooledBullet();
            if (obj == null) return;

            obj.transform.position = firePosition.position;
            obj.transform.rotation = firePosition.rotation;
            obj.SetActive(true);


            Debug.Log(rb.velocity);
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                checks.isMoving = false;

                //Attack Animation
                anim.SetTrigger("Attack");
            }
        }

        public override void UpdateState()
        {
            PlayerStateMachine.PSM.ComputeStateChange();
        }

        public override void ExitState()
        {
            Debug.Log("Exiting Shoot State");
        }
    }
}
