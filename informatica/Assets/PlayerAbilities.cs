using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public List<Ability> allAbilities; // Assign ALL available abilities in Inspector
    public List<Ability> acquiredAbilities = new List<Ability>(); // Player's chosen abilities
    public AbilitySelectionUI abilitySelectionUI; // UI for selecting abilities
    private int currentAbilityIndex = 0; // Current selected ability

    void Start()
    {
        acquiredAbilities.Clear();
    }

    

    void Update()
    {
        if (acquiredAbilities.Count > 0)
        {
            // Switch abilities with number keys
            if (Input.GetKeyDown(KeyCode.Alpha1) && acquiredAbilities.Count > 0) currentAbilityIndex = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2) && acquiredAbilities.Count > 1) currentAbilityIndex = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3) && acquiredAbilities.Count > 2) currentAbilityIndex = 2;

            // Use current ability with Space key
            if (Input.GetKeyDown(KeyCode.Space))
            {
                acquiredAbilities[currentAbilityIndex].Activate(gameObject);
                Debug.Log("Activated: " + acquiredAbilities[currentAbilityIndex].name);
            }
        }
    }

    // Call this when the player levels up
    public void LevelUp()
    {
        if (abilitySelectionUI != null)
        {
            // Choose 3 random abilities from allAbilities list
            List<Ability> randomAbilities = GetRandomAbilities(3);
            abilitySelectionUI.ShowAbilityChoices(randomAbilities, this);
        }
    }

    // Adds the selected ability
    public void AddAbility(Ability newAbility)
    {
        if (!acquiredAbilities.Contains(newAbility))
        {
            acquiredAbilities.Add(newAbility);
            Debug.Log("New Ability Acquired: " + newAbility.name);
        }
    }

    // Helper function to get random abilities
    private List<Ability> GetRandomAbilities(int count)
    {
        List<Ability> randomAbilities = new List<Ability>();
        List<Ability> copy = new List<Ability>(allAbilities);

        for (int i = 0; i < count && copy.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, copy.Count);
            randomAbilities.Add(copy[randomIndex]);
            copy.RemoveAt(randomIndex);
        }
        return randomAbilities;
    }
}
