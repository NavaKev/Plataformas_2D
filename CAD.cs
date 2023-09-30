using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAD : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    public float tiempoSigAtaque;
    public float tiemposEntreAtaques;
    public Transform puntoEmision;
    private Animator anim;
	public static int dirDisparo = 0; // Izquierda o Derecha 
	public AudioSource audio;
	public AudioClip ataqueDis;

    public static bool disparando = false;

	void Start() {
		audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        
    }

    void Update() {
        if (tiempoSigAtaque < 0.05f && tiemposEntreAtaques > 0 ){
            disparando = false;
        }
        
        if (tiempoSigAtaque > 0) {
            tiempoSigAtaque -= Time.deltaTime;
        }

	    /* if (Input.GetKey(KeyCode.Alpha2) && tiempoSigAtaque <= 0){
            disparando = true;
	        activaCapa("Atacar");
	        audio.clip = ataqueDis;
	        audio.Play();
            Dispara();
            tiempoSigAtaque = tiemposEntreAtaques;
	    }*/
        
    }
    
	public void Disparo(){
		disparando = true;
		activaCapa("Atacar");
		audio.clip = ataqueDis;
		audio.Play();
		Dispara();
		tiempoSigAtaque = tiemposEntreAtaques;
	}

    private void Dispara(){
        if (MovimientoPlayer.dirAtaque == 1) { // Derecha 
            anim.SetTrigger("disparaDerecha");
        }
        else if (MovimientoPlayer.dirAtaque == 2) { // Izquierda
            anim.SetTrigger("disparaIzquierda");
        }
    }

    private void activaCapa(string nombre){
        for (int i = 0; i < anim.layerCount; i++) {
            anim.SetLayerWeight(i,0); // Ambos layers  con weight en 0
        }

        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);

    }

     private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoEmision.position, 0.25f);
    }

    private void EmiteProyectil(){ // Se llama desde la animacion 
        dirDisparo = MovimientoPlayer.dirAtaque;
        Instantiate(proyectil, puntoEmision.position, transform.rotation); 

    }
}
