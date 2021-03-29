using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.Common
{
    /// <summary>
    /// Fornece acesso as manipulações de transformação (posição, escala e rotação) de um objeto.
    /// </summary>
    public sealed class Transform
    {
        //------------- CLASSES ----------------//

        /// <summary>
        /// Representa as transformações em uma dimensão 3D.
        /// </summary>
        public sealed class Transform3D
        {
            readonly Transform transform;

            /// <summary>
            /// Obtém ou define a posição através de um Vector3.
            /// </summary>
            public Vector3 Position { get => transform.Position; set => transform.Position = value; }
            /// <summary>Obtém ou define a posição no eixo X.</summary>
            public float X { get => transform.X; set => transform.X = value; }
            /// <summary>Obtém ou define a posição no eixo Y.</summary>
            public float Y { get => transform.Y; set => transform.Y = value; }
            /// <summary>Obtém ou define a posição no eixo Z.</summary>
            public float Z { get => transform.Z; set => transform.Z = value; }

            /// <summary>
            /// Obtém ou define a velocidade através de um Vector3.
            /// </summary>
            public Vector3 Velocity { get => transform.Velocity; set => transform.Velocity = value; }
            /// <summary>Obtém ou define a velocidade no eixo X.</summary>
            public float Vx { get => transform.Vx; set => transform.Vx = value; }
            /// <summary>Obtém ou define a velocidade no eixo Y.</summary>
            public float Vy { get => transform.Vy; set => transform.Vy = value; }
            /// <summary>Obtém ou define a velocidade no eixo Z.</summary>
            public float Vz { get => transform.Vz; set => transform.Vz = value; }

            /// <summary>
            /// Obtém ou define a escala através de um Vector3.
            /// </summary>
            public Vector3 Scale { get => transform.Scale; set => transform.Scale = value; }
            /// <summary>Obtém ou define a escala no eixo X.</summary>
            public float Sx { get => transform.Sx; set => transform.Sx = value; }
            /// <summary>Obtém ou define a escala no eixo Y.</summary>
            public float Sy { get => transform.Sy; set => transform.Sy = value; }
            /// <summary>Obtém ou define a escala no eixo Z.</summary>
            public float Sz { get => transform.Sz; set => transform.Sz = value; }

            /// <summary>
            /// Obtém ou define a rotação através de um Vector3.
            /// </summary>
            public Vector3 Rotation { get => transform.Rotation; set => transform.Rotation = value; }
            /// <summary>Obtém ou define a rotação no eixo X.</summary>
            public float Rx { get => transform.Rx; set => transform.Rx = value; }
            /// <summary>Obtém ou define a rotação no eixo Y.</summary>
            public float Ry { get => transform.Ry; set => transform.Ry = value; }
            /// <summary>Obtém ou define a rotação no eixo Z.</summary>
            public float Rz { get => transform.Rz; set => transform.Rz = value; }

            internal Transform3D(Transform t)
            {
                transform = t;
            }

            /// <summary>
            /// Obtém a matriz de transformação: escala * rotação * transformação.
            /// </summary>
            public Matrix GetMatrix() => GetMatrix();
        }

        /// <summary>
        /// Representa as transformações em uma dimensão 2D.
        /// </summary>
        public sealed class Transform2D
        {
            readonly Transform transform;

            /// <summary>
            /// Obtém ou define a posição através de um Vector2.
            /// </summary>
            public Vector2 Position { get => transform.Position2; set => transform.Position2 = value; }
            /// <summary>Obtém ou define a posição no eixo X.</summary>
            public float X { get => transform.X; set => transform.X = value; }
            /// <summary>Obtém ou define a posição no eixo Y.</summary>
            public float Y { get => transform.Y; set => transform.Y = value; }

            /// <summary>
            /// Obtém ou define a velocidade através de um Vector2.
            /// </summary>
            public Vector2 Velocity { get => transform.Velocity2; set => transform.Velocity2 = value; }
            /// <summary>Obtém ou define a velocidade no eixo X.</summary>
            public float Vx { get => transform.Vx; set => transform.Vx = value; }
            /// <summary>Obtém ou define a velocidade no eixo Y.</summary>
            public float Vy { get => transform.Vy; set => transform.Vy = value; }

            /// <summary>
            /// Obtém ou define a escala através de um Vector2.
            /// </summary>
            public Vector2 Scale { get => transform.Scale2; set => transform.Scale2 = value; }
            /// <summary>Obtém ou define a escala no eixo X.</summary>
            public float Sx { get => transform.Sx; set => transform.Sx = value; }
            /// <summary>Obtém ou define a escala no eixo Y.</summary>
            public float Sy { get => transform.Sy; set => transform.Sy = value; }
            
            /// <summary>Obtém ou define a rotação no eixo Z.</summary>
            public float Rotation { get => transform.Rz; set => transform.Rz = value; }

            internal Transform2D(Transform t)
            {
                transform = t;
            }

            /// <summary>
            /// Obtém a matriz de transformação: escala * rotação * transformação.
            /// </summary>
            public Matrix GetMatrix() => transform.GetMatrix2();

            /// <summary>
            /// Obtém a matriz de transformação: -origin * escala * rotação * transformação.
            /// </summary>
            /// <param name="origin">Define o ponto de origem a ser inserido no cálculo da matriz.</param>
            public Matrix GetMatrix(Vector2 origin) => transform.GetMatrix2(origin);
        }

        //------------- VARIABLES ---------------//

        Vector3 position = Vector3.Zero;
        Vector3 scale = Vector3.One;
        Vector3 rotation = Vector3.Zero;
        Vector3 velocity = Vector3.Zero;

        //------------- PROPERTIES ---------------//    

        /// <summary>Obtém ou define a posição nos eixos X, Y e Z.</summary>
        public Vector3 Position { get => position; set => position = value; }
        /// <summary>Obtém ou define a posição no eixo X.</summary>
        public float X { get { return Position.X; } set { Position = new Vector3(value, Y, Z); } }
        /// <summary>Obtém ou define a posição no eixo Y.</summary>
        public float Y { get { return Position.Y; } set { Position = new Vector3(X, value, Z); } }
        /// <summary>Obtém ou define a posição no eixo Z.</summary>
        public float Z { get { return Position.Z; } set { Position = new Vector3(X, Y, value); } }
        /// <summary>Obtém ou define a posição através de um Vector2 ignorando o eixo Z.</summary>
        public Vector2 Position2 { get => new Vector2(position.X, position.Y); set => position = new Vector3(value.X, value.Y, position.Z); }

        /// <summary>Obtém ou define a velocidade nos eixos X, Y e Z.</summary>
        public Vector3 Velocity { get => velocity; set => velocity = value; }
        /// <summary>Obtém ou define a velocidade no eixo X.</summary>
        public float Vx { get { return Velocity.X; } set { Velocity = new Vector3(value, Vy, Vz); } }
        /// <summary>Obtém ou define a velocidade no eixo Y.</summary>
        public float Vy { get { return Velocity.Y; } set { Velocity = new Vector3(Vx, value, Vz); } }
        /// <summary>Obtém ou define a velocidade no eixo Z.</summary>
        public float Vz { get { return Velocity.Z; } set { Velocity = new Vector3(Vx, Vy, value); } }
        /// <summary>Obtém ou define a velocidade através de um Vector2 ignorando o eixo Z.</summary>
        public Vector2 Velocity2 { get => new Vector2(velocity.X, velocity.Y); set => velocity = new Vector3(value.X, value.Y, velocity.Z); }

        /// <summary>Obtém ou define a escala nos eixos X, Y e Z.</summary>
        public Vector3 Scale { get => scale; set => scale = value; }
        /// <summary>Obtém ou define a escala no eixo X.</summary>
        public float Sx { get { return Scale.X; } set { Scale = new Vector3(value, Sy, Sz); } }
        /// <summary>Obtém ou define a escala no eixo Y.</summary>
        public float Sy { get { return Scale.Y; } set { Scale = new Vector3(Sx, value, Sz); } }
        /// <summary>Obtém ou define a escala no eixo Z.</summary>
        public float Sz { get { return Scale.Z; } set { Scale = new Vector3(Sx, Sy, value); } }
        /// <summary>Obtém ou define a escala através de um Vector2 ignorando o eixo Z.</summary>
        public Vector2 Scale2 { get => new Vector2(Scale.X, Scale.Y); set => scale = new Vector3(value.X, value.Y, scale.Z); }

        /// <summary>Obtém ou define a rotação nos eixos X, Y e Z.</summary>
        public Vector3 Rotation { get => rotation; set => rotation = value; }
        /// <summary>Obtém ou define a rotação no eixo X.</summary>
        public float Rx { get { return Rotation.X; } set { Rotation = new Vector3(value, Ry, Rz); } }
        /// <summary>Obtém ou define a rotação no eixo Y.</summary>
        public float Ry { get { return Rotation.Y; } set { Rotation = new Vector3(Rx, value, Rz); } }
        /// <summary>Obtém ou define a rotação no eixo Z.</summary>
        public float Rz { get { return Rotation.Z; } set { Rotation = new Vector3(Rx, Ry, value); } }
        /// <summary>Obtém ou define a rotação em um plano 2D (obtém ou define o valor de Rz)</summary>
        public float Rotation2 { get => Rz; set => Rz = value; }
                
        /// <summary>
        /// Obtém ou define as transformações em uma dimensão 3D.
        /// </summary>
        public Transform3D D3 { get; private set; }
        /// <summary>
        /// Obtém ou define as transformações em uma dimensão 2D.
        /// </summary>
        public Transform2D D2 { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public Transform() 
        {
            D3 = new Transform3D(this);
            D2 = new Transform2D(this);
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="position">Informa o valor da posição.</param>
        /// <param name="velocity">Informa o valor da velocidade.</param>
        /// <param name="scale">Informa o valor da escala.</param>
        /// <param name="rotation">Informa o valor da rotação. Para um jogo 2D somente o valor no eixo Z será considerado.</param>
        public Transform(Vector3 position, Vector3 velocity, Vector3 scale, Vector3 rotation)
        {
            this.position = position;
            this.velocity = velocity;
            this.scale = scale;
            this.rotation = rotation;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância
        /// </summary>
        /// <param name="source">A instância a ser copiada</param>
        public Transform(Transform source)
        {
            this.Position = source.Position;
            this.Velocity = source.Velocity;
            this.Scale = source.Scale;
            this.Rotation = source.Rotation;
            this.D3 = new Transform3D(this);
            this.D2 = new Transform2D(this);
        }

        /// <summary>
        /// Copia as transformações de um objeto Transform.
        /// </summary>
        /// <param name="transform"></param>
        public void Set(Transform transform)
        {
            this.Position = transform.Position;
            this.Velocity = transform.Velocity;
            this.Scale = transform.Scale;
            this.Rotation = transform.Rotation;
        }

        /// <summary>
        /// Aplica os cálculos de transformações.
        /// </summary>
        /// <param name="gameTime">O tempo decorrido de jogo.</param>
        public void Update(GameTime gameTime)
        {
            Position += Velocity;
        }

        /// <summary>
        /// Obtém a matriz de transformação: escala * rotação * transformação.
        /// </summary>
        public Matrix GetMatrix()
        {
            return Matrix.CreateScale(Scale)
                * Matrix.CreateRotationX(Rx)
                * Matrix.CreateRotationY(Ry)
                * Matrix.CreateRotationZ(Rz)
                * Matrix.CreateTranslation(Position);
        }

        /// <summary>
        /// Obtém a matriz de transformação: escala * rotação * transformação, excluindo o eixo Z.
        /// </summary>
        public Matrix GetMatrix2() => GetMatrix2(Vector2.Zero);

        /// <summary>
        /// Obtém a matriz de transformação: -origin * escala * rotação * transformação, excluindo o eixo Z.
        /// </summary>
        /// <param name="origin">Define um ponto de origem a ser inserido no cálculo da matriz.</param>
        public Matrix GetMatrix2(Vector2 origin)
        {
            return Matrix.CreateTranslation(new Vector3(-origin.X, -origin.Y, 0))
                 * Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 1))
                 * Matrix.CreateRotationZ(Rotation2)
                 * Matrix.CreateTranslation(new Vector3(X, Y, 0));
        }

        public override string ToString()
        {
            return string.Concat("Pos: ", Position, " Sca: ", Scale, " Rot: ", Rotation);
        }
    }
}