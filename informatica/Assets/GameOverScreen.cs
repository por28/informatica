using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;
    public GameObject gameOverScreen;
    public int currentLevel, totalExperience;

    public void Setup(int totalExperience)
    {
        gameOverScreen.SetActive(true);
        pointsText.text = totalExperience.ToString() + " POINTS";
    }
}
