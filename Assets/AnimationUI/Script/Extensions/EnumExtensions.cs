using System.Runtime.CompilerServices;

namespace DhafinFawwaz.AnimationUILib.Extensions
{
    internal static class EnumExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag2(this Sequence.RtTask value, Sequence.RtTask flag)
        {
            return (value & flag) == flag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag2(this Sequence.TransTask value, Sequence.TransTask flag)
        {
            return (value & flag) == flag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag2(this Sequence.ImgTask value, Sequence.ImgTask flag)
        {
            return (value & flag) == flag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag2(this Sequence.CamTask value, Sequence.CamTask flag)
        {
            return (value & flag) == flag;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag2(this Sequence.CgTask value, Sequence.CgTask flag)
        {
            return (value & flag) == flag;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag2(this Sequence.TextMeshProTask value, Sequence.TextMeshProTask flag)
        {
            return (value & flag) == flag;
        }
    }
}
