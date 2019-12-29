
using System.Collections.Generic;

namespace BT
{

    public enum BTResult
    {
        Success,
        Running,
        Failed,
    }

    public class BTNode
    {

        protected BTStoryBoard storyBoard;


        public virtual void Initialize(BTStoryBoard storyBoard)
        {
            this.storyBoard = storyBoard;
        }

        public virtual BTResult Tick() 
        {
            return BTResult.Success;
        }

        public virtual void Clear() { }

    }

}