using UnityEngine;
using System.Collections;

public class npc : MonoBehaviour {

    //Declaring variables
    bool text;
    void OnColliderEnter(Collider col)
    {
        //Telling game to actiavte boolean
        if (col.gameObject.tag == "Player")
        {
            text = true;
        }
    }
    void OnGUI()
    {
        //If the boolean is active, display the text
        if (text == true)
        {
            GUI.Box(new Rect(10,10, 100, 20), "hello world");
        }
    }

}

