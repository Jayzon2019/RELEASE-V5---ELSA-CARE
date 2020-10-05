using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public abstract class Enumeration<TKey> where TKey : IComparable
	{
		private readonly TKey id;
		private readonly string name;

		protected Enumeration() { }

		protected Enumeration(TKey id, string name)
		{
			this.id = id;
			this.name = name;
		}

		public TKey Id
		{
			get { return this.id; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public override string ToString()
		{
			return Name;
		}

		public static IEnumerable<T> GetAll<T>() where T : Enumeration<TKey>, new()
		{
			var type = typeof(T);
			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

			foreach (var info in fields)
			{
				var instance = new T();
				var locatedValue = info.GetValue(instance) as T;

				if (locatedValue != null)
				{
					yield return locatedValue;
				}
			}
		}

		public static IEnumerable<Enumeration<TKey>> GetAll(Type type)
		{
			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

			foreach (var info in fields)
			{
				var instance = Activator.CreateInstance(type);
				yield return (Enumeration<TKey>)info.GetValue(instance);
			}
		}

		public override bool Equals(object obj)
		{
			var otherValue = obj as Enumeration<TKey>;

			if (otherValue == null)
			{
				return false;
			}

			var typeMatches = GetType().Equals(obj.GetType());
			var idMatches = this.id.Equals(otherValue.Id);

			return typeMatches && idMatches;
		}

		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		public static T FromId<T>(TKey id, T defaultValue = null) where T : Enumeration<TKey>, new()
		{
			var matchingItem = Parse<T, TKey>(id, "id", item => 
				EqualityComparer<TKey>.Default.Equals(item.Id, id));

			if (matchingItem == null)
				matchingItem = defaultValue;

			return matchingItem;
		}

		public static T FromName<T>(string name, T defaultValue = null) where T : Enumeration<TKey>, new()
		{
			var matchingItem = Parse<T, string>(name, "display name", item => item.Name == name);

			if (matchingItem == null)
				matchingItem = defaultValue;

			return matchingItem;
		}

		public virtual int CompareTo(object other)
		{
			return Id.CompareTo(((Enumeration<TKey>)other).Id);
		}

		public static Enumeration<TKey> FromIdOrDefault(Type enumerationType, TKey enumerationId)
		{
			return GetAll(enumerationType).SingleOrDefault(e =>
				EqualityComparer<TKey>.Default.Equals(e.Id, enumerationId));
		}

		public static Enumeration<TKey> FromNameOrDefault(Type enumerationType, string name)
		{
			return GetAll(enumerationType).SingleOrDefault(e => e.Name == name);
		}


		private static T Parse<T, K>(K id, string description, Func<T, bool> predicate) where T : Enumeration<TKey>, new()
		{
			var matchingItem = GetAll<T>().FirstOrDefault(predicate);

			if (matchingItem == null)
			{
				var message = string.Format("'{0}' is not a valid {1} in {2}", id, description, typeof(T));
				//TODO:
				//throw new ApplicationException(message);
			}

			return matchingItem;
		}
	}
}