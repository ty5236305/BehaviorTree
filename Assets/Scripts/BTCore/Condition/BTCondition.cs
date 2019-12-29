
namespace BT
{

    public class BTCondition : BTNode
    {

        sealed public override BTResult Tick()
        {
            if (Check())
            {
                return BTResult.Success;
            }

            return BTResult.Failed;
        }

        public virtual bool Check()
        {
            return false;
        }

    }
}

