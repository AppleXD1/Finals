using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class GreatSword : MonoBehaviour
{
    public float Damage;
    public bool hasSwing = false;
    BoxCollider hitBox;
    private HashSet<BaseBoss> hitEnemies = new HashSet<BaseBoss>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseBoss enemy = other.GetComponent<BaseBoss>();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (enemy != null && other.gameObject.CompareTag("Boss") && player.isAttacking && !hasSwing)
        {
            hasSwing = true;
            DisableBoxTrigger();
            enemy.TakeDamage(Damage);
            Debug.Log("Hit " + enemy.name + " for " + Damage);
            Wait();
            enemy.isTakenDamage = false;
        }
    }

    public void EnableBoxTrigger()
    {
        hitEnemies.Clear();
        hitBox.enabled = true;
        hasSwing = false;
    }

    public void DisableBoxTrigger()
    {
        hitBox.enabled = false;
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2f);
        EnableBoxTrigger();
    }

    void Wait()
    {
        StartCoroutine(WaitSeconds());
    }
}
