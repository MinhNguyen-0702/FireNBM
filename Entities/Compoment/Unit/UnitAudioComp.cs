using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    public enum TypeAudioUnit { None, Attack, Move, Worker, Death }

    [Serializable]
    public class AudioData 
    {
        public TypeAudioUnit Type;
        public AudioClip AudioClipUnit;
    }

    [RequireComponent(typeof(AudioSource))]
    public class UnitAudioComp : MonoBehaviour
    {
        // [SerializeField] private int m_maxVisibilityDistance = 50;
        [SerializeField] private List<AudioData> m_listDataAudio;
        
        private bool m_isActive;
        private AudioSource m_audio;
        private TypeAudioUnit m_currTypeAudio;
        private Dictionary<TypeAudioUnit, AudioClip> m_mapDataAudio;

        // ----------------------------------------------------------------------
        // API UNITY
        // ---------
        /////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            m_isActive = true;
            m_currTypeAudio = TypeAudioUnit.None;

            m_audio = GetComponent<AudioSource>();
            // m_audio.spatialBlend = 1f;

            SetDataAudio();
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        public bool FunIsActive() => m_isActive;
        public void FunSetActive(bool active) => m_isActive = active;

        public bool FunPlayAudio(TypeAudioUnit audioUnit)
        {
            if (m_isActive == false || m_mapDataAudio.ContainsKey(audioUnit) == false)
                return false;
            
            // Cập nhật lại âm thanh nếu có sự thay đổi.
            if (m_currTypeAudio != audioUnit)
            {
                m_currTypeAudio = audioUnit;
                m_audio.clip = m_mapDataAudio[m_currTypeAudio];
                m_audio.loop = true;
            }
            
            // TODO:  Tính toán khoảng cách âm thanh.

            m_audio.Play();
            return true;
        }

        public void FunStopAudio()
        {
            if (m_isActive == false)
                return;

            m_audio.Stop();
        }  

        public void FunPauseAudio() => m_audio.Pause();
        public void FunUnPause()    => m_audio.UnPause();

        

        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////

        private void SetDataAudio()
        {
            m_mapDataAudio = new Dictionary<TypeAudioUnit, AudioClip>();
            foreach (var audioData in m_listDataAudio)
            {
                if (m_mapDataAudio.ContainsKey(audioData.Type) == false)
                    m_mapDataAudio.Add(audioData.Type, audioData.AudioClipUnit);
            }
        }
    }
}