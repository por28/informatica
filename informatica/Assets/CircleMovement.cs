using System.Collections;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform Player; 
    public float radius = 0.5f; 
    public float speed = 2f; 
    public float hideDuration = 5f; 
    private bool isHidden = false; 

    private SpriteRenderer circleRenderer; 
    private Collider2D circleCollider;

    void Start()
    {
        circleRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<Collider2D>();
        Player = GameObject.Find("Player")?.transform;

        StartCoroutine(HideAndShowCoroutine());
    }

    void Update()
    {
        if (!isHidden)
        {
            float x = Player.position.x + radius * Mathf.Cos(Time.time * speed);
            float y = Player.position.y + radius * Mathf.Sin(Time.time * speed);

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