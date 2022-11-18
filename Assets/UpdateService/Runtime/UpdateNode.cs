using System;
using System.Collections.Generic;

namespace LeeFramework.Update
{
    public class UpdateNode
    {
        public UpdateNode next;
        public List<Action> cbs = new List<Action>();
        public int order = -1;

        public void Reset()
        {
            next = null;
            cbs.Clear();
            order = -1;
        }
    }

    public class UpdateTask
    {
        public UpdateNode node;
        public Action cb;
    }
}