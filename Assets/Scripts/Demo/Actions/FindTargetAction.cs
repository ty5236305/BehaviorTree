using UnityEngine;
using System.Collections;
using BT;

public class FindTargetAction : FindDestinationAction
{
    private float _minDistance;

    public FindTargetAction(string targetName, string destinationDataName, float minDistance, BTCondition precondition = null) : base(targetName, destinationDataName, precondition)
    {
        _minDistance = minDistance;
    }

    protected override BTResult Execute()
    {
        if (!CheckTarget())
        {
            return BTResult.Success;
        }

        Vector3 offset = GetToTargetOffset();

        if (offset.sqrMagnitude >= _minDistance * _minDistance)
        {
            Vector3 direction = offset.normalized;
            Vector3 destination = _trans.position + offset - _minDistance * direction;
            storyBoard.SetData<Vector3>(_destinationDataName, destination);
            return BTResult.Running;
        }
        return BTResult.Success;
    }
}
