using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninja2Controller : MonoBehaviour
{
  private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private int correr=1;
    float velocityX=5f;
    int vida = 4;
    public robotController robot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
 
    }
    float count=0f;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("vida: "+vida);
    
        count += Time.deltaTime;
        if(count<3){
            CambiarAnimacion(correr);
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
             spriteRenderer.flipX = false;
        }
        if(count>3 && count<6){
            CambiarAnimacion(correr);
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
             spriteRenderer.flipX = true;
        }
        if(count>6){
            count = 0;
        }
        

    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
    

        if (collision.gameObject.tag=="normal")
        {
            vida=vida-1;
          if (vida<=0){              
                Destroy(this.gameObject);
                robot=GameObject.FindGameObjectWithTag("robot").GetComponent<robotController>(); 
                robot.reducirEnemigo();}

   
        }
         if (collision.gameObject.tag=="especial")
        {
            vida=vida-2;
            if (vida<=0){   
                Destroy(this.gameObject);
                    robot=GameObject.FindGameObjectWithTag("robot").GetComponent<robotController>(); 
                robot.reducirEnemigo();
                }
   
        }
   
    }
     private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
}
