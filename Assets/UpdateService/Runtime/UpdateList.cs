using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeeFramework.Update
{
    public class UpdateList
    {
        private UpdateNode _Head;
        private Dictionary<int, UpdateNode> _AllNode = new Dictionary<int, UpdateNode>();
        private UpdateNodePool _Pool = new UpdateNodePool();

        public void AddNode(Action cb, int order)
        {
            if (_AllNode.ContainsKey(order))
            {
                _AllNode[order].cbs.Add(cb);
                return;
            }
            
            UpdateNode node = _Pool.Spawn();
            node.order = order;
            node.cbs.Add(cb);

            _AllNode.Add(order, node);

            if (_Head == null)
            {
                _Head = node;
                return;
            }

            if (_Head.order < order)
            {
                if (_Head.next != null)
                {
                    CheckInsert(_Head, _Head.next, node, order);
                }
                else
                {
                    _Head.next = node;
                }
            }
            else
            {
                UpdateNode tmp = _Head;
                _Head = node;
                _Head.next = tmp;
            }
        }

        public void Remove(Action cb, int order)
        {
            if (_AllNode.ContainsKey(order))
            {
                _AllNode[order].cbs.Remove(cb);

                if (_Head.order == order)
                {
                    if (_Head.cbs.Count <= 0)
                    {
                        RemoveHead(_Head);
                    }
                }
                else
                {
                    RemoveNode(_Head, order);
                }
            }
            else
            {
                Debug.LogErrorFormat("不存在order为：{0}的节点", order);
            }
        }

        public void Clear()
        {
            _Head = null;

            foreach (UpdateNode item in _AllNode.Values)
            {
                if (item != null)
                {
                    _Pool.Recycle(item);
                }
            }
            _AllNode.Clear();
        }

        public void OnUpdate()
        {
            if (_Head != null)
            {
                CmdNode(_Head);
            }
        }

        private void CheckInsert(UpdateNode pre, UpdateNode next, UpdateNode node, int order)
        {
            if (next.order > order)
            {
                pre.next = node;
                node.next = next;
                return;
            }
            if (next.next != null)
            {
                CheckInsert(next, next.next, node, order);
            }
            else
            {
                next.next = node;
            }
        }

        private void CmdNode(UpdateNode node)
        {
            if (node.cbs != null && node.cbs.Count > 0)
            {
                foreach (Action item in node.cbs)
                {
                    item?.Invoke();
                }
            }
            if (node.next != null)
            {
                CmdNode(node.next);
            }
        }

        private void RemoveNode(UpdateNode head, int order)
        {
            if (head.next.order == order)
            {
                //移除节点
                if (head.next.cbs.Count <= 0)
                {
                    RemoveNext(head);
                }
            }
            else
            {
                RemoveNode(head.next, order);
            }
        }

        private void RemoveHead(UpdateNode head)
        {
            _AllNode.Remove(head.order);
            UpdateNode tmp = _Head;
            _Pool.Recycle(_Head);
            _Head = tmp.next;
        }

        private void RemoveNext(UpdateNode head)
        {
            UpdateNode tmp = head.next.next;
            _AllNode.Remove(head.next.order);
            _Pool.Recycle(head.next);
            head.next = tmp;
        }

    }
}