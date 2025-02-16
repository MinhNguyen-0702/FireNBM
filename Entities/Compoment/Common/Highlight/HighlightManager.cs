using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    public class HighlightManager
    {
        private Dictionary<GameObject, ObjectTypeBaseHighlightComp> m_mapHighlight;

        public HighlightManager()
        {
            m_mapHighlight = new Dictionary<GameObject, ObjectTypeBaseHighlightComp>();
        }

        public void FunSetHighlight(GameObject obj)
        {
            TryToAddObject(obj);
            m_mapHighlight[obj].FunHighlightColor();
        }

        public void FunSetSelector(GameObject obj)
        { 
            TryToAddObject(obj);
            m_mapHighlight[obj].FunSelectedColor();
        }

        public void FunSetCheck(GameObject obj)
        { 
            TryToAddObject(obj);
            m_mapHighlight[obj].FunCheckColor();
        }

        public void FunSetDisable(GameObject obj)
        {
            TryToAddObject(obj);
            m_mapHighlight[obj].FunDisableSelectedState();
        }



        private void TryToAddObject(GameObject obj)
        {
            if (m_mapHighlight.ContainsKey(obj) == false)
            {
                var highlightComp = obj.GetComponent<ObjectTypeBaseHighlightComp>();
                if (highlightComp != null)
                    m_mapHighlight.Add(obj, highlightComp);
            }
        }
    }
}