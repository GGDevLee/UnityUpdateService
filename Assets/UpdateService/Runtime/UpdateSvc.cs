using System;
using UnityEngine;


namespace LeeFramework.Update
{
    public class UpdateSvc : MonoBehaviour
    {
        #region 单例
        private static GameObject _GameObject;
        public static GameObject go => _GameObject;

        private static UpdateSvc _Instance;
        public static UpdateSvc instance
        {
            get
            {
                if (_Instance == null)
                {
                    _GameObject = new GameObject("LeeFramework.UpdateSvc");
                    _GameObject.hideFlags = HideFlags.HideAndDontSave;
                    DontDestroyOnLoad(_GameObject);
                    _Instance = _GameObject.AddComponent<UpdateSvc>();
                }
                return _Instance;
            }
        } 
        #endregion

        private UpdateList _UpdateList = new UpdateList();
        private UpdateList _FixUpdateList = new UpdateList();
        private UpdateList _LateUpdateList = new UpdateList();

        public void Register(UpdateType type, Action cb, int order)
        {
            switch (type)
            {
                case UpdateType.Update:
                    _UpdateList.AddNode(cb, order);
                    break;
                case UpdateType.LateUpdate:
                    _LateUpdateList.AddNode(cb, order);
                    break;
                case UpdateType.FixUpdate:
                    _FixUpdateList.AddNode(cb, order);
                    break;
            }
        }

        public void Unregister(UpdateType type, Action cb, int order)
        {
            switch (type)
            {
                case UpdateType.Update:
                    _UpdateList.Remove(cb, order);
                    break;
                case UpdateType.LateUpdate:
                    _LateUpdateList.Remove(cb, order);
                    break;
                case UpdateType.FixUpdate:
                    _FixUpdateList.Remove(cb, order);
                    break;
            }
        }

        public void Clear(UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Update:
                    _UpdateList.Clear();
                    break;
                case UpdateType.LateUpdate:
                    _LateUpdateList.Clear();
                    break;
                case UpdateType.FixUpdate:
                    _FixUpdateList.Clear();
                    break;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            _UpdateList.OnUpdate();
        }

        private void LateUpdate()
        {
            _LateUpdateList.OnUpdate();
        }

        private void FixedUpdate()
        {
            _FixUpdateList.OnUpdate();
        }


    }
}
