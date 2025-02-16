// using System.Collections.Generic;
// using UnityEngine;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Là một lớp dữ liệu chứa danh sách các lớp LevelItem.</summary>
//     [CreateAssetMenu(menuName = "FireNBM/New Configuration")]
//     public class LevelConfiguration : ScriptableObject
//     {
//         public List<LevelItem> LevelItems = new List<LevelItem>();

//         /// <summary>
//         ///     Trả về đối tượng dựa trên kiểu được gắn trên nó.</summary>
//         public LevelItem FunFindByType(LevelItemType type)
//         {
//             // Finds the first occurrence of the type in the list or return null otherwise.
//             return LevelItems.Find(item => item.Type == type);
//         }
//     }
// }