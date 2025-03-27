using UnityEngine;

[CreateAssetMenu(fileName = "New Fireball Ability", menuName = "Abilities/Fireball")]
public class FireballAbility : Ability
{
    public GameObject fireballPrefab; // Assign Fireball prefab in Inspector
    public float fireballSpeed = 10f;

    public override void Activate(GameObject player)
    {
        Debug.Log("Fireball Activated!");

        // Spawn a fireball in front of the player
        GameObject fireball = Instantiate(fireballPrefab, player.transform.position + player.transform.forward, Quaternion.identity);
        
        // Give it velocity
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = player.transform.forward * fireballSpeed;
        }
    }
}
