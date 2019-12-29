using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BT;


public abstract class BTTree : MonoBehaviour
{
    protected BTNode _root;

    [HideInInspector]
    public BTStoryBoard storyBoard;

    [HideInInspector]
    public bool isRunning = true;

    void Start()
    {
        Initialize();

        if(_root != null)
        {
            _root.Initialize(storyBoard);
        }
    }

    void Update()
    {
        if (!isRunning) return;

        if (_root != null)
        {
            _root.Tick();
        }
    }

    void OnDestroy()
    {
        if (_root != null)
        {
            _root.Clear();
        }
    }

    protected virtual void Initialize()
    {
        storyBoard = GetComponent<BTStoryBoard>();
        if (storyBoard == null)
        {
            storyBoard = gameObject.AddComponent<BTStoryBoard>();
        }
    }

}
