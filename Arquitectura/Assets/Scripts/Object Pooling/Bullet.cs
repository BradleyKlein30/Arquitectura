using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    public float speed = 10f;
    public int bulletDamage = 1;
    public Rigidbody2D rb;
    public Animator anim;
    #endregion

    private void OnEnable()
    {
        if(rb != null)
        {
            rb.velocity = transform.right * speed;
        }
        Invoke("Disable", 2f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
