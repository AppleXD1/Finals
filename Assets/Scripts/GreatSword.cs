using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GreatSword : MonoBehaviour
{
    public float Damage;
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

        if (enemy != null && other.gameObject.CompareTag("Boss"))
        {
            enemy.TakeDamage(Damage);
            Debug.Log("Hit " + enemy.name + " for " + Damage);
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
