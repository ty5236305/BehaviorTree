using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace BT
{
    public class BTComposite : BTNode
    {
        protected List<BTNode> _children;

        public override void Initialize(BTStoryBoard storyBoard)
        {
            base.Initialize(storyBoard);

            if (_children != null)
            {
                foreach (BTNode child in _children)
                {
                    child.Initialize(storyBoard);
                }
            }
        }

        public virtual void AddChild(BTNode node)
        {
            if (_children == null)
            {
                _children = new List<BTNode>();
            }
            if (node != null)
            {
                _children.Add(node);
            }
        }

        public virtual void RemoveChild(BTNode node)
        {
            if (_children != null && node != null)
            {
                _children.Remove(node);
            }
        }

        public override void Clear()
        {
            foreach (BTNode child in _children)
            {
                child.Clear();
            }
        }

    }

}
