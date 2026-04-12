using UnityEngine;

public class DemonPunch : MonoBehaviour
{
    public BaseBoss boss;
    public float damage = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boss = GetComponent<BaseBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if(other.gameObject.CompareTag("Player") && boss.isAttacking && !boss.speicalAttack)
        {
            player.TakenDamage(damage);
        }
    }
}
