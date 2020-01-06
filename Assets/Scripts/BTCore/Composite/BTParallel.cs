
namespace BT
{

    public class BTParallel : BTComposite
    {
        public enum ParallelOpt
        {
            And = 1,    
            Or = 2,
        }

        private ParallelOpt _opt;

        public BTParallel(ParallelOpt opt)
        {
            _opt = opt;
        }

        public override BTResult Tick()
        {
            if (_children == null || _children.Count <= 0)
            {
                return BTResult.Success;
            }

            if (_opt == ParallelOpt.And)
            {
                int successResultCount = 0;

                foreach (BTNode child in _children)
                {
                    if (child.Tick() == BTResult.Success)
                    {
                        successResultCount++;
                    }
                }
                if (successResultCount == _children.Count)
                {
                    return BTResult.Success;
                }
            }
            else
            {
                foreach (BTNode child in _children)
                {
                    if (child.Tick() == BTResult.Success)
                    {
                        return BTResult.Success;
                    }
                }
            }

            return BTResult.Running;
        }

    }

}