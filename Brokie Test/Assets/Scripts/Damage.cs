using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private float damage = 1f;
    private float damageTimer = .5f;
    private float timer = 1f;
    private bool startTimer = false;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.gamePaused) return;

        if (startTimer)
        {
            timer -= Time.deltaTime;
            if(timer <= 0) 
            {
                timer = damageTimer;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            //other.GetComponent<Health>().TakeDamage(damage);
            GameManager.Instance.SignalPlayerDamaged(damage);
            startTimer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //other.GetComponent<Health>().TakeDamage(damage);
            GameManager.Instance.SignalPlayerDamaged(damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            startTimer = false;
        }
    }
}
