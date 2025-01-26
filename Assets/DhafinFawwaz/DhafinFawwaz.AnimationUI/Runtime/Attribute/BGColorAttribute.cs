using UnityEngine;
using System;
using System.Collections.Generic;

namespace DhafinFawwaz.AnimationUI {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public class BGColorAttribute : Attribute {

		public Color Color { get; }
		static Dictionary<Type, BGColorAttribute> _cache = new Dictionary<Type, BGColorAttribute>();

		public BGColorAttribute (string colorCode) {
			UnityEngine.ColorUtility.TryParseHtmlString(colorCode, out Color col);
			Color = col;
		}

		public static bool TryFindThisOrAnyParentContainBGColorAttribute(Type type, out BGColorAttribute attr) {
			if(type == null) {
				attr = null;
				return false;
			}
			if(_cache.TryGetValue(type, out attr)) return true;
			attr = Attribute.GetCustomAttribute(type, typeof(BGColorAttribute)) as BGColorAttribute;
			if(attr != null) {
				_cache[type] = attr;
				return true;
			}
			return TryFindThisOrAnyParentContainBGColorAttribute(type.BaseType, out attr);
		}
	}
}	