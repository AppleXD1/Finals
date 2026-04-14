using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 8f;
    public float lifeTime = 5f;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player body = other.GetComponent<Player>();
            if (body != null)
            {
                body.Health -= damage;
                Destroy(gameObject);
            }
        }

        
    }
}
