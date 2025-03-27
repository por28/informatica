using System.Collections;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform player; 
    public float radius = 5f; 
    public float speed = 50f; 
    public float hideDuration = 5f; 
    private bool isHidden = false; 

    private SpriteRenderer circleRenderer; 
    private Collider2D circleCollider;

    void Start()
    {
        circleRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<Collider2D>();

        StartCoroutine(HideAndShowCoroutine());
    }

    void Update()
    {
        if (!isHidden)
        {
            float x = player.position.x + radius * Mathf.Cos(Time.time * speed);
            float y = player.position.y + radius * Mathf.Sin(Time.time * speed);

            transform.position = new Vector3(x, y, 0); 
        }
    }

    public IEnumerator HideAndShowCoroutine()
    {
        circleRenderer.enabled = true;
        circleCollider.enabled = true;  
        isHidden = false;

        yield return new WaitForSeconds(hideDuration);

        circleRenderer.enabled = false;
        circleCollider.enabled = false;  
        isHidden = true;

        yield return new WaitForSeconds(hideDuration);
        
        StartCoroutine(HideAndShowCoroutine());
    }
}