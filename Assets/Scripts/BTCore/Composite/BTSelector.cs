
namespace BT
{

    public class BTSelector : BTComposite
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
                        if (_runningChildIndex != i && _runningChildIndex != -1)
                        {
                            _children[_runningChildIndex].Clear();
                        }
                        child.Clear();
                        _runningChildIndex = -1;
                        return BTResult.Success;

                    case BTResult.Failed:
                        child.Clear();
                        continue;
                }
            }

            _runningChildIndex = -1;
            return BTResult.Failed;
        }

        public override void Clear()
        {
            base.Clear();
            _runningChildIndex = -1;
        }
    }
}