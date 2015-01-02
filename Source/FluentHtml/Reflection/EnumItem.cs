using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FluentHtml.Reflection
{
    /// <summary>
    /// A class representing an Enum value.
    /// </summary>
    public class EnumItem : EnumItem<object>
    {
    }


    /// <summary>
    /// A class representing an Enum value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <see cref="Enum"/>.</typeparam>
    public class EnumItem<TEnum>
        : IEquatable<EnumItem<TEnum>>
    {
        /// <summary>
        /// Gets or sets the string value of the enumeration.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description from <see cref="DescriptionAttribute"/>.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the underlying type of the specified enumeration.
        /// </summary>
        public object UnderlyingValue { get; set; }

        /// <summary>
        /// Gets or sets the enumeration value.
        /// </summary>
        public TEnum Value { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Description ?? Name;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        public bool Equals(EnumItem<TEnum> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return Value.Equals(other.Value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((EnumItem<TEnum>)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(EnumItem<TEnum> left, EnumItem<TEnum> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EnumItem<TEnum> left, EnumItem<TEnum> right)
        {
            return !Equals(left, right);
        }

        private static readonly Lazy<IList<EnumItem<TEnum>>> _items = new Lazy<IList<EnumItem<TEnum>>>(() =>
            CreateList().ToList().AsReadOnly());

        /// <summary>
        /// Gets a read only list of <see cref="EnumItem"/>.
        /// </summary>
        public static IList<EnumItem<TEnum>> Items
        {
            get { return _items.Value; }
        }

        /// <summary>
        /// Creates an <see cref="EnumItem"/> from specified Enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the Enum.</typeparam>
        /// <param name="enumValue">The Enum value.</param>
        /// <returns></returns>
        public static EnumItem<TEnum> Create(TEnum enumValue)
        {
            return Items.FirstOrDefault(i => enumValue.Equals(i.Value));
        }

        /// <summary>
        /// Creates a list of <see cref="EnumItem"/> from specified <typeparamref name="TEnum"/> type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns></returns>
        public static IEnumerable<EnumItem<TEnum>> CreateList()
        {
            var enumType = typeof(TEnum);
            return CreateList(enumType);
        }

        /// <summary>
        /// Creates a list of <see cref="EnumItem" /> from specified <typeparamref name="TEnum" /> type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static IEnumerable<EnumItem<TEnum>> CreateList(Type enumType)
        {
            return from field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                   let attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                       .OfType<DescriptionAttribute>()
                       .FirstOrDefault()
                   let description = attribute == null ? field.Name : attribute.Description
                   let underlyingValue = field.GetRawConstantValue()
                   let original = Enum.ToObject(enumType, underlyingValue)
                   select new EnumItem<TEnum>
                   {
                       Name = field.Name,
                       Description = description,
                       UnderlyingValue = underlyingValue,
                       Value = (TEnum)original
                   };
        }
    }

}