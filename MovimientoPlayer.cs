using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{

    // Variables de clase 

    public Rigidbody2D rb;
    public Animator anim;

    private Vector2 dirMov; //izquierda o derecha  
    [SerializeField] private float velMov, velSalto; //velocidades movimiento y salto (Se introducen los valores en unity)
    private float movX = 0; //Izq = -1  o der = 1, 0 = Sin movimiento 
    private int numSaltos; // Permitira 1 o 2 saltos 
	private bool estaSaltando = false;
	private bool SaltoIzq = false;
	private bool Daño = false;
    public static int dirIdle = 1; // Anim estatica hacia la izquierda o derecha 
	public static int dirAtaque = 0; // 0- Sin atacar, 1- Atacar der, 2- Ataca 
	public static int vida;
	public Joystick jostick;
	
	public AudioSource audio;
	public AudioClip jump;


    void Start() {
	    anim.SetBool("saltando",  false );
	    anim.SetBool("saltandoIzq", false);
	    anim.SetBool("Daño", false);
	    audio = GetComponent<AudioSource>();
        numSaltos = 0;
        
    }

    void FixedUpdate(){
        Movimiento();
    }

	public void Salto(){
		//if (Input.GetButtonDown("Jump")) { //Si esta saltando
		    anim.SetBool("saltando",true);
            if (numSaltos <= 1) { //Permite hasta salto doble
                numSaltos++;
	            ColisionSuelo.estaEnSuelo = false; //Cuando salta no esta en suelo
                rb.velocity = new Vector2(rb.velocity.x, velSalto); //Conserva vel Horizontal, y desplaza en vertical velSalto unidades
	            if (dirIdle == 1) {
		            Debug.Log("Saltando = true");
		            anim.SetBool("saltando", true);
		            audio.clip =jump;
		            audio.Play();
	            } else if (dirIdle == -1) {
		            Debug.Log("SaltandoIzq = true");
		            anim.SetBool("saltandoIzq", true );
	                audio.clip =jump;
	                audio.Play();
		            //}
	            
	            ActualizaCapa();
            }
	           
	    } else {
		    if (ColisionSuelo.estaEnSuelo == true) {
		    	anim.SetBool("saltando", false);
		    	anim.SetBool("saltandoIzq",false);
		    	estaSaltando = false;
                numSaltos = 0;
            }
        }

        
    }

	private void Movimiento() {
		movX = jostick.Horizontal;
	    //movX = Input.GetAxisRaw("Horizontal");
        dirMov = new Vector2(movX, 0).normalized;
        rb.velocity = new Vector2(dirMov.x * velMov, rb.velocity.y);

        if (movX != 0 ){ //Se mueve
            anim.SetBool("corriendo", true );
            AnimacionesPlayer(dirMov.x);
            if (movX > 0){ // Derecha
                dirIdle = 1; // der
                dirAtaque = 1;
            } else { //Izquierda
                dirIdle = -1; //izq 
                dirAtaque = 2;
            }
        } else {// Estatico
            anim.SetBool ("corriendo", false);
            if (dirIdle == 1){ // der
                AnimacionesPlayer(0);
            } else if (dirIdle == -1) {
                AnimacionesPlayer (-10);
            }
        }

	    anim.SetBool("saltando", false);
	    ActualizaCapa();
    }
    
	public void TomarDaño(int Daño){

		if (vida > 0) {
			if (MovimientoPlayer.dirIdle == 1){
				GetComponent<Animator>().SetTrigger("Daño");
				activaCapa("Daño");

			} else if (MovimientoPlayer.dirIdle == -1){
				GetComponent<Animator>().SetTrigger("dañoIzq");
				activaCapa("Daño");
			}
		}
		ActualizaCapa();
	}

    private void ActualizaCapa(){
        if (CCC.atacando || CAD.disparando) { //Si esta atacando cuerpo a cuerpo o a distancia  
	        activaCapa("Atacar");
            
        
        
        } else if (estaSaltando) { //Saltar 
	        activaCapa("Saltar");
	        
        }else if (SaltoIzq){
        	activaCapa("SaltoIzq");

	        
        } else if (Daño) { // Recibe daño
             activaCapa("Daño");

        }  else if ( movX != 0){ //Caminar 
            activaCapa("Caminar");
        }
        
    }

    private void activaCapa(string nombre){
        for ( int i = 0; i < anim.layerCount; i++){
            anim.SetLayerWeight(i,0);
        }
	    anim.SetLayerWeight(anim.GetLayerIndex(nombre) , 1); 

    }



    void AnimacionesPlayer(float n){
        anim.SetFloat("movX", n);
    }
}
