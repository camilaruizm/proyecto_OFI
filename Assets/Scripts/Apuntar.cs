using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Apuntar : MonoBehaviour {

    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public AudioClip clip;

    // Datos para conectarnos a processing
    internal Boolean socketReady = false;
    TcpClient mySocket;
    NetworkStream theStream;
    StreamReader theReader;
    String Host = "localhost";
    Int32 Port = 5204;
    float tcpX = 0;
    float tcpY = 0;

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
        abrirElSocket();
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
        leerDatosProcessing();
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
                if (nivel == 2)
                {   Debug.Log("hola");
                    GameObject.Find("Button").GetComponentInChildren<Text>().text = "VOLVER AL INICIO";
                }
            }            
        }
        
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
            float x = Remap(tcpX, 0, 640, 0, 1920);
            float y = Remap(tcpY, 0, 480, 0, 1020);

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
    }

    //pasar al siguiente nivel
    public void siguienteNivel()
    {
        p.cambiarPuntos();
        if (nivel != 3)
        {
            SceneManager.LoadScene("Inicio" );
        }
        else
        {
            SceneManager.LoadScene("Inicio");
        }
        
    }

    /**
	 * Leemos los datos que llegan por el socket
	 * esta informacion la envia processing.
	 * */
    public void leerDatosProcessing()
    {
        string informacion = readSocket();
        if (informacion != "")
        {
            string[] partes = informacion.Split(
                new string[] { "," },
                StringSplitOptions.None
            );
            Debug.Log("X=" + partes[0] + " Y=" + partes[1]);
            tcpX = Int32.Parse(partes[0]);
            tcpY = Int32.Parse(partes[1]);
        }
    }

    /**
	 * Crea el socket al puerto e Ip datos.
	 * **/
    public void abrirElSocket()
    {
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    /**
	 * Lee datos del socket
	 * **/
    public String readSocket()
    {
        if (!socketReady)
            return "";
        if (theStream.DataAvailable)
            return theReader.ReadLine();
        return "";
    }

    /**
	 * Cierra el socket
	 * */
    public void closeSocket()
    {
        if (!socketReady)
            return;
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }

    /**
	 * Se mapea los valores obtenidos de 640*480 a 1920*1020
	 * */
    public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;
        var normal = fromAbs / fromMaxAbs;
        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;
        var to = toAbs + toMin;
        return to;
    }
}

