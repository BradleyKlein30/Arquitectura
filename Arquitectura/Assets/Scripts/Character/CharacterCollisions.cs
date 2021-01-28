using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollisions : MonoBehaviour
{
    [Header("Ground Layer")]
    public Transform groundCheck;
    public float CheckRadius;
    public LayerMask groundLayer;
    
    private CharacterChecks checks;
    //private CharacterController controller;

    void Start()
    {
        checks = GetComponent<CharacterChecks>();
        //controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckSurroundings();
    }

    void CheckSurroundings()
    {
        checks.isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, CheckRadius);
    }

}
