using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioSonido : MonoBehaviour {

    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public AudioClip clip;

    void Start () {
        gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.PlayOneShot(clip, 0.4f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
