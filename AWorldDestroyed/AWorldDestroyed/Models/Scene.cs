// =============================================
//         Editor:     Lone Maaherra
//         Last edit:  2020-03-19 
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
using System.Collections.Generic;
using AWorldDestroyed.GUI;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Holds information about a scene and connects a Camera and an ObjectHandler with the objects of SceneLayers.
    /// </summary>
    public abstract class Scene
    {
        public SpriteBatch SpriteBatch { get; set; }

        private Camera camera;
        private ObjectHandler objectHandler;
        private List<GameObject> gameObjects;
        private List<UIElement> uIElements;

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified GameObject. 
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        /// <params name="gameObjects">A variable number of gameObjects.</param>
        public Scene(SpriteBatch spriteBatch, params GameObject[] gameObjects)
            : this(spriteBatch, new Vector2(800, 480), gameObjects)
        {
        }

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified GameObject. 
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        /// <params name="gameObjects">A variable number of gameObjects.</param>
        public Scene(SpriteBatch spriteBatch, Vector2 cameraViewSize, params GameObject[] gameObjects)
        {
            SpriteBatch = spriteBatch;

            camera = new Camera(cameraViewSize);
            objectHandler = new ObjectHandler();
            this.gameObjects = new List<GameObject>();
            this.uIElements = new List<UIElement>();

            this.gameObjects.AddRange(gameObjects);
        }

        /// <summary>
        /// Initializes all GameObjects and UIElements in this Scene.
        /// </summary>
        public void Initialize()
        {
            foreach (GameObject obj in gameObjects)
                obj.Initialize();

            foreach (UIElement elem in uIElements)
                elem.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadContent()
        {
            objectHandler.GameObjects.Clear();

            foreach (GameObject obj in gameObjects)
                objectHandler.AddObject(obj);

            // TODO: add elem to a UIObjectManager
            //foreach (UIElement elem in uIElements)
            //    elem.Initialize();
        }

        /// <summary>
        /// Update the logic of the Scene.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public void Update(double deltaTime)
        {
            objectHandler.Update(deltaTime, camera.View);
        }

        public void Draw()
        {
            GameObject[] gameObjects = objectHandler.Query(camera.View);

            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            foreach (GameObject obj in gameObjects)
            {
                if (obj.HasSpriteRenderer)
                {
                    SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                    float sortingOrder = ((float)renderer.SortingLayer * 1000f + renderer.SortingOrder + 1000f) 
                        / (Enum.GetValues(typeof(SortingLayer)).Length * 1000f + 1000f);

                    SpriteBatch.Draw(
                        renderer.Sprite.Texture, 
                        obj.Transform.Position,
                        renderer.Sprite.SourceRectangle,
                        renderer.Color,
                        obj.Transform.Rotation,
                        renderer.Sprite.Origin,
                        obj.Transform.Scale,
                        renderer.SpriteEffect,
                        sortingOrder);
                }
            }

            SpriteBatch.End();
        }

        /// <summary>
        /// Add a GameObject to the Scene.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        public void AddObject(GameObject gameObject)
        {
            if (gameObject == null || gameObjects.Contains(gameObject)) return;
            
            gameObjects.Add(gameObject);
            objectHandler.AddObject(gameObject);
        }

        /// <summary>
        /// Add a UIElement to the Scene.
        /// </summary>
        /// <param name="uIElement">The UIElement to add.</param>
        public void AddUIObject(UIElement uIElement)
        {
            if (uIElement == null || uIElements.Contains(uIElement))
                return;

            uIElements.Add(uIElement);
        }
    }
}
