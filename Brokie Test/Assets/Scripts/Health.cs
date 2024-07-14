using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthBar;
    

    private void Awake()
    {
        healthBar.maxValue = health;
        SetHealthBar();
    }

    private void Start()
    {
        GameManager.Instance.PlayerDamaged += TakeDamage;
    }

    public void SetHealthBar()
    {
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        SetHealthBar();
    }

    private void Update()
    {
        //healthText.text = health.ToString();
        if(health <= 0)
        {
            Respawn();
            health = 100;
            SetHealthBar();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
    }
}
