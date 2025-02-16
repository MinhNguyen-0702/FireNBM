// ---------------------------------------------------------------------------------------
//  Outline.cs
//  QuickOutline
//
//  Created by Chris Nolet on 3/30/18.
//  Copyright © 2018 Chris Nolet. All rights reserved.
// 
//  => Custom :)) 
//
// ---------------------------------------------------------------------------------------


using System.Linq;
using UnityEngine;

namespace FireNBM
{
    [DisallowMultipleComponent]
    public class OutlineComp : MonoBehaviour 
    {
        private Color m_currentColor;
        private Color m_outlineColorSelecter = Color.white;
        private Color m_outlineColorHighlight = Color.black;
        private float m_outlineWidth = 5f;

        private Renderer[] m_renderers;
        private Material m_outlineMaskMaterial;
        private Material m_outlineFillMaterial;


        private void Awake() 
        {
            // Cache renderers
            m_renderers = GetComponentsInChildren<Renderer>();

            // Instantiate outline materials
            m_outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
            m_outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

            m_outlineMaskMaterial.name = "OutlineMask (Instance)";
            m_outlineFillMaterial.name = "OutlineFill (Instance)";
        }

        private void OnEnable() 
        {
            foreach (var renderer in m_renderers) 
            {
                var materials = renderer.sharedMaterials.ToList();
                materials.Add(m_outlineMaskMaterial);
                materials.Add(m_outlineFillMaterial);
                renderer.materials = materials.ToArray();
            }
        }

        private void OnDisable() 
        {
            foreach (var renderer in m_renderers) 
            {
                var materials = renderer.sharedMaterials.ToList();
                materials.Remove(m_outlineMaskMaterial);
                materials.Remove(m_outlineFillMaterial);
                renderer.materials = materials.ToArray();
            }
        }

        private void OnDestroy() 
        {
            // Destroy material instances
            Destroy(m_outlineMaskMaterial);
            Destroy(m_outlineFillMaterial);
        }

        public void FunSetDataOutline(Color selecter, Color highlight, float width)
        {
            m_outlineWidth = width;
            m_outlineColorSelecter = selecter;
            m_outlineColorHighlight = highlight;
        }


        public void FunSetActive(bool active)
        {
            if (active == true)
                SetWidthOutline(m_outlineWidth);
            else    
                SetWidthOutline(0f);
        }

        public void FunSetSelecter()
        {
            SetColorOutline(m_outlineColorSelecter);
        }

        public void FunSetHighlight()
        {
            SetColorOutline(m_outlineColorHighlight);
        }

        

        private void UpdateMaterialProperties() 
        {
            // Apply properties according to mode
            m_outlineFillMaterial.SetColor("_OutlineColor", m_currentColor);

            m_outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
            m_outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
            m_outlineFillMaterial.SetFloat("_OutlineWidth", m_outlineWidth);
        }

        private void SetWidthOutline(float width)
        {
            m_outlineWidth = width;
            UpdateMaterialProperties();
        }

        private void SetColorOutline(Color color)
        {
            m_currentColor = color;
            UpdateMaterialProperties();
        }
    }
}