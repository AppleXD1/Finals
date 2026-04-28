using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class GreatSword : MonoBehaviour
{
    public float Damage = 10f;

    private BoxCollider hitBox;

    private HashSet<BaseBoss> hitEnemies = new HashSet<BaseBoss>();

    void Start()
    {
        hitBox = GetComponent<BoxCollider>();
        hitBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitBox.enabled) return;

        BaseBoss boss = other.GetComponent<BaseBoss>();

        if (boss != null && !hitEnemies.Contains(boss))
        {
            hitEnemies.Add(boss);

            boss.currentHealth -= Damage;

            Debug.Log("Hit boss once");
        }
    }

    
    public void EnableBoxTrigger()
    {
        hitEnemies.Clear(); 
        hitBox.enabled = true;
    }

    
    public void DisableBoxTrigger()
    {
        hitBox.enabled = false;
    }
}
