using UnityEngine;
using System.Collections;
using BT;

public class CheckInSightCondition : BTCondition
{
    private float _sightLength;
    private string _targetName;

    private Transform _trans;


    public CheckInSightCondition(float sightLength, string targetName)
    {
        _sightLength = sightLength;
        _targetName = targetName;
    }

    public override void Initialize(BTStoryBoard storyBoard)
    {
        base.Initialize(storyBoard);

        _trans = storyBoard.transform;
    }

    public override bool Check()
    {
        GameObject target = GameObject.Find(_targetName) as GameObject;
        if (target == null) return false;

        Vector3 offset = target.transform.position - _trans.position;
        return offset.sqrMagnitude <= _sightLength * _sightLength;
    }
}
