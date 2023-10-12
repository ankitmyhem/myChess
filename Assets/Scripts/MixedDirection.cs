using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace ProjectScope
{
    public struct MixedVector3
    {
        private static readonly Vector3 upwardLeft = Vector3.Normalize(Vector3.up + Vector3.left);

        private static readonly Vector3 downwardLeft = Vector3.Normalize(Vector3.down + Vector3.left);

        private static readonly Vector3 upwardRight = Vector3.Normalize(Vector3.up + Vector3.right);

        private static readonly Vector3 downwardRight = Vector3.Normalize(Vector3.down + Vector3.right);

        private static readonly Vector3 forwardUp = Vector3.Normalize(Vector3.forward + Vector3.up);

        private static readonly Vector3 forwardDown = Vector3.Normalize(Vector3.forward + Vector3.down);

        private static readonly Vector3 backwardUp = Vector3.Normalize(Vector3.back + Vector3.up);

        private static readonly Vector3 backwardDown = Vector3.Normalize(Vector3.back + Vector3.down);

        private static readonly Vector3 forward_Down_Left = Vector3.Normalize(Vector3.forward + downwardLeft);

        private static readonly Vector3 forward_Down_Right = Vector3.Normalize(Vector3.forward + downwardRight);

        private static readonly Vector3 forward_Up_Left = Vector3.Normalize(Vector3.forward + upwardLeft);

        private static readonly Vector3 forward_Up_Right = Vector3.Normalize(Vector3.forward + upwardRight);

        private static readonly Vector3 backward_Down_Left = Vector3.Normalize(Vector3.back + downwardLeft);

        private static readonly Vector3 backward_Down_Right = Vector3.Normalize(Vector3.back + downwardRight);

        private static readonly Vector3 backward_Up_Left = Vector3.Normalize(Vector3.back +  upwardLeft);

        private static readonly Vector3 backward_Up_Right = Vector3.Normalize(Vector3.back + upwardRight);

        public static Vector3 UpLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return upwardLeft;
            }
        }

        public static Vector3 DownLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return downwardLeft;
            }
        }

        public static Vector3 UpRight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return upwardRight;
            }
        }

        public static Vector3 DownRight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return downwardRight;
            }
        }

        public static Vector3 ForwardUp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return forwardUp;
            }
        }

        public static Vector3 ForwardDown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return forwardDown;
            }
        }

        public static Vector3 BackWardUp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return backwardUp;
            }
        }

        public static Vector3 BackwardDown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return backwardDown;
            }
        }
        public static Vector3 ForwardUpLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return forward_Up_Left;
            }
        }

        public static Vector3 ForwardUpRight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return forward_Up_Right;
            }
        }

        public static Vector3 ForwardDownLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return forward_Down_Left;
            }
        }

        public static Vector3 ForwardDownRight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return forward_Down_Right;
            }
        }

        public static Vector3 BackwardDownLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return backward_Down_Left; }
        }

        public static Vector3 BackwardDownRight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => backward_Down_Right;
        }

        public static Vector3 BackwardUpLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return backward_Up_Left; }
        }

        public static Vector3 BackwardUpRight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return backward_Up_Right; }
        }
    }
}
