using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class robotController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    public Text Enemigostext ;
    public int Enemyscore=5;
    private int vidas=3; 
    public Text vidastext ;
    public float fuerzaSalto = 8;
    public float velocidad = 5;
    private bool EstaSaltando = false;
    private bool EstaMuerto = false;
    private bool EstaDestruido = false;
    private bool EstaDisparando=false;
    private bool EstaDeslisando=false;
    private bool EstaCorriendo=false;
    private const int ANIMATION_QUIETO = 0;
    private const int DEATH = 1;
    private const int ANIMATION_SLIDE = 2;
    private const int ANIMATION_CORRERSHOT = 3;
    private const int ANIMATION_CORRER = 4;
    private const int ANIMATION_SALTAR = 5;
    private const int SHOT = 6;
    private float timeshot = 0f;
    public GameObject disparonormalIzquierda;
    public GameObject disparonormalDerecha;
    public GameObject disparoEspecialIzquierda;
    public GameObject disparoEspecialDerecha;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        vidastext.text = "Vidas: " + vidas;
        Enemigostext.text = "Enemigos: " + Enemyscore;
        Debug.Log("tiempo: "+timeshot);
        Debug.Log("enemigos: "+Enemyscore);
        if(actualizapuntaje==true){
            Enemyscore--;
            Enemigostext.text = "Enemigos: " + Enemyscore;
            actualizapuntaje=false;
        }

         if(vidas<0){            
            CambiarAnimacion(DEATH);
            StartCoroutine("esperar");
            Destroy(this.gameObject, 1f);
            SceneManager.LoadScene(this.gameObject.scene.name);
   
        }
        if (EstaMuerto == false & EstaDestruido==false)
        {
            if (Input.GetKey(KeyCode.Space) && !EstaSaltando)
            {
                CambiarAnimacion(ANIMATION_SALTAR);
                Saltar();
                EstaSaltando = true;
                 
            }
            
            else if (Input.GetKey(KeyCode.D))//Si presiono la tecla rigtharrow voy a ir hacia la derecha
            {
                EstaCorriendo = true;   
                rb.velocity = new Vector2(velocidad, rb.velocity.y);//velocidad de mi objeto
        //Accion correr 
                if(EstaSaltando==true){
                    CambiarAnimacion(ANIMATION_SALTAR);
                }
                else{
                    CambiarAnimacion(ANIMATION_CORRER);
                }
                /////////////////////////////////////////////
                if (Input.GetKey(KeyCode.F))
                {
                  
                    CambiarAnimacion(ANIMATION_CORRERSHOT);
                
                    timeshot += Time.deltaTime;       
            
                }
                spriteRenderer.flipX = false;//Que mi objeto mire hacia la derecha
                if(Input.GetKey(KeyCode.C))
                {
                    
                    CambiarAnimacion(ANIMATION_SLIDE);
                }
                 if (Input.GetKey(KeyCode.Space) && !EstaSaltando)
                {
                    EstaSaltando = true;
                    CambiarAnimacion(ANIMATION_SALTAR);
                    Saltar();
                    EstaSaltando = true;
                }
            }
            
            else if (Input.GetKey(KeyCode.A))
            {
                EstaCorriendo = true; 
                rb.velocity = new Vector2(-velocidad, rb.velocity.y);
                if(Input.GetKey(KeyCode.C))
                {
                    EstaDeslisando= true;
                    CambiarAnimacion(ANIMATION_SLIDE);
                     
                }
              
                if(EstaSaltando==true){
                    CambiarAnimacion(ANIMATION_SALTAR);
                }
                if(EstaDeslisando==false){
                    CambiarAnimacion(ANIMATION_CORRER);
                }  

                
                if (Input.GetKey(KeyCode.F))
                {
                  
                    CambiarAnimacion(ANIMATION_CORRERSHOT);
                
                    timeshot += Time.deltaTime;       
            
                }
                spriteRenderer.flipX = true;
              
                 if (Input.GetKey(KeyCode.Space) && !EstaSaltando)
                {
                    CambiarAnimacion(ANIMATION_SALTAR);
                    Saltar();
                    EstaSaltando = true;
                }
          
            } else if(vidas>0&&EstaSaltando==false)
            {
                EstaCorriendo=false;
                 
                CambiarAnimacion(ANIMATION_QUIETO);//Metodo donde mi objeto se va a quedar quieto
                rb.velocity = new Vector2(0, rb.velocity.y);//Dar velocidad a nuestro objeto
            }
         //
             if(Input.GetKey(KeyCode.C))
            {
                EstaDeslisando=true;
                CambiarAnimacion(ANIMATION_SLIDE);
            }
            if(Input.GetKeyUp(KeyCode.C))
            {
                
                EstaDeslisando=false;
            }
            if (Input.GetKey(KeyCode.F))
            {
                EstaDisparando = true;
                if(EstaCorriendo==true){
                    CambiarAnimacion(ANIMATION_CORRERSHOT);
                }else{CambiarAnimacion(SHOT);}
                 
                
                timeshot += Time.deltaTime;       
          
            }
            if (Input.GetKeyUp(KeyCode.F))
                {
                    EstaDisparando = false;
                    disparo();
                    
                }
 
        }
             
    }
    void OnCollisionEnter2D(Collision2D other){
        EstaSaltando = false;
        if(other.gameObject.tag=="enemy"||other.gameObject.tag=="acido"||other.gameObject.tag=="puas"){
            vidas--;
       
        }
         if(other.gameObject.tag=="llave"){
            actualizapuntaje=true;
            SceneManager.LoadScene("escena2");
            Debug.Log("llave tomada");
        }
       
    }
   
    
    private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
    private void Saltar()
    {
        CambiarAnimacion(ANIMATION_SALTAR);
        rb.velocity = Vector2.up * fuerzaSalto;//Vector 2.up es para que salte hacia arriba
    }
    IEnumerator esperar(){
        yield return new WaitForSeconds(5);
    }
    bool actualizapuntaje=false; 
    public void reducirEnemigo(){
    
        actualizapuntaje=true;
    
    }
    void disparo(){
           if (timeshot < 2f)
            {
                var bullet = spriteRenderer.flipX ? disparonormalIzquierda : disparonormalDerecha;
                if(spriteRenderer.flipX){
                    var position = new Vector2(transform.position.x-2.5f, transform.position.y);
                    var rotation = disparonormalIzquierda.transform.rotation;
                    Instantiate(bullet, position, rotation);
                }else{
                    var position = new Vector2(transform.position.x+2.5f, transform.position.y);
                    var rotation = disparonormalDerecha.transform.rotation;
                    Instantiate(bullet, position, rotation);
                }
                timeshot = 0f;
            }
            if (timeshot > 2f )
            {
                var bullet = spriteRenderer.flipX ? disparoEspecialIzquierda : disparoEspecialDerecha;
                if(spriteRenderer.flipX){
                    var position = new Vector2(transform.position.x-3.5f, transform.position.y);
                    var rotation = disparoEspecialIzquierda.transform.rotation;
                    Instantiate(bullet, position, rotation);
                }else{
                    var position = new Vector2(transform.position.x+3.5f, transform.position.y);
                    var rotation = disparoEspecialDerecha.transform.rotation;
                    Instantiate(bullet, position, rotation);
                }
                  timeshot = 0f;
            }
            
    }
}
