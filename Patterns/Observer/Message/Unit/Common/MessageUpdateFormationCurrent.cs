using UnityEngine;
using FireNBM.Pattern;
using System.Collections.Generic;

namespace FireNBM
{
    public class MessageUpdateFormCurrMoveTarget : IMessage
    {
        public Vector3 PosTarget;
        public HashSet<GameObject> SelectedUnits;

        public MessageUpdateFormCurrMoveTarget(Vector3 posTarget, HashSet<GameObject> selectedUnits)
        {
            PosTarget = posTarget;
            SelectedUnits = selectedUnits;
        }
    }
}