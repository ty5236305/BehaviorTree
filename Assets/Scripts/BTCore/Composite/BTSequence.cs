
namespace BT
{

    public class BTSequence : BTComposite
    {
    
        private int _runningChildIndex = -1;


        public override BTResult Tick()
        {
            if (_children == null || _children.Count <= 0)
            {
                return BTResult.Success;
            }

            for (int i = 0; i < _children.Count; i++)
            {
                BTNode child = _children[i];

                switch (child.Tick())
                {
                    case BTResult.Running:
                        if (_runningChildIndex != i && _runningChildIndex != -1)
                        {
                            _children[_runningChildIndex].Clear();
                        }
                        _runningChildIndex = i;
                        return BTResult.Running;

                    case BTResult.Success:
                        child.Clear();
                        continue;

                    case BTResult.Failed:
                        if (_runningChildIndex != i && _runningChildIndex != -1)
                        {
                            _children[_runningChildIndex].Clear();
                        }
                        child.Clear();
                        _runningChildIndex = -1;
                        return BTResult.Failed;
                }
            }

            _runningChildIndex = -1;
            return BTResult.Success;
        }

        public override void Clear()
        {
            if (_runningChildIndex != -1)
            {
                _children[_runningChildIndex].Clear();
            }
            _runningChildIndex = -1;
        }
    }

}