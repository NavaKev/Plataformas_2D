using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Presiona : MonoBehaviour {
	
	public void InicioAmain(){
		SceneManager.LoadScene("Main");
		
	}
	
	public void NavegacionInicio(){
		SceneManager.LoadScene("Inicio");
	}
	
	
	/*void Update(){
    	

		if (Input.GetKey(KeyCode.Space)){
			SceneManager.LoadScene("Main");
			
		}
		
		if (Input.GetKey(KeyCode.I)){
			SceneManager.LoadScene("Inicio");
		
	    
	}
	}*/
   
}