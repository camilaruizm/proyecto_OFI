using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iniciar()
    {
        SceneManager.LoadScene("Nivel 2");
    }

    public void instrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }

    public void regresar()
    {
        SceneManager.LoadScene("Inicio");
    }
}
