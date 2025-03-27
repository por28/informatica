using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectionUI : MonoBehaviour
{
    public GameObject abilityPanel; // UI Panel for ability choices
    public Button[] abilityButtons; // Buttons for choosing abilities
    private List<Ability> currentChoices;
    private PlayerAbilities playerAbilities;


    void Start()
    {
        abilityPanel.SetActive(false);
        Time.timeScale = 1f;
    }


    public void ShowAbilityChoices(List<Ability> abilities, PlayerAbilities player)
    {
        Debug.Log("Showing ability choices...");

        if (abilities == null || abilities.Count == 0)
        {
            Debug.LogError("No abilities available! Make sure abilities are added.");
            return;
        }

        abilityPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        currentChoices = abilities;
        playerAbilities = player;

        Debug.Log("Number of abilities available: " + abilities.Count);

        // Loop through the buttons and enable them if abilities are available
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            if (i < abilities.Count)
            {
                abilityButtons[i].gameObject.SetActive(true);
                // Use TextMeshPro component to change text
                abilityButtons[i].GetComponentInChildren<TMP_Text>().text = abilities[i].name;  // Use TMP_Text here instead of Text

                // Capture index for button click listener
                int index = i;
                abilityButtons[i].onClick.RemoveAllListeners();
                abilityButtons[i].onClick.AddListener(() => SelectAbility(index));
            }
            else
            {
                abilityButtons[i].gameObject.SetActive(false);  // Disable buttons that are not in use
            }
        }
    }

    public void SelectAbility(int index)
    {
        if (currentChoices == null || currentChoices.Count == 0)
        {
            Debug.LogError("No abilities available to select!");
            return;
        }

        if (index < 0 || index >= currentChoices.Count)
        {
            Debug.LogError("Invalid index: " + index);
            return;
        }

        Debug.Log("Ability Selected: " + currentChoices[index].name);
        playerAbilities.AddAbility(currentChoices[index]);

        abilityPanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game Unpaused!");
    }

}
