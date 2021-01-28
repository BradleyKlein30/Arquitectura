using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateBehaviour
{
    //Shoot State Class deriving from State
    public class ShootRunState : State
    {
        #region Variables
        private GameObject projectile;
        private Transform firePosition;

        private Animator anim;
        private Rigidbody2D rb;

        private CharacterChecks checks;
        #endregion

        public ShootRunState(GameObject projectile, Transform firePosition, Animator anim, Rigidbody2D rb, CharacterChecks checks)
        {
            this.projectile = projectile;
            this.firePosition = firePosition;
            this.anim = anim;
            this.rb = rb;
            this.checks = checks;
        }


        public override void EnterState()
        {
            Debug.Log("Enter Shoot Run State");
            Debug.Log(checks == null);


            GameObject obj = BulletPool.current.GetPooledBullet();
            if (obj == null) return;

            obj.transform.position = firePosition.position;
            obj.transform.rotation = firePosition.rotation;
            obj.SetActive(true);

            if (rb.velocity != Vector2.zero)
            {
                checks.isMoving = true;

                //Run Attack Animation
                anim.SetTrigger("Run Attack");
            }
        }

        public override void UpdateState()
        {
            PlayerStateMachine.PSM.ComputeStateChange();
        }

        public override void ExitState()
        {
            Debug.Log("Exiting Shoot Run State");
        }
    }
}
