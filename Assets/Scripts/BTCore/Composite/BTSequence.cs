
namespace BT
{

    public class BTSequence : BTComposite
    {
    
        private int _activeChildIndex = -1;


        public override BTResult Tick()
        {
            if (_children == null || _children.Count <= 0)
            {
                return BTResult.Success;
            }

            if (_activeChildIndex == -1)
            {
                _activeChildIndex = 0;
            }

            for (; _activeChildIndex < _children.Count; _activeChildIndex++)
            {
                BTNode activeChild = _children[_activeChildIndex];

                switch (activeChild.Tick())
                {
                    case BTResult.Running:
                        return BTResult.Running;

                    case BTResult.Success:
                        activeChild.Clear();
                        continue;

                    case BTResult.Failed:
                        activeChild.Clear();
                        _activeChildIndex = -1;
                        return BTResult.Failed;
                }
            }

            _activeChildIndex = -1;
            return BTResult.Success;
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