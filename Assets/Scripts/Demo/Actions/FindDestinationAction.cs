using UnityEngine;
using System.Collections;
using BT;



public abstract class FindDestinationAction : BTAction
{
    protected string _targetName;
    protected string _destinationDataName;

    protected Transform _trans;

    protected FindDestinationAction(string targetName, string destinationDataName)
    {
        _targetName = targetName;
        _destinationDataName = destinationDataName;
    }

    public override void Initialize(BTStoryBoard storyBoard)
    {
        base.Initialize(storyBoard);

        _trans = storyBoard.transform;
    }

    protected Vector3 GetToTargetOffset()
    {
        GameObject targetGo = GameObject.Find(_targetName) as GameObject;
        if (targetGo == null)
        {
            return Vector3.zero;
        }

        return targetGo.transform.position - _trans.position;
    }

    protected bool CheckTarget()
    {
        return GameObject.Find(_targetName) as GameObject != null;
    }
}



