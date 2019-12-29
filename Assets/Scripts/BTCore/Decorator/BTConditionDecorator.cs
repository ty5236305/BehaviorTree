
namespace BT
{
    public class BTConditionDecorator : BTDecorator
    {
        private BTCondition condition;

        public BTConditionDecorator(BTNode node, BTCondition condition) : base(node) 
        {
            this.condition = condition;
        }

        public override void Initialize(BTStoryBoard storyBoard)
        {
            base.Initialize(storyBoard);
            condition.Initialize(storyBoard);
        }

        public override BTResult Tick()
        {
            if (condition != null && condition.Tick() == BTResult.Failed)
            {
                return BTResult.Failed;
            }
            return child.Tick();
        }
    }
}

