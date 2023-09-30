using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidasPlayer : MonoBehaviour
{
    public static int vida;
	public Image[] imgVidas;
    private bool haMuerto;
    public GameObject gameOver;
    public AudioSource audio;
    public AudioClip muerte;

    void Start() {

        audio = GetComponent<AudioSource>();

        haMuerto = false;
        vida = 5;
        for (int i = 0; i < vida; i++){
            imgVidas[i].enabled = true;
        }

        gameOver.SetActive(false);
        
    }


    void Update() {

    }

	public void TomarDaño(int Daño){

        if (vida > 0) {
            if (MovimientoPlayer.dirIdle == 1){
	            GetComponent<Animator>().SetTrigger("Daño");
	            GetComponent<Animator>().SetBool("Daño", true);

            } else if (MovimientoPlayer.dirIdle == -1){
	            GetComponent<Animator>().SetTrigger("dañoIzq");
	            GetComponent<Animator>().SetBool("Daño", true);
            }
            
	        vida -= Daño;
            DibujaVidas(vida);

        } 
        if (vida <= 0 && !haMuerto){
            haMuerto = true;
            GetComponent<Animator>().SetBool("muerte",true);
            StartCoroutine(EjecutaMuerte());
        }
        
    }

    public void DibujaVidas(int n){
        for (int i = 0; i < 5; i++){ //Ocultas todas al inicio
            imgVidas[i].enabled = false;
        }
        
        for(int i = 0; i < n ; i++){
            imgVidas[i].enabled = true;
        }
    }

    IEnumerator EjecutaMuerte(){
        yield return new WaitForSeconds(2.1f);
        audio.clip = muerte;
        audio.Play();
        // Cuando oculta al player, muestra Game Over
        gameOver.SetActive(true);
        StartCoroutine(RegresaMenu()); //Regereso automatico al menu despues de morir
    }

    IEnumerator RegresaMenu() {
        yield return new WaitForSeconds(2.7f);
	    SceneManager.LoadScene("Creditos");
        //Destroy(gameObject)
    }
}
