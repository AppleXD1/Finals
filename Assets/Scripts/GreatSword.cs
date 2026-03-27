using UnityEngine;

public class GreatSword : MonoBehaviour
{
    public float Damage;
    BoxCollider hitBox;
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
        Debug.Log("Hit");
    }

    public void EnableBoxTrigger()
    {
        hitBox.enabled = true;
    }

    public void DisableBoxTrigger()
    {
        hitBox.enabled = false;
    }
}
