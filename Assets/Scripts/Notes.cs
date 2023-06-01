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

    [SerializeField]TMP_Text showText;

    // Start is called before the first frame update
    void Start()
    {
        totalNotes = notes.Length;
        showText.text = notesCollected + " / " + totalNotes;
    }

    // Update is called once per frame
    void Update()
    {
        showText.text = notesCollected + " / " + totalNotes;
    }
}
