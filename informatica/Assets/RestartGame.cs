using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class RestartGame : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject StartScreen;
    public GameObject InfoScreen;

    public void Restart()
    {
        SceneManager.LoadScene(0);
        StartScreen.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Info()
    {
        InfoScreen.SetActive(true);
        StartScreen.SetActive(false);
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
        StartScreen.SetActive(false);
    }

}
