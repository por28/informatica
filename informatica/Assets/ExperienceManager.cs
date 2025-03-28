using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;

    public int currentLevel, totalExperience = 0;
    public int previousLevelsExperience, nextLevelsExperience;
    public GameOverScreen GameOverScreen;
    public List<GameObject> enemies;
    public PlayerAbilities playerAbilities;
    [SerializeField] TextMeshProUGUI pointsText;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;


    void Start()
    {
        enemies.Clear();
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(enemyObjects);
        currentLevel = 0;
        totalExperience = 0;
        UpdateLevel();
        playerAbilities = FindObjectOfType<PlayerAbilities>();
    }

    void Update() 
    {
        pointsText.text = totalExperience.ToString() + " POINTS";
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    void CheckForLevelUp()
    {
        if(totalExperience >= nextLevelsExperience)
        {
            currentLevel++;
            UpdateLevel();
            playerAbilities.LevelUp();


            // Start level up sequence... Possibly vfx?
        }
    }

    void UpdateLevel()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }

    void UpdateInterface()
    {
        int start = totalExperience - previousLevelsExperience;
        int end = nextLevelsExperience - previousLevelsExperience; 

        levelText.text = currentLevel.ToString();
        experienceText.text = start + " / " + end + " ";
        experienceFill.fillAmount = (float)start / (float)end;
    }
}