using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance { set; get; }

    public bool SHOW_COLLIDER = true;

    //Level Spawning
    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 10;
    private const int INITIAL_TRANSITION_SEGMENTS = 2;
    private const int MAX_SEGMENTS_ON_SCREEN = 15;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1,y2,y3;


    //List of Pieces

    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();

    [HideInInspector]
    public List<Piece> pieces = new List<Piece>(); //All the pieces in the pool

    //List of Segements
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();

    [HideInInspector]
    public List<Segment> segments = new List<Segment>();


    private void Awake()
    {
        instance = this;
        cameraContainer = Camera.main.transform;
        currentLevel = currentSpawnZ = 0;
    }

    private void Start()
    {
        for (int i = 0; i < INITIAL_SEGMENTS; i++)
        {
            if(i < INITIAL_TRANSITION_SEGMENTS)
            {
                SpawnTransition();
            }
            else
            {
                GenerateSegment();
            }
            
        }
    }

    private void Update()
    {
        if(currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();
            if (amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
            {
                segments[amountOfActiveSegments - 1].DeSpawn();
                amountOfActiveSegments--;
            }
        }
    }

    private void GenerateSegment()
    {
        SpawnSegment();
        if(UnityEngine.Random.Range(0,1) < (continiousSegments * 1.25f))
        {
            //spawn transition segment
            continiousSegments = 0;
            SpawnTransition();

        }
        else
        {
            continiousSegments++;
        }
        
    }

    private void SpawnSegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = UnityEngine.Random.Range(0, possibleSeg.Count);
        Segment s = GetSegment(id, false);
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }

    private void SpawnTransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = UnityEngine.Random.Range(0, possibleTransition.Count);
        Segment s = GetSegment(id, true);
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }

    public Segment GetSegment(int id,bool transition)
    {
        Segment r = null;
        r = segments.Find(x => x.SegmentId == id && x.transition == transition && !x.gameObject.activeSelf);
        if(r == null)
        {
            GameObject go = Instantiate(transition ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            r = go.GetComponent<Segment>();
            r.SegmentId = id;
            r.transition = transition;
            segments.Insert(0, r)
;
        }
        else
        {
            segments.Remove(r);
            segments.Insert(0, r);
        }
        return r;
    }
    

    public Piece getPiece(PieceType type,int visualIndex)
    {
        Piece p = pieces.Find(x => x.type == type && x.visualIndex == visualIndex && ! x.gameObject.activeSelf);
        if(p == null)
        {
            GameObject go = null;
            if(type == PieceType.ramp)
            {go = ramps[visualIndex].gameObject;

            }else if (type == PieceType.longblock)
            {go = longblocks[visualIndex].gameObject;

            }else if (type == PieceType.jump)
            {go = jumps[visualIndex].gameObject;

            }else if (type == PieceType.slide)
            {go = slides[visualIndex].gameObject;

            }

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);

        }
        return p;
    }
}
