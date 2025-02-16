using UnityEngine;

namespace FireNBM
{
    public enum TypeAudioUnderConstruction { None, Place, Destroy }

    [RequireComponent(typeof(AudioSource))]
    public class UnderConstructionAudioComp : MonoBehaviour
    {
    }
}