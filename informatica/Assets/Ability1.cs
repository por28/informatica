using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/HpUp")]
public class HpUp : Ability
{
    public int healthIncrease = 10;
    GameObject healthbar;

    public override void Activate(GameObject Player)
    {  
        PlayerController playerController = Player.GetComponent<PlayerController>();
        HealthBar healthBar = healthbar.GetComponentInChildren<HealthBar>();

        if (playerController != null)
        {
            playerController.Health = healthIncrease;
            healthBar.SetMaxHealth(playerController.health);
        }
    }
}
