using UnityEngine;
using System;

namespace DhafinFawwaz.AnimationUI {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public class BGColorAttribute : Attribute {

		public Color Color { get; }

		public BGColorAttribute (string colorCode) {
			UnityEngine.ColorUtility.TryParseHtmlString(colorCode, out Color col);
			Color = col;
		}

		public static bool TryFindThisOrAnyParentContainBGColorAttribute(Type type, out BGColorAttribute attr) {
			if(type == null) {
				attr = null;
				return false;
			}
			attr = Attribute.GetCustomAttribute(type, typeof(BGColorAttribute)) as BGColorAttribute;
			if(attr != null) return true;
			return TryFindThisOrAnyParentContainBGColorAttribute(type.BaseType, out attr);
		}
	}
}	