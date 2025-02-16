// using Unity.Mathematics;

// namespace FireNBM.PathDing
// {
//     public struct Circle
//     {
//         public float2 Point;
//         public float Radius;

//         public Circle(float2 point, float radius)
//         {
//             Point = point;
//             Radius = radius;
//         }

        


//         /// <summary>
//         ///     Kiểm tra xem hai hình tròn có va chạm vào nhau hay không. </summary>
//         /// ------------------------------------------------------------------------ 
//         public static bool FunCollide(Circle a, Circle b)
//         {
//             float radius = a.Radius + b.Radius;
//             return math.lengthsq(a.Point - b.Point) < (radius * radius - math.EPSILON);
//         }

//         /// <summary>
//         ///     Tính góc tiếp tuyến từ một điểm bên ngoài hình tròn đến đường tròn. </summary>
//         /// ---------------------------------------------------------------------------------- 
//         public static float FunTangentLineToCircleAngle(float circleRadius, float distanceFromCircle)
//         {
//             float opposite = circleRadius;
//             float hypotenuse = distanceFromCircle;

//             // Tính góc tiếp tuyến sử dụng định lý pi-ta-go trong tam giác vuông.
//             var tangentLineAngle = math.asin(math.clamp(opposite / hypotenuse, -1, 1)); // asin phải nằm trong (-1, 1)
//             return tangentLineAngle;
//         }

//     }
// }