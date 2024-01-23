
//######## Laborator EGC Cocriș Iulian, grupa 3132A #############//

using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.IO;

namespace ControlObiectRandareOpenTK
{
    class Game : GameWindow
    {
        //lab2########
        private int objectX = 50;
        private int objectY = 50;
        private int lastMouseX;
        private int lastMouseY;
        private Vector2 objectPosition = Vector2.Zero;

        //lab3########
        private float rotationAngle = 0.0f;
        private Color4 objectColor = Color4.White;

        public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Control Obiect cu Input mouse") 
        {
            VSync = VSyncMode.On;
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.BlueViolet);

            // Incarcă coordonatele obiectului dintr-un fișier text
            if (File.Exists("object_coordinates.txt"))
            {
                string[] lines = File.ReadAllLines("object_coordinates.txt");
                if (lines.Length == 2)
                {
                    float x = float.Parse(lines[0]);
                    float y = float.Parse(lines[1]);
                    objectPosition = new Vector2(x, y);
                }
            }
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            //lab2########
            /*  Controlarea obiectului folosind mouse-ul
            MouseState mouse = Mouse.GetState();
            objectPosition.X = (mouse.X - Width / 2) / (float)(Width / 2);
            objectPosition.Y = (Height / 2 - mouse.Y) / (float)(Height / 2);
            */

            //lab3########
            // Controlul unghiului camerei folosind mouse-ul
            MouseState mouse = Mouse.GetState();
            rotationAngle = (mouse.X - Width / 2) / (float)Width;

            // Controlarea culorii obiectului folosind tastele
            KeyboardState keyboard = Keyboard.GetState();
            objectColor.R = keyboard[Key.Q] ? 1.0f : 0.0f; // Rosu
            objectColor.G = keyboard[Key.W] ? 1.0f : 0.0f; // Verde
            objectColor.B = keyboard[Key.E] ? 1.0f : 0.0f; // Albastru
            objectColor.A = keyboard[Key.R] ? 1.0f : 0.0f; // Transparenta
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            /*lab2########
            var keyboardState = Keyboard.GetState();
            float speed = 1.0f;
            // Controlul obiectului cu tastatura
            if (keyboardState.IsKeyDown(Key.W))
            {
                objectPosition.Y += speed;
            }
            if (keyboardState.IsKeyDown(Key.S))
            {
                objectPosition.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Key.A))
            {
                objectPosition.X -= speed;
            }
            if (keyboardState.IsKeyDown(Key.D))
            {
                objectPosition.X += speed;
            }
            GL.Begin(PrimitiveType.Quads);
            GL.Color3((System.Drawing.Color)Color4.Blue);
            GL.Vertex2(objectPosition.X, objectPosition.Y);
            GL.Vertex2(objectPosition.X + 50, objectPosition.Y);
            GL.Vertex2(objectPosition.X + 50, objectPosition.Y + 50);
            GL.Vertex2(objectPosition.X, objectPosition.Y + 50);
            */

            //lab3########
            // Aplica rotatia
            GL.Rotate(rotationAngle * 360.0f, 0, 0, 1);

            // Deseneaza triunghiul folosind culoarea setata
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(objectColor);
            GL.Vertex2(-0.1f, -0.1f);
            GL.Vertex2(0.1f, -0.1f);
            GL.Vertex2(0.0f, 0.1f);
            GL.End();

            SwapBuffers();

        }


        [STAThread]
        static void Main()
        {
            using (var game = new Game(800, 600))
            {
                game.Run(60.0);
            }
        }
    }
}
