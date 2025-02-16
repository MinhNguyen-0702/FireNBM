// using UnityEditor;
// using FireNBM.Pattern;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Một lớp kiểm tra xem các sự kiện nhận 
//     ///     thông điệp có hoạt động đúng hay ko.</summary>
//     public static class ResourceDebugger
//     {
//         // Test resource Gold
//         [MenuItem("FireNBM/Debug/Resources/+1000 Gold", priority = 0)]
//         private static void AddGold()
//         {
//             MessagingSystem.Instance.FunTriggerMessage(
//                 new MessageUpdateResource(1000, TypeResource.Gold), 
//                 false
//             );
//         }

//         [MenuItem("FireNBM/Debug/Resources/-1000 Gold", priority = 1)]
//         private static void SubtractGold()
//         {
//             MessagingSystem.Instance.FunTriggerMessage(
//                 new MessageUpdateResource(-1000, TypeResource.Gold), 
//                 false
//             );
//         }

//         // Test resource Wood
//         [MenuItem("FireNBM/Debug/Resources/+1000 Wood", priority = 2)]
//         private static void AddWood()
//         {
//             MessagingSystem.Instance.FunTriggerMessage(
//                 new MessageUpdateResource(1000, TypeResource.Wood), 
//                 false
//             );
//         }

//         [MenuItem("FireNBM/Debug/Resources/-1000 Wood", priority = 3)]
//         private static void SubtractWood()
//         {
//             MessagingSystem.Instance.FunTriggerMessage(
//                 new MessageUpdateResource(-1000, TypeResource.Wood), 
//                 false
//             );
//         }
//     }
// }