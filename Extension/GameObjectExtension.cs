using Unity.VisualScripting;
using UnityEngine;

namespace FireNBM
{
    public static class GameObjectExtension
    {
        public static void SetLayerMaskAllChildren(this GameObject item, string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            item.layer = layer;

            foreach (Transform child in item.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = layer;
            }
        }
    }
}