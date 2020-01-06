using UnityEngine;
using System.Collections;
using BT;

public class FindEscapeAction : FindDestinationAction
{
    private float _safeDistance;

    public FindEscapeAction(string targetName, string destinationDataName, float safeDistance)
        : base(targetName, destinationDataName)
    {
        _safeDistance = safeDistance;
    }

    protected override BTResult Execute()
    {
        if (!CheckTarget())
        {
            return BTResult.Success;
        }

        Vector3 offset = GetToTargetOffset();

        if (offset.sqrMagnitude <= _safeDistance * _safeDistance)
        {
            Vector3 direction = -offset.normalized;
            Vector3 destination = _safeDistance * direction;
            storyBoard.SetData<Vector3>(_destinationDataName, destination);
            return BTResult.Running;
        }

        return BTResult.Success;
    }
}
