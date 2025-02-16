using System;
using System.Collections.Generic;
using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    public class MessageUpdateFormCurrState : IMessage
    {
        public bool IsFormCurrent;
        public Enum TypeActionUnit;
        public HashSet<GameObject> SelectedUnits;

        public MessageUpdateFormCurrState(bool isFormCurrent, Enum typeActionUnit, HashSet<GameObject> selectedUnits)
        {
            IsFormCurrent = isFormCurrent;
            TypeActionUnit = typeActionUnit;
            SelectedUnits = selectedUnits;
        }
    }
}