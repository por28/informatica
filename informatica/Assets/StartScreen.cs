using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreen;
    
    public void StartGame(){
        SceneManager.LoadScene(0);
    }
}

