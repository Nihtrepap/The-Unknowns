﻿// =============================================
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

using System;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Models.Components
{
    /// <summary>
    /// Determines how to render a sprite.
    /// </summary>
    public class SpriteRenderer : Component
    {
        public Sprite Sprite { get; set; }
        public Color Color { get; set; }
        public int SortingOrder { get; set; }
        public SortingLayer SortingLayer { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public bool FlipX { get; set; }
        public bool FlipY { get; set; }

        /// <summary>
        /// Create a new SpriteRenderer instance.
        /// </summary>
        public SpriteRenderer()
        {
            Color = Color.White;
            SortingOrder = 0;
            SortingLayer = (SortingLayer)Enum.Parse(typeof(SortingLayer), "Default");
            SpriteEffect = SpriteEffects.None;
        }

        /// <summary>
        /// Create a new SpriteRenderer instance with a specified sprite.
        /// </summary>
        /// <param name="sprite">The sprite this SpriteRenderer will hold.</param>
        public SpriteRenderer(Sprite sprite) : this()
        {
            Sprite = sprite;
        }

        /// <summary>
        /// Make a new SpriteRenderer instance with the same attribute values as this instance.
        /// </summary>
        /// <returns>A copy of this SpriteRenderer instance.</returns>
        public override Component Copy()
        {
            return new SpriteRenderer(this.Sprite)
            {
                Color = this.Color,
                SortingOrder = this.SortingOrder,
                SortingLayer = this. SortingLayer,
                SpriteEffect = this.SpriteEffect
            };
        }
    }
}