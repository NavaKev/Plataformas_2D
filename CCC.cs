using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CCC : MonoBehaviour
{
    public Transform controladorGolpe;
    public float radioGolpe;
    public int dañoGolpe;
    public float tiempoEntreAtaques;
    public float tiempoSigAtaque;
	private Animator anim;
	public AudioSource audio;
	public AudioClip ataque;
	public Button Btn;


    public static bool atacando = false;

    void Start() {
	    anim = GetComponent<Animator>();
	    audio = GetComponent<AudioSource>();

        
    }


    void Update() {
        if (tiempoSigAtaque < 0.05f && tiempoEntreAtaques > 0 ){
            atacando = false;
        }
        
        if (tiempoSigAtaque > 0 ) {
            tiempoSigAtaque -= Time.deltaTime;
        } 

	    /*if (Input.GetKey(KeyCode.Alpha1) && tiempoSigAtaque <= 0 ) {
            atacando = true ;
	        activaCapa("Atacar");
	        audio.clip = ataque;
	        audio.Play();
            Golpe();
            tiempoSigAtaque = tiempoEntreAtaques;
	    }*/
    }
    
	public void combate(){
		atacando = true;
		activaCapa("Atacar");
		audio.clip = ataque;
		audio.Play();
		Golpe();
		tiempoSigAtaque = tiempoEntreAtaques;
	}

    private void Golpe(){
        if (MovimientoPlayer.dirAtaque == 1){ // Ataca a la derecha 
            anim.SetTrigger("ataqueDerecha");
        } else if (MovimientoPlayer.dirAtaque == 2) { // Ata a la izquierda< 
            anim.SetTrigger("ataqueIzquierda");
        }
    }



    private void activaCapa(string nombre){
        for ( int i = 0; i < anim.layerCount; i++){
            anim.SetLayerWeight(i,0);
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre),1);

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

    private void VerificaGolpe() { // Se llama desde la animacion 
        Collider2D[] objs = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe); //verifica que golpeó
        foreach (Collider2D colisionador in objs){
            if (colisionador.CompareTag("enemigo")) { 
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }
    
}

