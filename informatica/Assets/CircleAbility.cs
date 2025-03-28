using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/CircleAbility")]
public class CircleAbility : Ability
{
    public GameObject circleCollider;  // Reference to the circle prefab that will move around the player
    public float radius = 0.5f;
    public float speed = 2f;
    public float hideDuration = 5f;

    private GameObject activeCircle;
    private CircleMovement circleMovement;

    public override void Activate(GameObject player)
    {
        // Spawn the circle and make it follow the player
        activeCircle = Instantiate(circleCollider, player.transform.position, Quaternion.identity);
        circleMovement = activeCircle.GetComponent<CircleMovement>();

        if (circleMovement != null)
        {
            circleMovement.Player = player.transform;
            circleMovement.radius = radius;
            circleMovement.speed = speed;
            circleMovement.hideDuration = hideDuration;

            // Call the coroutine from CircleMovement class
            circleMovement.StartCoroutine(circleMovement.HideAndShowCoroutine());
        }
    }
}
