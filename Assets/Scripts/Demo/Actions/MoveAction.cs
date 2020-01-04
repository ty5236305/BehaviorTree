using UnityEngine;
using System.Collections;
using BT;

public class MoveAction : BTAction
{

    private string _destinationDataName;

    private Vector3 _destination;
    private float _tolerance = 0.00001f;
    private float _speed;

    private Transform _trans;


    public MoveAction(string destinationDataName, float speed)
    {
        _destinationDataName = destinationDataName;
        _speed = speed;
    }

    public override void Initialize(BTStoryBoard storyBoard)
    {
        base.Initialize(storyBoard);

        _trans = storyBoard.transform;
    }

    protected override BTResult Execute()
    {
        UpdateDestination();
        UpdateFaceDirection();

        if (CheckArrived())
        {
            return BTResult.Success;
        }
        MoveToDestination();
        return BTResult.Running;
    }

    private void UpdateDestination()
    {
        _destination = storyBoard.GetData<Vector3>(_destinationDataName);
    }

    private void UpdateFaceDirection()
    {
        Vector3 offset = _destination - _trans.position;
        if (offset.x >= 0)
        {
            _trans.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            _trans.localEulerAngles = Vector3.zero;
        }
    }

    private bool CheckArrived()
    {
        Vector3 offset = _destination - _trans.position;

        return offset.sqrMagnitude < _tolerance * _tolerance;
    }

    private void MoveToDestination()
    {
        Vector3 direction = (_destination - _trans.position).normalized;
        _trans.position += direction * _speed;
    }
}
