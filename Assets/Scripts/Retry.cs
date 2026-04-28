using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    public Player player;
    public Button Button;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.isDead)
        {
            Button.enabled = true;
        }
        else
        {
            Button.enabled = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ReloadScene()
    {
        // Loads the active scene by its string name
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
