using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D rb;
    public Transform jugador;
    private bool mirandoderecha = true, atacando = false;

    [Header("Vida")]
    public int vida_INI;
    private int vida;
    //public GameObject barraVida;

    [Header("Ataque")]
    public Transform controladorAtaque;
    public float radioAtaque;
    public int dañoAtaque;
    public float tiempoEntreAtaques, tiempoEntreGiros;
    public float tiempoSigAtaque, tiempoSigGiro;
    public static float distanciaEnemigoJugador; 
    [SerializeField] private float dAtaque, dSeguimiento, velSeguimiento;
    
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tiempoSigGiro = tiempoEntreGiros; // Empieza a girar para ver donde esta el personaje
        vida = vida_INI;
        
    }

   
    void Update(){
        if (VidasPlayer.vida > 0 ) {
            //Frecuencia con la que busca al player
            if (tiempoSigGiro > 0 ){
                tiempoSigGiro -= Time.deltaTime;
            } else {
                MirarJugador();
                tiempoSigGiro = tiempoEntreGiros;
            }

            if (tiempoSigAtaque < 0.05f && tiempoEntreAtaques > 0 ){
                atacando = false;
            }
        
            if (tiempoSigAtaque > 0 ) {
                tiempoSigAtaque -= Time.deltaTime;
                    
            }
            distanciaEnemigoJugador = Vector2.Distance(transform.position, jugador.position);
            
            //Determina que accion realiza en contra del jugador
            DeterminaAccion(distanciaEnemigoJugador);
        } 
    }

   private void DeterminaAccion(float d) {
        ResetAnimsEnemigo();
        if (d <= dAtaque && tiempoSigAtaque <= 0){ // Enemigo Ataca
            atacando = true;
            anim.SetBool("atacando",true);
        } else if (( d >dAtaque) && ( d <= dSeguimiento)){ //Seguir al Player
            anim.SetBool("caminando", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(jugador.position.x, transform.position.y), velSeguimiento / 1000 );

        }
    }

   private void ResetAnimsEnemigo(){
        anim.SetBool("atacando", false);
        anim.SetBool("caminando", false);

    }

    public void TomarDaño(int daño){
        vida -= daño;
        //DibujaBarra(vida);
        if (vida <= 0) {
	        anim.SetTrigger("Muerte");
	        anim.SetBool("Muerte", true);
            StartCoroutine(EliminaEnemigo());
        }
    }

    IEnumerator EliminaEnemigo(){
	    yield return new WaitForSeconds(1.2f); // Anim de 1.2 seg antes de que lo destruyamos 
	    anim.SetBool("Muerte", true);
        Muerte();
    }

	private void Muerte(){
        Destroy(this.gameObject);
    }

    public void MirarJugador(){
        if ((jugador.position.x > transform.position.x && !mirandoderecha) || (jugador.position.x < transform.position.x && mirandoderecha)){
            mirandoderecha = !mirandoderecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0 );
        }
    }

    private void AtaqueEnemigo(){
        Collider2D [] objs = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque); //verifica que golpeó
        foreach (Collider2D colisionador in objs ){
            if (colisionador.CompareTag("Player")){
                colisionador.transform.GetComponent<VidasPlayer>().TomarDaño(dañoAtaque);
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
        Gizmos.color = new Color(0.75f, 0, 0 );
        Gizmos.DrawWireSphere(this.transform.position, dAtaque);
        Gizmos.color = new Color(0.5f, 0, 0);
        Gizmos.DrawWireSphere(this.transform.position, dSeguimiento);
    }

    private void DibujaBarra(float n){
       // barraVida.transform.localScale = new Vector2(0.73f * n / vida_INI, barraVida.transform.localScale.y);
    }
}
