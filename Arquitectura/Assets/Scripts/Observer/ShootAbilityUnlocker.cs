using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbilityUnlocker : MonoBehaviour
{
    public CharacterChecks checks;

    private void Start()
    {
        GameEvents.current.onShootTriggerEnter += OnShootUnlocked;
    }

    private void OnShootUnlocked()
    {
        Debug.Log("Shoot Unlocked");
        checks.canShoot = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.current.ShootTriggerEnter();
    }
}
