// =============================================
//         Editor:     Philip  Abrahamsson
//         Last edit:  2020-03-21 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// This is used to display sprites.
    /// What layer to display them on.
    /// </summary>
    public class SpriteRenderer : Component
    {
        public Sprite Sprite { get; set; }
        public Color Color { get; set; }
        public int SortingOrder { get; set; }
        public SortingLayer SortingLayer { get; set; }
        public SpriteEffect SpriteEffect { get; set; }

        public SpriteRenderer(GameObject gameObject, Sprite sprite)
        {
            Sprite = sprite;
        }

        /// <summary>
        /// Creates a copy of SpriteRenderer object.
        /// </summary>
        /// <returns>SpriteRenderer object</returns>
        public object Copy()
        {
            return this.MemberwiseClone();
        }
    }
}