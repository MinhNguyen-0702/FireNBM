using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Chịu trách nhiệm tạo bất kỳ đối tượng nào trong game rts.
    /// </summary>
    [AddComponentMenu("FireNBM/System/Factory System")]
    public class FactorySystem : Singleton<FactorySystem>
    {
        public RaceFactorySystem RaceFactory { get; set; } 
        public static FactorySystem Instance { get => InstanceSingletonInScene; }


        // ----------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // ///////////////////////////////////////////////////////////////////////////////////

        protected override void Awake()
        { 
            base.Awake();
            RaceFactory = new RaceFactorySystem();
        }

        // --------------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        public bool FunInitializeDataRace(List<RaceDataSO> datas)
        {
            return RaceFactory.FunInitializeData(datas);
        }
    }
}