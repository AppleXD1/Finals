using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public BaseBoss boss;
    public Slider healthBarSlider;
    public Slider BossHPSlider;
    public Slider StaminaBar;
    public float currPlayerHealth; 
    public float currBossHealth;
    public float maxPlayerHP;
    public float maxBossHP;
    public float playerStamina;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindWithTag("Boss").GetComponent<BaseBoss>();

        maxPlayerHP = player.MaxHealth;
        maxBossHP = boss.maxHealth;

        healthBarSlider.maxValue = maxPlayerHP;
        BossHPSlider.maxValue = maxBossHP;

        currPlayerHealth = player.Health;
        currBossHealth = boss.currentHealth;

        healthBarSlider.value = currPlayerHealth;
        BossHPSlider.value = currBossHealth;

        StaminaBar.maxValue = player.MaxStamina;
        StaminaBar.value = player.Stamina;
    }

    // Update is called once per frame
    void Update()
    {
        currPlayerHealth = player.Health;
        currBossHealth = boss.currentHealth;
        playerStamina = player.Stamina;

        healthBarSlider.value = currPlayerHealth;
        BossHPSlider.value = currBossHealth;
        StaminaBar.value = playerStamina;

    }
}
