using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 1f;
    public float collisionOffset = 0.05f;
    
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 6f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;
    public float health = 2;
    int totalExperience;

    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public TrailRenderer tr;
    public Transform arrow;
    public GameObject Enemy;
    public GameObject GameOverScreen;
   
    
    Vector2 movementInput;
    Vector2 mousePos;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    
    public float Health{
        set{
            health = value;
            if(health <= 0){
                GameOver();
            }
        }
        get{
            return health;
        }
        
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && canDash )
        {
            StartCoroutine(Dashleft());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && canDash )
        {
            StartCoroutine(Dashright());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && canDash )
        {
            StartCoroutine(Dashup());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && canDash )
        {
            StartCoroutine(Dashdown());
        }
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if(canMove){
            if(movementInput != Vector2.zero){
                bool success = TryMove(movementInput);
                
                if(!success){
                    success = TryMove(new Vector2(movementInput.x, 0));  
                }

                if(!success){
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("isMoving", success);
            } else{
                animator.SetBool("isMoving", false);
            }

            Vector2 lookDirection = mousePos - rb.position;

            if(lookDirection.x < 0){
                spriteRenderer.flipX = true;
            } else if(lookDirection.x > 0){
                spriteRenderer.flipX = false;
            }
        } 

         
    }

       
 

    private bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    movespeed * Time.fixedDeltaTime + collisionOffset);

            if(count == 0){
                rb.MovePosition(rb.position + direction * movespeed * Time.fixedDeltaTime);
                return true;
            } else{
                return false;
            }
        }else{
            return false;
            }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack(){
        if(spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        } else{
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack(){
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }

    private IEnumerator Dashleft()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = Vector2.zero;
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator Dashright()
    {
        canDash = false;
        isDashing = true;
        
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = Vector2.zero;
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator Dashup()
    {
        canDash = false;
        isDashing = true;
        
        rb.velocity = new Vector2(0f, transform.localScale.y * dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = Vector2.zero;
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator Dashdown()
    {
        canDash = false;
        isDashing = true;
        
        rb.velocity = new Vector2(0f, -transform.localScale.y * dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = Vector2.zero;
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
 
}
