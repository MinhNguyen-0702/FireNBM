// using System.Collections.Generic;
// using FireNBM.Pattern;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Lớp abstract chung chứa các dữ liệu dùng chung cho unit.
//     /// </summary>
//     public abstract class FlyweightDataUnit : FlyweightData
//     {
//         /// <summary> 
//         ///     Các kiểu Animation có thể sủ dụng cho unit.</summary>
//         public TypeUnitAnimState AnimStates;
//         /// <summary>
//         ///     Danh sách chứa các tên của anim được lấy từ người dùng.</summary>
//         public List<AnimStateNamePair> NameAnimsUnit = new List<AnimStateNamePair>();

//         /// <summary>
//         ///     Dùng để truy xuất string dựa trên enum của nó. </summary>
//         public Dictionary<TypeUnitAnimState, string> MapNameAnims;

        
//         // ---------------------------------------------------------------------------------
//         // FUNCTION PUBLIC 
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         public override void FunInitialize()
//         {
//             // Thiết lập dữ liệu cho amins.
//             MapNameAnims = new Dictionary<TypeUnitAnimState, string>();
//             SetAnimStateNames();
//         }


//         /// <summary>
//         ///     Lấy tên chuỗi của Animation dựa trên type enum được đưa vào. </summary>
//         /// ---------------------------------------------------------------------------
//         public string FunGetStringAnim(TypeUnitAnimState typeUnitAnim)
//         {
//             return (MapNameAnims.ContainsKey(typeUnitAnim) == true)
//                    ? MapNameAnims[typeUnitAnim] 
//                    : null;
//         }


//         // ---------------------------------------------------------------------------------
//         // FUNSTION HELPER
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         // Thiết lập các trạng thái hoạt ảnh mà unit có thông qua dữ liệu từ người dùng.
//         private void SetAnimStateNames()
//         {
//             if (NameAnimsUnit == null)
//                 return;

//             foreach (AnimStateNamePair pair in NameAnimsUnit)
//             {
//                 // Bỏ qua nếu unit ko chứa hoạt ảnh này.
//                 if (AnimStates.HasFlag(pair.AnimState) == false)
//                     continue;

//                 if (MapNameAnims.ContainsKey(pair.AnimState) == false)
//                     MapNameAnims.Add(pair.AnimState, pair.AnimName);
//             }
//         }
//     }
// }