using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Created by H. Lloyd 
/// For project: AT03 - Indie Game
/// References:
///  - In class material
/// </summary> 

public class Notes : MonoBehaviour
{
    public GameObject[] notes;
    public int notesCollected = 0;
    private int totalNotes;

    public bool endGame;

    [SerializeField]TMP_Text notesText, endgameText;

    // Start is called before the first frame update
    void Start()
    {
        endGame = false;
        totalNotes = notes.Length;
        notesText.text = notesCollected + " / " + totalNotes;
        endgameText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        notesText.text = notesCollected + " / " + totalNotes;
        if(notesCollected == totalNotes)
        {
            Debug.Log("Endgame Entered");
            endGame = true;
            endgameText.enabled = true;
        }
    }
}
