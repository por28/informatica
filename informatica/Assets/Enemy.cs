using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    private float distance;
    public float distanceBetween;
    public float damage = 1;

    Animator animator;
    public ExperienceManager experienceManager;
    

    public float Health{
        set{
            health = value;
            if(health <= 0){
                Defeated();
                experienceManager.AddExperience(5);
            }
        }
        get{
            return health;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            PlayerController Player = other.GetComponent<PlayerController>();

            if(Player != null){
                Player.Health -= damage;
            }
        }
        
        
    }

    public float health = 1;

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }
}
