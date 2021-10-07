using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiempocontroller : MonoBehaviour
{
    public Text Tiempotxt; 
    public float Tiempo = 0.0f;
    public bool DebeAumentar = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Se comprueba si debe aumentar el valor primero...
        DebeAumentar = (Tiempo <= 0.0f)  ? true : false;

        // Luego se efectua el aumento.
        if (DebeAumentar) Tiempo += Time.deltaTime;
        else Tiempo -= Time.deltaTime;

        // Se asigna el color dependiendo del tiempo restante.
        //Tiempotxt.color  = (Tiempo <= 30.0f) ? Color.Red : Color.Green;

        Tiempotxt.text = "Tiempo:" + " " + Tiempo.ToString("f0");
    }
}
