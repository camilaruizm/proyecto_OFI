using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Apuntar : MonoBehaviour {

    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public AudioClip clip;
    public int jugador;
    
    int contador = 0;
    // Referencias
    public GameObject referenciaTarget;
    private GameObject FlechaPrefab;
    public GameObject arco;
    public Camera cam;
    public GameObject flechaEstatica;


    // Parametros del script
    Rigidbody rigidB;
    public float distanciaTarget = 1.5f;
    Boolean flechaActiva = true;
    Boolean flechaActiva1 = true;

    public float tiempoDisparo = 0.3f;
    private float proximoDisparo = 0.0f;
    public float fuerzaDisapro = 300;

    Rigidbody rb;
    GameObject nuevaFlecha;

    //Flechas UI
    private Text flechas;
    private Text puntos;
    public GameObject panelFallo;
    public GameObject panelVictoria;
    public int flechasC = 5;

    //puntos
    public int ptsMaximos = 800;

    //metodos de otras calses
    PegarFlecha p = new PegarFlecha();

    //nivel
    public int nivel=2;
    private bool fin;

    //sonido
    public AudioClip desierto;
    public AudioClip ganar;
    public AudioClip perder;
    public AudioClip fondo;

    // Start is called before the first frame update
    void Start()
    {
        FlechaPrefab = Resources.Load("Flecha") as GameObject;
        
        rigidB = GetComponent<Rigidbody>();
        gameObject.AddComponent<AudioSource>();
        flechas = GameObject.Find("Flechas").GetComponent<Text>();
        puntos = GameObject.Find("PuntosMaximo").GetComponent<Text>();
        panelFallo.SetActive(false);
        panelVictoria.SetActive(false);
        flechas.text = flechasC.ToString();
        puntos.text = "/ " + ptsMaximos.ToString();

        fin = true;

        source.loop = true;
        source.PlayOneShot(desierto, 0.7f);
        source.PlayOneShot(fondo, 0.2f);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
      
        Apuntado();

        //Correr tiempo para nueva flecha y hacer parabola con la flecha
        if (flechaActiva==false && Time.time > proximoDisparo)
        {               
            flechaEstatica.SetActive(true);
            flechaActiva = true;                     
        }

        if (!flechaActiva1)
        {
            caidaFlecha();
        }
        if (fin)
        {
            if (flechasC < 0)
            {
                p.cambiarPuntos();
                panelFallo.SetActive(true);
                source.Stop();
                source.PlayOneShot(perder, 1.5f);
                fin = false;
                Time.timeScale = 0;
            }
        }
        

        if (fin)
        {
            if (p.darPuntos() >= ptsMaximos)
            {
                panelVictoria.SetActive(true);
                source.Stop();
                Time.timeScale = 0;
                source.PlayOneShot(ganar, 0.5f);
                fin = false;

                int jugadoraso = p.getPuntos();

                if (contador == 0)
                {

                    Debug.Log("el jugador 1 obtuvo un puntaje de: " + jugadoraso);
                    jugadoraso = 0;

                }
                if (contador != 0)
                {

                    Debug.Log("el jugador 2 obtuvo un puntaje de: " + jugadoraso);
                    jugadoraso = 0;

                }

                if (nivel == 2)
                {
                    Debug.Log("hola");
                    GameObject.Find("Button").GetComponentInChildren<Text>().text = "JUGAR OTRA VEZ";
                }
            }            
        }

       Debug.Log(contador);
       
    }

    /**
     * Disparar la fecla en la direccion determinada.
     */
    void DisparoFlecha(Vector3 origen, Vector3 direccion, float velocidad)
    {
        if (Input.GetMouseButtonDown(0))
        {
            flechasC -= 1;
            if (flechasC >= 0)
            {
                flechas.text = flechasC.ToString();
            }            

            source.PlayOneShot(clip, 1.5f);
            //Desaparece flecha y corre tiempo para aparecer nuevamente
            flechaEstatica.SetActive(false);
            flechaActiva=false;
            flechaActiva1 = false;
            proximoDisparo = Time.time + tiempoDisparo;

            // Nace una flecha
            nuevaFlecha = Instantiate(FlechaPrefab) as GameObject;
            nuevaFlecha.transform.position = origen;
            nuevaFlecha.transform.LookAt(-direccion);

            // Aplicamos una fuerza
            rb = nuevaFlecha.GetComponent<Rigidbody>();
            rb.AddForce(direccion * velocidad);            
        }              
    }

    /**
     * Metodo para la caida en parabola de la flecha
     */
    void caidaFlecha(){
        float velocidadY = rb.velocity.y;
        float velocidadX = rb.velocity.x;
        float velocidadZ = rb.velocity.z;
        float velocidadesCombinadas = Mathf.Sqrt(velocidadX*velocidadX+velocidadZ*velocidadZ);
        float anguloCaida = -1 * Mathf.Atan2(velocidadY, velocidadesCombinadas)*180/Mathf.PI;
        nuevaFlecha.transform.eulerAngles = new Vector3(anguloCaida, nuevaFlecha.transform.eulerAngles.y, nuevaFlecha.transform.eulerAngles.z);
    }

    void Apuntado()
    {
        if (flechasC >= 0 && p.darPuntos() < ptsMaximos)
        {
            

            //Obtener la direccion del target con un rayo
            //Ray mouseRay = cam.ScreenPointToRay(new Vector3(x,y,0));
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 posicionTarget = mouseRay.origin + mouseRay.direction * distanciaTarget;

            // Depurar vector donde se apunta
            this.referenciaTarget.transform.position = posicionTarget;
            transform.LookAt(posicionTarget);
            DisparoFlecha(transform.position, -transform.forward, -fuerzaDisapro);
        }               
    }

    //reinicia el nivel
    public void reiniciar()
    {
        SceneManager.LoadScene("Nivel " + nivel.ToString());
        contador = contador + 1;

    }

    //pasar al siguiente nivel
    public void siguienteNivel()
    {
        p.cambiarPuntos();
        if (nivel != 3)
        {
            SceneManager.LoadScene("Nivel 2" );
        }
        else
        {
            SceneManager.LoadScene("Inicio");
        }
        contador = contador + 1;
    }

   
}

