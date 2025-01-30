using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int totalExperience;
    public GameOverScreen GameOverScreen;
    [SerializeField] TextMeshProUGUI pointsText;

    

    public void GameOver()
    {
        GameOverScreen.Setup(totalExperience);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
