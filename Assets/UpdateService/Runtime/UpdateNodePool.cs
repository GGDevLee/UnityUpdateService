using System.Collections.Generic;


namespace LeeFramework.Update
{
    public class UpdateNodePool
    {
        private Stack<UpdateNode> _Pool = new Stack<UpdateNode>();

        public UpdateNode Spawn()
        {
            if (_Pool.Count > 0)
            {
                return _Pool.Pop();
            }

            return new UpdateNode();
        }

        public void Recycle(UpdateNode node)
        {
            if (node != null)
            {
                node.Reset();
                _Pool.Push(node);
            }
        }

    } 
}
