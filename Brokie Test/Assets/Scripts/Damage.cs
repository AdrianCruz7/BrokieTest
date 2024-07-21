using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] float damageTimer = .5f;
    [SerializeField] float timer = 1f;
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
            GameManager.Instance.SignalPlayerHealthChange(-damage);
            startTimer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //other.GetComponent<Health>().TakeDamage(damage);
            GameManager.Instance.SignalPlayerHealthChange(-damage);
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
