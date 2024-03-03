using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class S_PlayerStatistics : MonoBehaviour
{
    //Main Stats
    private float _playerMaxHealth;
    private float _playerSpeed;
    private float _playerhitStength;
    private float _playerhitCooldown;

    void Start()
    {
        initStat();
    }


    void Update()
    {

    }


    private void initStat()
    {
        S_Player playerScript = GetComponent<S_Player>();
        _playerMaxHealth = playerScript.getMaxHealth();
        _playerSpeed = playerScript.getSpeed();
        _playerhitStength = playerScript.getHitDamage();
        _playerhitCooldown = playerScript.getHitCooldown();
    }

    private void applyStatsToPlayer()
    {
        S_Player playerScript = GetComponent<S_Player>();
        playerScript.setMaxHealth(_playerMaxHealth);
        playerScript.setSpeed(_playerSpeed);
        playerScript.setHitDamage(_playerhitStength);
        playerScript.setHitCooldown(_playerhitCooldown);
    }

    //Setters
    public void setMaxHealth(float maxHealth)
    {
        _playerMaxHealth += maxHealth;
        applyStatsToPlayer();
    }
    public void setSpeed(float speed)
    {
        _playerSpeed += speed;
        GetComponent<S_Player>().HUD.rootVisualElement.Q<Label>("MSvalue").text = "" + _playerSpeed;
        applyStatsToPlayer();
    }
    public void setHitStength(float hitStength)
    {
        _playerhitStength += hitStength;
        GetComponent<S_Player>().HUD.rootVisualElement.Q<Label>("DMGvalue").text = "" + _playerhitStength;
        applyStatsToPlayer();
    }
    public void setHitCooldown(float hitCooldown)
    {
        _playerhitCooldown -= hitCooldown;
        GetComponent<S_Player>().HUD.rootVisualElement.Q<Label>("ASvalue").text = "" + _playerhitCooldown;
        applyStatsToPlayer();
    }
    //Getters
    public float getMaxHealth()
    {
        return _playerMaxHealth;
    }
    public float getSpeed()
    {
        return _playerSpeed;
    }
    public float getHitStength()
    {
        return _playerhitStength;
    }
    public float getHitCooldown()
    {
        return _playerhitCooldown;
    }
}
