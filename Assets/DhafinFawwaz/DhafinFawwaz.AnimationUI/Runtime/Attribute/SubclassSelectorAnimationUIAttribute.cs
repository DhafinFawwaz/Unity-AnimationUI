using System;
using UnityEngine;
namespace DhafinFawwaz.AnimationUI {
	/// <summary>
	/// Attribute to specify the type of the field serialized by the SerializeReference attribute in the inspector.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class SubclassSelectorAnimationUIAttribute : SubclassSelectorAttribute
	{}
}