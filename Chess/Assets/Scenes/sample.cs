using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sample : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
	{
       GameObject gameobject=GameObject.Find("Program");
	   gameobject.GetComponent<ChessPieces>().pawnselect(0,1);
	   gameobject.GetComponent<ChessPieces>().red();
	   
    }
}
