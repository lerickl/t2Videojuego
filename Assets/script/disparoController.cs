using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparoController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocityX = 15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
     
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(this.gameObject);
           
    }
}
