﻿#pragma warning disable CS8669
using System;namespace FileGenerate
{
    [System.ComponentModel.TypeConverter(typeof(BTypeConverter))]
    readonly partial struct B : IEquatable<B>
    {
        readonly string value;

        public string AsPrimitive() => value;

        public B(string value)
        {
            this.value = value;
        }
        
        public static implicit operator string(B value)
        {
            return value.value;
        }
        
        public static implicit operator B(string value)
        {
            return new B(value);
        }
        
        public bool Equals(B other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj.GetType();
            if (t == typeof(B))
            {
                return Equals((B)obj);
            }
            if (t == typeof(string))
            {
                return value.Equals((string)obj);
            }

           return value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        
        public override string ToString()
        {
            return value.ToString();
        }
        
        public static bool operator ==(in B x, in B y)
        {
            return x.value.Equals(y.value);
        }

        public static bool operator !=(in B x, in B y)
        {
            return !x.value.Equals(y.value);
        }

        // Default
        private class BTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(B);
            private static readonly Type ValueType = typeof(string);

            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == WrapperType || sourceType == ValueType)
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == WrapperType || destinationType == ValueType)
                {
                    return true;
                }

                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value != null)
                {
                    var t = value.GetType();
                    if (t == typeof(B))
                    {
                        return (B)value;
                    }
                    if (t == typeof(string))
                    {
                        return new B((string)value);
                    }
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is B wrappedValue)
                {
                    if (destinationType == WrapperType)
                    {
                        return wrappedValue;
                    }

                    if (destinationType == ValueType)
                    {
                        return wrappedValue.AsPrimitive();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
