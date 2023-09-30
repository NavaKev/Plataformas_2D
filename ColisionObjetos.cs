using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColisionObjetos : MonoBehaviour
{
    public static int puntos;
    public Text txtPuntos;
	public static int nivel;
	public GameObject Fin1, Fin2, Fin3, Fin4, Felicidades, Creditos;
	public AudioSource audio;
	public AudioClip Diamantes, vida, niveles, Win;
	

    private void Start(){
	    puntos = 0;
	    nivel = 1; 
	    OcultaPantalllas();
	    audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D obj){
	    if (obj.tag == "puntos") {
		    audio.clip = Diamantes;
		    audio.Play();
            //Debug.Log("Punto");
            puntos++;
            EscibrePuntos(puntos);
            Destroy(obj.gameObject);

	    }

        if (obj.tag == "vidas"){
	        Debug.Log("Vidas");
	        audio.clip = vida;
	        audio.Play();
            if (VidasPlayer.vida < 5){
                VidasPlayer.vida++;
                GetComponent<VidasPlayer>().DibujaVidas(VidasPlayer.vida);
                Destroy(obj.gameObject);
            }
        }
        
	    if(obj.tag == "Casa"){
	    	audio.clip = niveles;
	    	audio.Play();
	    	Destroy(obj.gameObject);
	    	Debug.Log("Casa");
		    StartCoroutine(PantallaFinNivel());
	    }
    }
    
	IEnumerator PantallaFinNivel(){
		yield return new WaitForSeconds(2.0f);
		switch (nivel){
		case 1: //Pasa al 2 
			OcultaPantalllas();
			Fin1.SetActive(true);
			break;
		case 2: //Pasa al 3
			OcultaPantalllas();
			Fin2.SetActive(true);
			break;
		case 3: // Pasa al 4
			OcultaPantalllas();
			Fin3.SetActive(true);
			break;
		case 4: // Pasa al 5
			OcultaPantalllas();
			Fin4.SetActive(true);
			break;
		case 5: //Pantalla Feliciades
			OcultaPantalllas();
			Felicidades.SetActive(true);
			audio.clip = Win;
			audio.Play();
			yield return new WaitForSeconds (2.0f);
			SceneManager.LoadScene("Creditos");
			
			break;	
		}
		
		StartCoroutine(SiguienteNivel());
	}
	
	private void OcultaPantalllas(){
		Fin1.SetActive(false);
		Fin2.SetActive(false);
		Fin3.SetActive(false);
		Fin4.SetActive(false);
		Felicidades.SetActive(false);
		Creditos.SetActive(false);
		
	}
	
	public void EscibrePuntos(int n){
        txtPuntos.text = n.ToString();
    }
    
	
	IEnumerator SiguienteNivel(){
		switch (nivel){
		case 1: 
			transform.position = new Vector2(-22f,-17); //Posicion nivel 2
			nivel = 1;
			break;
		case 2 : // Pasa al nivel 2 
			transform.position = new Vector2(-22f,-29); // Posicion nivel 3
			nivel = 2;
			break;
		case 3 : // Pasa al nivel 3
			transform.position = new Vector2(-20f,-55); // pos lvl 4
			nivel = 3;
			break;
		case 4 : // Pasa al nivel 4
			nivel = 4;
			transform.position = new Vector2(-17f,-88); // pos lvl 5
			break;
		}
		
		yield return new WaitForSeconds(5.0f);
		OcultaPantalllas();
		Debug.Log("Nivel:"+ nivel);
		nivel++;
	}
    
}
