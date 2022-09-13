using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace NSW.Testing.Internal
{
    /// <summary>
    /// Random helper
    /// </summary>
    internal static partial class RandomHelper
    {
        private static readonly char[] _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ _-1234567890".ToCharArray();
        private static Random _random;
        static RandomHelper()
        {
            _random = new Random(Environment.TickCount);
        }
        /// <summary>
        /// Reset randomization
        /// </summary>
        public static void Reset() => _random = new Random(Environment.TickCount);

        /// <summary>
        /// Get random string
        /// </summary>
        /// <param name="length">String length</param>
        /// <returns>Random string</returns>
        public static string NextString(int length = 100)
        {
            if (length <=  0) throw new ArgumentOutOfRangeException($"The {length} must be greater then zero.");
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                builder.Append(_chars[NextInt(_chars.Length)]);
            }
            return builder.ToString();
        }
        /// <summary>
        /// Get random Guid
        /// </summary>
        /// <returns>Random guid</returns>
        public static Guid NextGuid() => Guid.NewGuid();
        /// <summary>
        /// Returns double in the range [0, max)
        /// </summary>
        /// <param name="max">Max int value</param>
        /// <returns>Random int</returns>
        public static int NextInt(int max = int.MaxValue) => NextInt(0, max);

        /// <summary>
        /// Returns int in the range [min, max)
        /// </summary>
        /// <param name="min">Min int value</param>
        /// <param name="max">Max int value</param>
        /// <returns>Random int</returns>
        public static int NextInt(int min, int max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            return _random.Next(min, max);
        }
        /// <summary>
        /// Returns short in the range [min, max)
        /// </summary>
        public static short NextShort(short max = short.MaxValue) => NextShort(0, max);

        /// <summary>
        /// Returns short in the range [min, max)
        /// </summary>
        public static short NextShort(short min, short max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            var rn = (max * 1.0 - min * 1.0) * _random.NextDouble() + min * 1.0;
            return Convert.ToInt16(rn);
        }
        /// <summary>
        ///Returns double in the range [0, max)
        /// </summary>
        /// <param name="max">Max double value</param>
        /// <returns>Random double</returns>
        public static double NextDouble(double max = double.MaxValue) => NextDouble(0.0, max);

        /// <summary>
        /// Returns double in the range [min, max)
        /// </summary>
        public static double NextDouble(double min, double max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            var rn = (max - min) * _random.NextDouble() + min;
            return rn;
        }
        /// <summary>
        /// Returns float (Single) in the range [0, max)
        /// </summary>
        public static float NextFloat(float max = float.MaxValue) => NextFloat(0, max);

        /// <summary>
        /// Returns float (Single) in the range [min, max)
        /// </summary>
        public static float NextFloat(float min, float max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            var rn = (max * 1.0 - min * 1.0) * _random.NextDouble() + min * 1.0;
            return Convert.ToSingle(rn);
        }
        /// <summary>
        /// Returns long in the range [0, max)
        /// </summary>
        public static long NextLong(long max = long.MaxValue) => NextLong(0, max);

        /// <summary>
        /// Returns long in the range [min, max)
        /// </summary>
        public static long NextLong(long min, long max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            var rn = (max * 1.0 - min * 1.0) * _random.NextDouble() + min * 1.0;
            return Convert.ToInt64(rn);
        }
        /// <summary>
        /// Returns byte in the range [0, max)
        /// </summary>
        public static byte NextByte(byte max = byte.MaxValue) => NextByte(0, max);

        /// <summary>
        /// Returns byte in the range [min, max)
        /// </summary>
        public static byte NextByte(byte min, byte max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            return Convert.ToByte(NextInt(min, max));
        }
        /// <summary>
        /// Get random byte array
        /// </summary>
        /// <param name="length">Byte array length</param>
        /// <returns>Random double</returns>
        public static byte[] NextBytes(int length = 100)
        {
            var bytes = new byte[length];
            _random.NextBytes(bytes);
            return bytes;
        }
        /// <summary>
        /// Get random Stream
        /// </summary>
        /// <param name="length">Stream length</param>
        /// <param name="writable">Make random Stream writable</param>
        /// <returns>Random <see cref="MemoryStream"/></returns>
        public static Stream NextStream(int length = 100, bool writable = true) => new MemoryStream(NextBytes(length), writable);

        /// <summary>
        /// Get random date between first day of current year and <see cref="DateTime.Now"/>
        /// </summary>
        /// <returns>Random date</returns>
        public static DateTime NextDateTime()
        {
            var today = DateTime.Today;
            return NextDateTime(new DateTime(today.Year, 1, 1), DateTime.Now);
        }
        /// <summary>
        /// Get random date between <paramref name="min"/> and <see cref="DateTime.Now"/>
        /// </summary>
        /// <param name="min">Start date</param>
        /// <returns>Random date</returns>
        public static DateTime NextDateTime(DateTime min) => NextDateTime(min, DateTime.Now);

        /// <summary>
        /// Get random date between <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        /// <param name="min">Start date</param>
        /// <param name="max">Finish date</param>
        /// <returns>Random date</returns>
        public static DateTime NextDateTime(DateTime min, DateTime max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            var ticks = Convert.ToInt64(NextDouble(Convert.ToDouble(min.Ticks), Convert.ToDouble(max.Ticks)));
            return new DateTime(ticks);
        }
        /// <summary>
        /// Get random <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="maxMilliseconds">Max milliseconds</param>
        /// <returns>Random TimeSpan</returns>
        public static TimeSpan NextMilliseconds(int maxMilliseconds = 1000) => TimeSpan.FromMilliseconds(NextInt(maxMilliseconds));

        /// <summary>
        /// Get random <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="maxSeconds">Max seconds</param>
        /// <returns>Random TimeSpan</returns>
        public static TimeSpan NextSeconds(int maxSeconds = 60) => TimeSpan.FromSeconds(NextInt(maxSeconds));

        /// <summary>
        /// Get random <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="maxMinutes">Max minutes</param>
        /// <returns>Random TimeSpan</returns>
        public static TimeSpan NextMinutes(int maxMinutes = 60) => TimeSpan.FromMinutes(NextInt(maxMinutes));

        /// <summary>
        /// Get random <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="maxHours">Max hours</param>
        /// <returns>Random TimeSpan</returns>
        public static TimeSpan NextHours(int maxHours = 24) => TimeSpan.FromHours(NextInt(maxHours));

        /// <summary>
        /// Returns TimeSpan in the range [min, max)
        /// </summary>
        public static TimeSpan NextTimeSpan(TimeSpan min, TimeSpan max)
        {
            if (max < min) throw new ArgumentOutOfRangeException($"The {min} should be less then {max}.");
            var ticks = Convert.ToInt64(NextDouble(Convert.ToDouble(min.Ticks), Convert.ToDouble(max.Ticks)));
            return new TimeSpan(ticks);
        }
        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        /// <returns>Random boolean value</returns>
        public static bool NextBool() => _random.NextDouble() > 0.5;

        /// <summary>
        /// Returns a uniformly random integer representing one of the values 
        /// in the enum.
        /// </summary>
        public static int NextEnum(Type enumType)
        {
            var values = (int[])Enum.GetValues(enumType);
            var randomIndex = NextInt(0, values.Length);
            return values[randomIndex];
        }
        /// <summary>
        /// This method can be used to fill all public properties of an object with random values depending on their type.
        /// CAUTION: it support only generic types
        /// </summary>
        /// <param name="obj">Input object</param>
        /// <param name="arraysSize">Size of generated arrays</param>
        /// <param name="fillNullable">Fill nullable properties</param>
        /// <returns>Object with randomized properties</returns>
        public static T Randomize<T>(this T obj, int arraysSize = 256, bool fillNullable = true)
        {
            var type = obj!.GetType();
            var infos = type.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var info in infos)
            {
                var propertyType = info.PropertyType;
                if(!info.CanWrite)
                    continue;
                var underlyingType = Nullable.GetUnderlyingType(propertyType);
                if(underlyingType != null && !fillNullable)
                    continue;
                var infoType = underlyingType ?? propertyType;

                if (infoType == typeof(string))
                    info.SetValue(obj, info.Name + " " + NextString(NextInt(1,100)), null);
                else if (infoType == typeof(byte))
                    info.SetValue(obj, NextByte(), null);
                else if (infoType == typeof(int))
                    info.SetValue(obj, NextInt(), null);
                else if (infoType == typeof(long))
                    info.SetValue(obj, NextLong(), null);
                else if (infoType == typeof(double))
                    info.SetValue(obj, NextDouble(), null);
                else if (infoType == typeof(short))
                    info.SetValue(obj, NextShort(), null);
                else if (infoType == typeof(float))
                    info.SetValue(obj, NextFloat(), null);
                else if (infoType == typeof(uint))
                    info.SetValue(obj, Convert.ToUInt32(NextInt()), null);
                else if (infoType == typeof(ulong))
                    info.SetValue(obj, Convert.ToUInt64(NextLong()), null);
                else if (infoType == typeof(ushort))
                    info.SetValue(obj, Convert.ToUInt16(NextShort()), null);
                else if (infoType == typeof(bool))
                    info.SetValue(obj, NextBool(), null);
                else if (infoType == typeof(byte[]))
                    info.SetValue(obj, NextBytes(arraysSize), null);
                else if (infoType == typeof(Guid))
                    info.SetValue(obj, NextGuid(), null);
                else if (infoType == typeof(TimeSpan))
                    info.SetValue(obj, NextSeconds(), null);
                if (infoType == typeof(DateTime))
                    info.SetValue(obj, NextDateTime(), null);
                else if (infoType == typeof(Stream) || infoType == typeof(MemoryStream))
                    info.SetValue(obj, NextStream(arraysSize), null);
                else if (infoType.GetTypeInfo().IsEnum)
                    info.SetValue(obj, Enum.ToObject(infoType,NextEnum(infoType)), null);
            }
            return obj;
        }
    }
}