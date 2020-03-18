using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AWorldDestroyed
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        SpriteFont font;
        MouseState lastMouse;
        
        Rectangle windowRect;
        Rectangle queryRect;

        QuadTree<Point> quadTree;
        List<Point> queriedPoints;
        List<Point> points;
        bool doQuery;
        int pointSize;
        int treeMargin;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            windowRect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            queryRect = new Rectangle(0, 0, 200, 120);

            lastMouse = Mouse.GetState();

            quadTree = new QuadTree<Point>(windowRect, 4);
            queriedPoints = new List<Point>();
            points = new List<Point>();
            pointSize = 6;
            treeMargin = 30;

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");
            Paint.Init(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            Window.Title = $"FPS: {(1 / gameTime.ElapsedGameTime.TotalSeconds):.0}";

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))  Initialize();

            quadTree = new QuadTree<Point>(
                new Rectangle(0, treeMargin, windowRect.Width, windowRect.Height - treeMargin * 2),
                4);

            // Left mouse clicked.
            if (Mouse.GetState().LeftButton == ButtonState.Pressed 
                && (Mouse.GetState().Position.Y > treeMargin && Mouse.GetState().Position.Y < windowRect.Height - treeMargin))
                points.Add(Mouse.GetState().Position - new Point(pointSize / 2));

            // Add all points to the QuadTree.
            foreach (Point position in points)
                quadTree.Insert(position.ToVector2(), position);
            
            // Query all points and move queryRect.
            doQuery = Mouse.GetState().RightButton == ButtonState.Pressed;
            if (doQuery)
            {
                queryRect.Location = Mouse.GetState().Position - new Point(queryRect.Width / 2, queryRect.Height / 2);
                queriedPoints = quadTree.Query(queryRect);
            }
            else
                queriedPoints.Clear();

            lastMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(40, 40, 40));

            spriteBatch.Begin();

            // Draw the QuadTree.
            DrawQuadTree(quadTree, 1);

            // Draw queryRect
            if (doQuery)
                Paint.DrawOutlinedRect(spriteBatch, queryRect, 3, Color.Green);

            // Draw all points.
            foreach (Point point in points)
            {
                if (queriedPoints.Contains(point))
                    Paint.DrawRect(spriteBatch, new Rectangle(point, new Point(pointSize)), Color.Blue);
                else
                    Paint.DrawRect(spriteBatch, new Rectangle(point, new Point(pointSize)), Color.White);
            }

            // Draw Text.
            string text = $"Total points: {points.Count}  |  Total queried points: {queriedPoints.Count}";
            string infoText = "left mouse to insert point  |  right mouse to query  |  press space to reset";
            DrawCenteredText(text, new Vector2(windowRect.Width / 2f, treeMargin / 2f), windowRect.Width, Color.White);
            DrawCenteredText(infoText, new Vector2(windowRect.Width / 2f, windowRect.Height - treeMargin / 2f), windowRect.Width, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCenteredText(string text, Vector2 position, float totalWidth, Color color)
        {
            spriteBatch.DrawString(font, text, position, color, 0f,
                font.MeasureString(text) / 2f,
                1f, SpriteEffects.None, 0f);
        }

        private void DrawQuadTree<T>(QuadTree<T> quadTree, int stroke)
        {
            if (quadTree.Boundary.Intersects(queryRect) && doQuery)
                Paint.DrawOutlinedRect(spriteBatch, quadTree.Boundary, stroke, new Color(25, 25, 25));
            else
                Paint.DrawOutlinedRect(spriteBatch, quadTree.Boundary, stroke, new Color(90, 90, 90));

            if (quadTree.NorthWest != null) DrawQuadTree(quadTree.NorthWest, stroke);
            if (quadTree.NorthEast != null) DrawQuadTree(quadTree.NorthEast, stroke);
            if (quadTree.SouthWest != null) DrawQuadTree(quadTree.SouthWest, stroke);
            if (quadTree.SouthEast != null) DrawQuadTree(quadTree.SouthEast, stroke);
        }
    }
}
