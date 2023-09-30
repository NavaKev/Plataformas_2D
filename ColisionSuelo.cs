using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionSuelo : MonoBehaviour {

     public static bool estaEnSuelo = true;

     private void OnTriggerEnter2D(Collider2D collision){

         if (collision.gameObject.tag == "suelo"){
            
             estaEnSuelo = true;
         }
     }
}
