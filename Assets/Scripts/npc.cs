using UnityEngine;
using System.Collections;

public class npc : MonoBehaviour {

    //Declaring variables
    bool text;
    void OnTriggerEnter(Collider col)
    {
        //Telling game to actiavte boolean
        if (col.gameObject.tag == "Player")
        {
            text = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //Telling game to actiavte boolean
        if (col.gameObject.tag == "Player")
        {
            text = false;
        }
    }

    void OnGUI()
    {
        //If the boolean is active, display the text
        if (text == true)
        {
            GUI.skin.box.wordWrap = true;
            print("sadsadsadadsads");
            GUI.Box(new Rect(250,100, 200, 100), "im toad the toad give me your coins!");
            
        }
    }

}

