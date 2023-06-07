using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by H. Lloyd 
/// For project: AT03 - Indie Game
/// References:
///  - In class material
/// </summary> 

public class ExitDoor : MonoBehaviour
{
    public Notes noteScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(noteScript.endGame)
        {

        }
    }
}
