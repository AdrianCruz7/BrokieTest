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
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 50;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthBar;

    private void Awake()
    {
        healthBar.maxValue = maxHealth;
        SetHealthBar();
    }

    private void Start()
    {
        GameManager.Instance.PlayerHealthChange += DamageAndHeal;
    }

    public void SetHealthBar()
    {
        healthBar.value = currentHealth;
    }

    public void DamageAndHeal(float damage)
    {
        currentHealth += damage;
        SetHealthBar();
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Respawn();
            currentHealth = 100;
            SetHealthBar();
        } else if (currentHealth > maxHealth)
        {
            healthBar.value = currentHealth;
            currentHealth = maxHealth;
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
    }
}
