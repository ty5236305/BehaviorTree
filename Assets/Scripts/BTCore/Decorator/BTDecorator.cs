
namespace BT
{

    public class BTDecorator : BTNode
    {

        protected BTNode child;


        public BTDecorator(BTNode node)
        {
            child = node;
        }

        public override void Initialize(BTStoryBoard storyBoard)
        {
            base.Initialize(storyBoard);
            child.Initialize(storyBoard);
        }

        public override void Clear()
        {
            base.Clear();
            child.Clear();
        }

    }

}