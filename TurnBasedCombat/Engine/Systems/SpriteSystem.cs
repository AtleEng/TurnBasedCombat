using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using CoreEngine;
using Engine;

namespace CoreEngine
{
    public class SpriteSystem : GameSystem
    {
        public float scale;
        int offsetX;
        int offsetY;

        RenderTexture2D target = new();

        float gameScreenWidth;
        float gameScreenHeight;

        public override void Start()
        {
            System.Console.WriteLine("Innit window");
            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);

            Raylib.InitWindow(WindowSettings.startWindowWidth, WindowSettings.startWindowHeight, "Game Window");
            Raylib.SetWindowMinSize(400, 300);

            Raylib.SetTargetFPS(10);

            Raylib.SetExitKey(KeyboardKey.KEY_NULL);

            target = Raylib.LoadRenderTexture(WindowSettings.gameScreenWidth, WindowSettings.gameScreenHeight);
            SetValuesOfWindow();

        }
        public override void Update(float delta)
        {
            if (Raylib.IsWindowResized())
            {
                SetValuesOfWindow();
            }

            Raylib.BeginTextureMode(target);
            Raylib.ClearBackground(new Color(41, 189, 193, 255));
            //Render all sprites
            RenderAll();
            Raylib.EndTextureMode();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            Raylib.DrawTexturePro(target.texture,
    new Rectangle(0.0f, 0.0f, (float)target.texture.width, (float)-target.texture.height),
    new Rectangle(offsetX, offsetY, gameScreenWidth * scale, gameScreenHeight * scale),
    new Vector2(0, 0), 0.0f, Color.WHITE);

            Raylib.EndDrawing();

            if (Core.shouldClose)
            {
                Raylib.UnloadRenderTexture(target);
                Raylib.CloseWindow();
            }

            if (Raylib.WindowShouldClose())
            {
                Core.shouldClose = true;
            }
        }
        void RenderAll()
        {
            DisplayGrid();
            //render all entities's spriteRenderers
            foreach (GameEntity gameEntity in Core.gameEntities)
            {

                Sprite? spriteComponent = gameEntity.components.ContainsKey(typeof(Sprite)) ? gameEntity.components[typeof(Sprite)] as Sprite : null;
                if (spriteComponent != null)
                {
                    Vector2 p = WorldSpace.ConvertToCameraPosition(gameEntity.transform.position);
                    Vector2 s = WorldSpace.ConvertToCameraSize(gameEntity.transform.size);

                    Rectangle destRec = new Rectangle(
                    (int)p.X - (int)(s.X / 2), (int)p.Y - (int)(s.Y / 2), //pos
                    (int)s.X, (int)s.Y //size
                    );

                    Raylib.DrawRectangleRec(destRec, new Color(255, 255, 255, 100));

                    if (spriteComponent.spriteSheet.id != 0)
                    {
                        int flipX = spriteComponent.isFlipedX ? -1 : 1;
                        int flipY = spriteComponent.isFlipedY ? -1 : 1;

                        int i = spriteComponent.FrameIndex;

                        int x = (int)spriteComponent.spriteGrid.X;
                        int y = (int)spriteComponent.spriteGrid.Y;

                        float gridSizeX = spriteComponent.spriteSheet.width / x;
                        float gridSizeY = spriteComponent.spriteSheet.height / y;

                        int posX = i % x;
                        int posY = i / x;

                        Rectangle source = new Rectangle(
                            (int)(posX * gridSizeX),
                            (int)(posY * gridSizeY),
                            spriteComponent.spriteSheet.width * flipX / spriteComponent.spriteGrid.X,
                            spriteComponent.spriteSheet.height * flipY / spriteComponent.spriteGrid.Y
                        );

                        Raylib.DrawTexturePro(spriteComponent.spriteSheet, source, destRec, Vector2.Zero, 0, spriteComponent.colorTint);
                    }
                    Raylib.DrawCircle((int)p.X, (int)p.Y, 5, Color.RED);
                }
                Raylib.DrawText($"GameEntitys:{Core.gameEntities.Count}\nFPS:{Raylib.GetFPS()}", 20, 20, 20, Color.RAYWHITE);
            }
        }
        void SetValuesOfWindow()
        {
            gameScreenWidth = WindowSettings.gameScreenWidth;
            gameScreenHeight = WindowSettings.gameScreenHeight;

            float screenAspectRatio = (float)Raylib.GetScreenWidth() / Raylib.GetScreenHeight();
            float gameAspectRatio = (float)gameScreenWidth / gameScreenHeight;

            if (screenAspectRatio > gameAspectRatio)
            {
                // Window is wider than the game screen
                scale = (float)Raylib.GetScreenHeight() / gameScreenHeight;
                offsetX = (int)((Raylib.GetScreenWidth() - (gameScreenWidth * scale)) * 0.5f);
                offsetY = 0;
            }
            else
            {
                // Window is taller than the game screen
                scale = (float)Raylib.GetScreenWidth() / gameScreenWidth;
                offsetX = 0;
                offsetY = (int)((Raylib.GetScreenHeight() - (gameScreenHeight * scale)) * 0.5f);
            }
        }

        void DisplayGrid()
        {
            int gridSize = 20;
            Vector2 spX = WorldSpace.ConvertToCameraPosition(new Vector2(gridSize, 0));

            Vector2 epX = WorldSpace.ConvertToCameraPosition(new Vector2(-gridSize, 0));
            Raylib.DrawLine((int)spX.X, (int)spX.Y, (int)epX.X, (int)epX.Y, Color.RED);

            Vector2 spY = WorldSpace.ConvertToCameraPosition(new Vector2(0, gridSize));

            Vector2 epY = WorldSpace.ConvertToCameraPosition(new Vector2(0, -gridSize));
            Raylib.DrawLine((int)spY.X, (int)spY.Y, (int)epY.X, (int)epY.Y, Color.BLUE);

            for (int x = -gridSize; x < gridSize; x++)
            {
                Vector2 sp = WorldSpace.ConvertToCameraPosition(new Vector2(x + 0.5f, gridSize + 0.5f));

                Vector2 ep = WorldSpace.ConvertToCameraPosition(new Vector2(x + 0.5f, -gridSize - 0.5f));
                Raylib.DrawLine((int)sp.X, (int)sp.Y, (int)ep.X, (int)ep.Y, Color.RAYWHITE);
            }
            for (int y = -gridSize; y < gridSize; y++)
            {
                Vector2 sp = WorldSpace.ConvertToCameraPosition(new Vector2(gridSize + 0.5f, y + 0.5f));

                Vector2 ep = WorldSpace.ConvertToCameraPosition(new Vector2(-gridSize - 0.5f, y + 0.5f));
                Raylib.DrawLine((int)sp.X, (int)sp.Y, (int)ep.X, (int)ep.Y, Color.RAYWHITE);
            }
        }
    }
}