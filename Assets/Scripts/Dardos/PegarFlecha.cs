using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PegarFlecha : MonoBehaviour {

    //sonido
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public AudioClip clip;
    //puntos
    private Text puntos;
    private Text puntosT;
    private static int pts=0;
    private bool esPuntos = false;
    //
    Rigidbody rigiBd;    
    private float tiempo = 0;
    private Camera Cam;
    String Tag;

    // Use this for initialization
    void Start () {
        rigiBd = GetComponent<Rigidbody>();
        puntos = GameObject.Find("Puntos").GetComponent<Text>();
        puntosT = GameObject.Find("PuntosTotal").GetComponent<Text>();
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        puntos.enabled = false;
        gameObject.AddComponent<AudioSource>();        
    }
	
	// Update is called once per frame
	void Update () {
        if (esPuntos)
        {
            
            switch (Tag)
            {
                case "100":
                    puntos.enabled = true;
                    puntos.text = "+ 100";                    
                    puntos.color = Color.cyan;
                    break;
                case "200":
                    puntos.enabled = true;
                    puntos.text = "+ 200";
                    puntos.color = Color.red;
 
                    break;
                case "300":
                    puntos.enabled = true;
                    puntos.text = "+ 300";
                    puntos.color = Color.yellow;
                    break;
            }
            tiempo += Time.deltaTime;
            
            if (tiempo >= 2)
            {
                puntos.enabled = false;
                tiempo = 0;                
                esPuntos = false;
            }
        }
	}   
    
    void OnCollisionEnter(Collision collision)
    {
        pegar();        
        Collider other = collision.GetComponent<Collider>();
        transform.parent = other.transform;

        if (other.tag == "100" || other.tag == "200" || other.tag == "300")
        {
            transform.parent = other.transform;
            esPuntos = true;
            Tag = other.tag;
            

            switch (Tag)
            {
                case "100":
                    pts = pts + 100;
                    puntosT.text = pts.ToString();
                    source.PlayOneShot(clip, 1f);
                    break;
                case "200":
                    pts = pts + 200;
                    puntosT.text = pts.ToString();
                    source.PlayOneShot(clip, 0.4f);
                    break;
                case "300":
                    pts = pts + 300;
                    puntosT.text = pts.ToString();
                    source.PlayOneShot(clip, 0.2f);
                    break;
            }

        }
    }
    
    private void cambiarPosicion()
    {
        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
        Vector2 ViewportPosition = Cam.WorldToViewportPoint(transform.position);
        Vector2 wP = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        puntos.transform.position = new Vector3(wP.x, wP.y, 100f);
    }

    void pegar()
    {
        GetComponent<Collider>().enabled = false;
        transform.GetComponent<Flecha>().enabled = false;
        rigiBd.velocity = Vector3.zero;
        rigiBd.useGravity = false;
        rigiBd.isKinematic = true;
    }

    public int darPuntos()
    {
        return pts;
    }

    public void cambiarPuntos()
    {
        pts = 0;
    }

    public int getPuntos()
    {
        return pts;

    }
}
