using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    GameObject Player;
    public float speed;
    private float distance;
    public float distanceBetween;
    public int damage = 1;
    Rigidbody2D rb;
    public Collider2D collider2d;

    Animator animator;
    ExperienceManager experienceManager;
    public float health = 50;

    public float Health{
        set{
            health = value;
            if(health <= 0){
                Defeated();
                Debug.Log("SlimeBoss health is zero or below, calling Defeated");
                
            }
        }
        get{
            return health;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        
        if (other.CompareTag("Player"))
        {
            PlayerController Player = other.GetComponent<PlayerController>();
            if (Player != null)
            {
                Player.Health -= damage;
            }
        }
        else{
            Debug.Log("triggered");
            health -= 1;
        }
    }

    

    public void Defeated(){
        Debug.Log("SlimeBoss Defeated!");
        experienceManager.AddExperience(50);
        collider2d.enabled = false;
        animator.SetTrigger("Defeated");
        Destroy(gameObject);
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        collider2d.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Player.GetComponent<Rigidbody2D>().position - rb.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
