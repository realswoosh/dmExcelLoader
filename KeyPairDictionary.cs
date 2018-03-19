using System.Collections.Generic;

namespace dmExcelLoader
{
	public class KeyPair<TKey1, TKey2>
	{
		public TKey1 Key1 { get; set; }
		public TKey2 Key2 { get; set; }

		public KeyPair(TKey1 key1, TKey2 key2)
		{
			Key1 = key1;
			Key2 = key2;
		}

		public override int GetHashCode()
		{
			int hash = 17;

			hash = hash * 23 + Key1.GetHashCode();
			hash = hash * 23 + Key2.GetHashCode();

			return hash;
		}

		public override bool Equals(object obj)
		{
			KeyPair<TKey1, TKey2> o = obj as KeyPair<TKey1, TKey2>;

			if (o == null)
				return false;

			return Key1.Equals(o.Key1) && Key2.Equals(o.Key2);
		}
	}

	public class KeyPairDictionary<TKey1, TKey2, TValue> : Dictionary<KeyPair<TKey1, TKey2>, TValue>
	{
		public TValue this[TKey1 key1, TKey2 key2]
		{
			get { return this[new KeyPair<TKey1, TKey2>(key1, key2)]; }
			set { this[new KeyPair<TKey1, TKey2>(key1, key2)] = value; }
		}
	}
}
