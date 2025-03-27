using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/CircleAbility")]
public class CircleAbility : Ability
{
    public GameObject circlePrefab;  // Reference to the circle prefab that will move around the player
    public float radius = 5f;
    public float speed = 50f;
    public float hideDuration = 5f;

    private GameObject activeCircle;
    public CircleMovement circleMovement;

    public override void Activate(GameObject player)
    {
        // Spawn the circle and make it follow the player
        activeCircle = Instantiate(circlePrefab, player.transform.position, Quaternion.identity);
        CircleMovement circleMovement = activeCircle.GetComponent<CircleMovement>();

        if (circleMovement != null)
        {
            circleMovement.player = player.transform;
            circleMovement.radius = radius;
            circleMovement.speed = speed;
            circleMovement.hideDuration = hideDuration;

            // Start the circle's Hide and Show logic (from CircleMovement script)
            activeCircle.SetActive(true);
            // Call the coroutine from CircleMovement class
            circleMovement.StartCoroutine(circleMovement.HideAndShowCoroutine());
        }
    }
}
