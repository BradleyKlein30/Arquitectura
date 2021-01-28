using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollowWithLimits : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnwalkSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        public Vector2 maxXAndY; // The maximum x and y coordinates the camera can have.
        public Vector2 minXAndY; // The minimum x and y coordinates the camera can have.


        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero,Time.unscaledDeltaTime * lookAheadReturnwalkSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping * Time.timeScale);

            // The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
            newPos.x = Mathf.Clamp(newPos.x, minXAndY.x, maxXAndY.x);
            newPos.y = Mathf.Clamp(newPos.y, minXAndY.y, maxXAndY.y);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}
