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
    /// Holds information about a scene and connects a Camera and an ObjectHandler with the objects of this scene.
    /// </summary>
    public abstract class Scene
    {
        public SpriteBatch SpriteBatch { get; set; }

        protected Camera Camera;
        protected SceneObject CameraFollow;

        private ObjectHandler objectHandler;
        private List<GameObject> gameObjects;
        private List<UIElement> uIElements;

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified spriteBatch and a collection of GameObjects. 
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        /// <params name="gameObjects">A list of GameObjects this scene should start with.</param>
        public Scene(SpriteBatch spriteBatch, params GameObject[] gameObjects)
            : this(spriteBatch, new Vector2(800, 480), gameObjects)
        {
        }

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified spriteBatch, camera view size and a collection of GameObjects. 
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        /// <param name="cameraViewSize">The view size of the camera in this Scene.</param>
        /// <params name="gameObjects">A list of GameObjects this scene should start with.</param>
        public Scene(SpriteBatch spriteBatch, Vector2 cameraViewSize, params GameObject[] gameObjects)
        {
            SpriteBatch = spriteBatch;

            Camera = new Camera(cameraViewSize);
            objectHandler = new ObjectHandler();
            this.gameObjects = new List<GameObject>();
            this.uIElements = new List<UIElement>();

            this.gameObjects.AddRange(gameObjects);
        }

        /// <summary>
        /// Initializes all GameObjects and UIElements in this Scene.
        /// </summary>
        public virtual void Initialize()
        {
            foreach (GameObject obj in gameObjects)
                obj.Initialize();

            foreach (UIElement elem in uIElements)
                elem.Initialize();

            LoadContent();
        }

        /// <summary>
        /// Loads all GameObjects and UIElements to their respective handlers.
        /// </summary>
        public void LoadContent()
        {
            objectHandler.GameObjects.Clear();

            foreach (GameObject obj in gameObjects)
                objectHandler.AddObject(obj);

            // TODO: add elem to a UIObjectHandler
            //foreach (UIElement elem in uIElements)
            //    elem.Initialize();
        }

        /// <summary>
        /// Update the logic of this Scene.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public void Update(double deltaTime)
        {
            objectHandler.Update(deltaTime, Camera.View);
            if (CameraFollow != null) Camera.Transform.Position = CameraFollow.Transform.Position - Camera.ViewSize * 0.5f;
        }

        /// <summary>
        /// Draws all GameObjects within the Camera and displays any UIElements in this Scene.
        /// </summary>
        public void Draw()
        {
            GameObject[] gameObjects = objectHandler.Query(Camera.View);

            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, 
                null, null, Camera.GetTranslationMatrix());

            SpriteBatch.Draw(Game1.Pixel, new Rectangle(-1000, -1000, 2000, 2000), Color.White);

            foreach (GameObject obj in gameObjects)
            {
                if (obj.HasSpriteRenderer)
                {
                    SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                    float sortingOrder = ((float)renderer.SortingLayer * 1000f + renderer.SortingOrder + 1000f) 
                        / (Enum.GetValues(typeof(SortingLayer)).Length * 1000f + 1000f);

                    SpriteBatch.Draw(
                        renderer.Sprite.Texture, 
                        obj.Transform.WorldPosition,
                        renderer.Sprite.SourceRectangle,
                        renderer.Color,
                        MathHelper.ToRadians(obj.Transform.WorldRotation),
                        renderer.Sprite.Origin,
                        obj.Transform.Scale,
                        renderer.SpriteEffect,
                        sortingOrder);
                }
            }

            SpriteBatch.End();
        }

        /// <summary>
        /// Add a GameObject to this Scene.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        public void AddObject(GameObject gameObject)
        {
            if (gameObject == null || gameObjects.Contains(gameObject)) return;

            gameObjects.Add(gameObject);
            objectHandler.AddObject(gameObject);
        }

        /// <summary>
        /// Add a UIElement to this Scene.
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
