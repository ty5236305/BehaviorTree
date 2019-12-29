
namespace BT
{

    public class BTSelector : BTComposite
    {
        private int _activeChildIndex = -1;

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
                        if (_activeChildIndex != i && _activeChildIndex != -1)
                        {
                            _children[_activeChildIndex].Clear();
                        }
                        _activeChildIndex = i;
                        return BTResult.Running;


                    case BTResult.Success:
                        if (_activeChildIndex != i && _activeChildIndex != -1)
                        {
                            _children[_activeChildIndex].Clear();
                        }
                        child.Clear();
                        _activeChildIndex = -1;
                        return BTResult.Success;

                    case BTResult.Failed:
                        child.Clear();
                        continue;
                }
            }

            _activeChildIndex = -1;
            return BTResult.Failed;
        }

        public override void Clear()
        {
            if (_activeChildIndex != -1)
            {
                _children[_activeChildIndex].Clear();
            }
        }
    }
}