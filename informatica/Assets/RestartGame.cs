using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class RestartGame : MonoBehaviour
{
    public GameObject DeathScreen;
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        
    }
    public IEnumerator DeathScreenLoader()
    {
        yield return new WaitForSeconds(2);
        DeathScreen.SetActive(true);
    }
    void Start()
    {
        DeathScreen.SetActive(false);
    }

}
