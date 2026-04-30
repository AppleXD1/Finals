using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Retry1 : MonoBehaviour
{
   
    public GameObject Button;

    private void Start()
    {
        

    }
    private void Update()
    {
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ReloadScene()
    {
        // Loads the active scene by its string name
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Testing");
    }
}
