using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DhafinFawwaz.AnimationUILib.Demo
{
    [Serializable]
    /// <summary>
    /// Structure that stores the state of a Scale transition on a Selectable.
    /// </summary>
    public struct ScaleBlock : IEquatable<ScaleBlock>
    {
        [SerializeField] private float m_NormalScale;
        [SerializeField] private float m_HighlightedScale;
        [SerializeField] private float m_PressedScale;
        [SerializeField] private float m_SelectedScale;
        [SerializeField] private float m_DisabledScale;
        [Range(1, 5)] [SerializeField] private float m_ScaleMultiplier;
        [SerializeField] private float m_FadeDuration;

        /// <summary>
        /// The normal Scale for this Scale block.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI; // Required when Using UI elements.
        ///
        /// public class ExampleClass : MonoBehaviour
        /// {
        ///     public Button button;
        ///     public float newScale;
        ///
        ///     void Start()
        ///     {
        ///         //Changes the button's Normal Scale to the new Scale.
        ///         ScaleBlock cb = button.Scales;
        ///         cb.normalScale = newScale;
        ///         button.Scales = cb;
        ///     }
        /// }
        /// ]]>
        ///</code>
        /// </example>
        public float normalScale       { get { return m_NormalScale; } set { m_NormalScale = value; } }

        /// <summary>
        /// The highlight Scale for this Scale block.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI; // Required when Using UI elements.
        ///
        /// public class ExampleClass : MonoBehaviour
        /// {
        ///     public Button button;
        ///     public float newScale;
        ///
        ///     void Start()
        ///     {
        ///         //Changes the button's Highlighted Scale to the new Scale.
        ///         ScaleBlock cb = button.Scales;
        ///         cb.highlightedScale = newScale;
        ///         button.Scales = cb;
        ///     }
        /// }
        /// ]]>
        ///</code>
        /// </example>
        public float highlightedScale  { get { return m_HighlightedScale; } set { m_HighlightedScale = value; } }

        /// <summary>
        /// The pressed Scale for this Scale block.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI; // Required when Using UI elements.
        ///
        /// public class ExampleClass : MonoBehaviour
        /// {
        ///     public Button button;
        ///     public float newScale;
        ///
        ///     void Start()
        ///     {
        ///         //Changes the button's Pressed Scale to the new Scale.
        ///         ScaleBlock cb = button.Scales;
        ///         cb.pressedScale = newScale;
        ///         button.Scales = cb;
        ///     }
        /// }
        /// ]]>
        ///</code>
        /// </example>
        public float pressedScale      { get { return m_PressedScale; } set { m_PressedScale = value; } }

        /// <summary>
        /// The selected Scale for this Scale block.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI; // Required when Using UI elements.
        ///
        /// public class ExampleClass : MonoBehaviour
        /// {
        ///     public Button button;
        ///     public float newScale;
        ///
        ///     void Start()
        ///     {
        ///         //Changes the button's Selected Scale to the new Scale.
        ///         ScaleBlock cb = button.Scales;
        ///         cb.selectedScale = newScale;
        ///         button.Scales = cb;
        ///     }
        /// }
        /// ]]>
        ///</code>
        /// </example>
        public float selectedScale     { get { return m_SelectedScale; } set { m_SelectedScale = value; } }

        /// <summary>
        /// The disabled Scale for this Scale block.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using UnityEngine;
        /// using System.Collections;
        /// using UnityEngine.UI; // Required when Using UI elements.
        ///
        /// public class ExampleClass : MonoBehaviour
        /// {
        ///     public Button button;
        ///     public float newScale;
        ///
        ///     void Start()
        ///     {
        ///         //Changes the button's Disabled Scale to the new Scale.
        ///         ScaleBlock cb = button.Scales;
        ///         cb.disabledScale = newScale;
        ///         button.Scales = cb;
        ///     }
        /// }
        /// ]]>
        ///</code>
        /// </example>
        public float disabledScale     { get { return m_DisabledScale; } set { m_DisabledScale = value; } }

        /// <summary>
        /// Multiplier applied to Scales (allows brightening greater then base Scale).
        /// </summary>
        public float ScaleMultiplier   { get { return m_ScaleMultiplier; } set { m_ScaleMultiplier = value; } }

        /// <summary>
        /// How long a Scale transition between states should take.
        /// </summary>
        public float fadeDuration      { get { return m_FadeDuration; } set { m_FadeDuration = value; } }

        /// <summary>
        /// Simple getter for a code generated default ScaleBlock.
        /// </summary>
        public static ScaleBlock defaultScaleBlock;

        static ScaleBlock()
        {
            defaultScaleBlock = new ScaleBlock
            {
                m_NormalScale      = 1,
                m_HighlightedScale = 1.1f,
                m_PressedScale     = 1.2f,
                m_SelectedScale    = 1.1f,
                m_DisabledScale    = 1,
                ScaleMultiplier    = 1.0f,
                fadeDuration       = 0.15f
            };
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ScaleBlock))
                return false;

            return Equals((ScaleBlock)obj);
        }

        public bool Equals(ScaleBlock other)
        {
            return normalScale == other.normalScale &&
                highlightedScale == other.highlightedScale &&
                pressedScale == other.pressedScale &&
                selectedScale == other.selectedScale &&
                disabledScale == other.disabledScale &&
                ScaleMultiplier == other.ScaleMultiplier &&
                fadeDuration == other.fadeDuration;
        }

        public static bool operator==(ScaleBlock point1, ScaleBlock point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator!=(ScaleBlock point1, ScaleBlock point2)
        {
            return !point1.Equals(point2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}