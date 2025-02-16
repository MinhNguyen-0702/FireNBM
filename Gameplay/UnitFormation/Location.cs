using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Lưu trữ vị trí và hướng.
    /// </summary>
    public class Location
    {
        public Vector3 Position;
        public Quaternion Orientation;


        // ----------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        // //////////////////////////////////////////////////////////

        public Location(Vector3 pos, Quaternion orientation)
            => (Position, Orientation) = (pos, orientation);

        public Location() : this(Vector3.zero, Quaternion.identity){}
        public Location(Vector3 pos) : this(pos, Quaternion.identity){}
    }
}