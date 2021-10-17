using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    private int selectedCharacter = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in characters)
        {
            go.SetActive(false);
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void ChangeCharacter(int index)
    {
        characters[selectedCharacter].SetActive(false);
        characters[index].SetActive(true);
        selectedCharacter = index;
    }

}
