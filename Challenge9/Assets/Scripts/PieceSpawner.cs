using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PieceType type;
    public Piece currentPiece;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {

        int amtObj = 0;
        if(type == PieceType.jump)
        {
            amtObj = LevelManager.instance.jumps.Count;
        }else
        if (type == PieceType.longblock)
        {
            amtObj = LevelManager.instance.longblocks.Count;
        }
        else
        if (type == PieceType.ramp)
        {
            amtObj = LevelManager.instance.ramps.Count;
        }
        else
        if (type == PieceType.slide)
        {
            amtObj = LevelManager.instance.slides.Count;
        }

        currentPiece = LevelManager.instance.getPiece(type, Random.Range(0,amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void DeSpawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}
