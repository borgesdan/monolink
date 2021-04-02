// Danilo Borges Santos, 25/03/2021.

using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Xml;
using System;

namespace MonoLink.Input
{
    /// <summary>Define um mapa do teclado em referência aos botões do GamePad.</summary>
    public class KeyButtonMap
    {
        public Keys? DPadUp { get; set; } = null;
        public Keys? DPadDown { get; set; } = null;
        public Keys? DPadRight { get; set; } = null;
        public Keys? DPadLeft { get; set; } = null;

        public Keys? X { get; set; } = null;
        public Keys? Y { get; set; } = null;
        public Keys? A { get; set; } = null;
        public Keys? B { get; set; } = null;

        public Keys? LeftTrigger { get; set; } = null;
        public Keys? RightTrigger { get; set; } = null;

        public Keys? LeftShoulder { get; set; } = null;
        public Keys? RightShoulder { get; set; } = null;

        public Keys? RightStick { get; set; } = null;
        public Keys? RightThumbStickUp { get; set; } = null;
        public Keys? RightThumbStickDown { get; set; } = null;
        public Keys? RightThumbStickRight { get; set; } = null;
        public Keys? RightThumbStickLeft { get; set; } = null;

        public Keys? LeftStick { get; set; } = null;
        public Keys? LeftThumbStickUp { get; set; } = null;
        public Keys? LeftThumbStickDown { get; set; } = null;
        public Keys? LeftThumbStickRight { get; set; } = null;
        public Keys? LeftThumbStickLeft { get; set; } = null;

        public Keys? Start { get; set; } = null;
        public Keys? Back { get; set; } = null;

        public Keys? BigButton { get; set; } = null;

        /// <summary>Inicializa uma nova instância da classe.</summary>
        public KeyButtonMap() { }

        /// <summary>
        /// Inicializa uma nova instância da classe definindo o mapa do tecla-botão. Valores podem ser nulos.
        /// </summary>
        /// <param name="dpadup">Direcional digital para cima.</param>
        /// <param name="dpaddown">Direcional digital para baixo.</param>
        /// <param name="dpadright">Direcional digital para direita.</param>
        /// <param name="dpadleft">Direcional digital para esquerda.</param>
        /// <param name="x">Botão X do GamePad.</param>
        /// <param name="y">Botão Y do GamePad.</param>
        /// <param name="a">Botão A do GamePad.</param>
        /// <param name="b">Botão B do GamePad.</param>
        /// <param name="lefttrigger">Gatilho esquerdo [LT].</param>
        /// <param name="righttrigger">Gatilho direito [RT].</param>
        /// <param name="leftshouder">Botão de ombro esquerdo. [LB].</param>
        /// <param name="rightshoulder">Botão de ombro direito [RB].</param>
        /// <param name="rightstick">Analógico direito.</param>
        /// <param name="rightthumbstickup">Analógico direito para cima.</param>
        /// <param name="rightthumbstickdown">Analógico direito para baixo.</param>
        /// <param name="rightthumbstickright">Analógico direito para direita.</param>
        /// <param name="rightthumbstickleft">Analógico direito para esquerda.</param>
        /// <param name="leftstick">Analógico esquerdo.</param>
        /// <param name="leftthumbstickup">Analógico esquerdo para cima.</param>
        /// <param name="leftthumbstickdown">Analógico esquerdo para baixo.</param>
        /// <param name="leftthumbstickright">Analógico esquerdo para direita.</param>
        /// <param name="leftthumbstickleft">Analógico esquerdo para esquerda.</param>
        /// <param name="start">Botão Start.</param>
        /// <param name="back">Botão Back.</param>
        /// <param name="bigbutton">Botão BigButton.</param>
        public KeyButtonMap(Keys? dpadup, Keys? dpaddown, Keys? dpadright, Keys? dpadleft,
                            Keys? x, Keys? y, Keys? a, Keys? b,
                            Keys? lefttrigger, Keys? righttrigger, Keys? leftshouder, Keys? rightshoulder,
                            Keys? rightstick, Keys? rightthumbstickup, Keys? rightthumbstickdown,
                            Keys? rightthumbstickright, Keys? rightthumbstickleft,
                            Keys? leftstick, Keys? leftthumbstickup, Keys? leftthumbstickdown,
                            Keys? leftthumbstickright, Keys? leftthumbstickleft,
                            Keys? start, Keys? back, Keys? bigbutton)
        {
            DPadUp = dpadup;
            DPadDown = dpaddown;
            DPadRight = dpadright;
            DPadLeft = dpadleft;

            X = x;
            Y = y;
            A = a;
            B = b;

            LeftTrigger = lefttrigger;
            RightTrigger = righttrigger;
            LeftShoulder = leftshouder;
            RightShoulder = rightshoulder;

            RightStick = rightstick;
            RightThumbStickUp = rightthumbstickup;
            RightThumbStickDown = rightthumbstickdown;
            RightThumbStickRight = rightthumbstickright;
            RightThumbStickLeft = rightthumbstickleft;

            LeftStick = leftstick;
            LeftThumbStickUp = leftthumbstickup;
            LeftThumbStickDown = leftthumbstickdown;
            LeftThumbStickRight = leftthumbstickright;
            LeftThumbStickLeft = leftthumbstickleft;

            Start = start;
            Back = back;
            BigButton = bigbutton;            
        }

        /// <summary>
        /// Define o mapa dos direcionais digitais.
        /// </summary>
        /// <param name="dpadup">Direcional digital para cima.</param>
        /// <param name="dpaddown">Direcional digital para baixo.</param>
        /// <param name="dpadright">Direcional digital para direita.</param>
        /// <param name="dpadleft">Direcional digital para esquerda.</param>
        public KeyButtonMap SetDPad(Keys? dpadup, Keys? dpaddown, Keys? dpadright, Keys? dpadleft)
        {
            DPadUp = dpadup;
            DPadDown = dpaddown;
            DPadRight = dpadright;
            DPadLeft = dpadleft;

            return this;
        }

        /// <summary>
        /// Define o mapa dos botões X, Y, A e B.
        /// </summary>
        /// <param name="x">Botão X do GamePad.</param>
        /// <param name="y">Botão Y do GamePad.</param>
        /// <param name="a">Botão A do GamePad.</param>
        /// <param name="b">Botão B do GamePad.</param>
        public KeyButtonMap SetAXBY(Keys? x, Keys? y, Keys? a, Keys? b)
        {
            X = x;
            Y = y;
            A = a;
            B = b;

            return this;
        }

        /// <summary>
        /// Define o mapa dos gatilhos [RT] e [LT].
        /// </summary>
        /// <param name="lefttrigger">Gatilho esquerdo [LT].</param>
        /// <param name="righttrigger">Gatilho direito [RT].</param>
        public KeyButtonMap SetTrigger(Keys? lefttrigger, Keys? righttrigger)
        {
            LeftTrigger = lefttrigger;
            RightTrigger = righttrigger;

            return this;
        }

        /// <summary>
        /// Define o mapa dos botões de ombro [RB] e [LB]
        /// </summary>
        /// <param name="leftshouder">Botão de ombro esquerdo. [LB].</param>
        /// <param name="rightshoulder">Botão de ombro direito [RB].</param>
        public KeyButtonMap SetShoulder(Keys? leftshouder, Keys? rightshoulder)
        {
            LeftShoulder = leftshouder;
            RightShoulder = rightshoulder;

            return this;
        }

        /// <summary>
        /// Define o mapa do direcional analógico direito.
        /// </summary>
        /// <param name="rightstick">Analógico direito.</param>
        /// <param name="rightthumbstickup">Analógico direito para cima.</param>
        /// <param name="rightthumbstickdown">Analógico direito para baixo.</param>
        /// <param name="rightthumbstickright">Analógico direito para direita.</param>
        /// <param name="rightthumbstickleft">Analógico direito para esquerda.</param>
        public KeyButtonMap SetRightStick(Keys? rightstick, Keys? rightthumbstickup,
            Keys? rightthumbstickdown, Keys? rightthumbstickright, Keys? rightthumbstickleft)
        {
            RightStick = rightstick;
            RightThumbStickUp = rightthumbstickup;
            RightThumbStickDown = rightthumbstickdown;
            RightThumbStickRight = rightthumbstickright;
            RightThumbStickLeft = rightthumbstickleft;

            return this;
        }

        /// <summary>
        /// Define o mapa do direcional analógico esquerdo.
        /// </summary>
        /// <param name="leftstick">Analógico esquerdo.</param>
        /// <param name="leftthumbstickup">Analógico esquerdo para cima.</param>
        /// <param name="leftthumbstickdown">Analógico esquerdo para baixo.</param>
        /// <param name="leftthumbstickright">Analógico esquerdo para direita.</param>
        /// <param name="leftthumbstickleft">Analógico esquerdo para esquerda.</param>
        public KeyButtonMap SetLeftStick(Keys? leftstick, Keys? leftthumbstickup,
            Keys? leftthumbstickdown, Keys? leftthumbstickright, Keys? leftthumbstickleft)
        {
            LeftStick = leftstick;
            LeftThumbStickUp = leftthumbstickup;
            LeftThumbStickDown = leftthumbstickdown;
            LeftThumbStickRight = leftthumbstickright;
            LeftThumbStickLeft = leftthumbstickleft;

            return this;
        }

        /// <summary>
        /// Define o mapa dos botões START, BACK e BIGBUTTON.
        /// </summary>
        /// <param name="start">Botão Start.</param>
        /// <param name="back">Botão Back.</param>
        /// <param name="bigbutton">Botão BigButton.</param>
        public KeyButtonMap SetStartBackBig(Keys? start, Keys? back, Keys? bigbutton)
        {
            Start = start;
            Back = back;
            BigButton = bigbutton;

            return this;
        }

        /// <summary>Obtém um dicionário com o mapa do teclado em referência ao GamePad.</summary>
        public Dictionary<Buttons, Keys?> GetKeyboardMap()
        {
            Dictionary<Buttons, Keys?> dictionary = new Dictionary<Buttons, Keys?>
            {
                { Buttons.A, A },
                { Buttons.B, B },
                { Buttons.X, X },
                { Buttons.Y, Y },

                { Buttons.LeftShoulder, LeftShoulder },
                { Buttons.RightShoulder, RightShoulder },
                { Buttons.LeftTrigger, LeftTrigger },
                { Buttons.RightTrigger, RightTrigger },
                { Buttons.LeftStick, LeftStick },
                { Buttons.RightStick, RightStick },

                { Buttons.Back, Back },
                { Buttons.Start, Start },
                { Buttons.BigButton, BigButton },

                { Buttons.DPadDown, DPadDown },
                { Buttons.DPadUp, DPadUp },
                { Buttons.DPadLeft, DPadLeft },
                { Buttons.DPadRight, DPadRight },

                { Buttons.LeftThumbstickDown, LeftThumbStickDown },
                { Buttons.LeftThumbstickLeft, LeftThumbStickLeft },
                { Buttons.LeftThumbstickRight, LeftThumbStickRight },
                { Buttons.LeftThumbstickUp, LeftThumbStickUp },

                { Buttons.RightThumbstickDown, RightThumbStickDown },
                { Buttons.RightThumbstickLeft, RightThumbStickLeft },
                { Buttons.RightThumbstickRight, RightThumbStickRight },
                { Buttons.RightThumbstickUp, RightThumbStickUp }
            };

            return dictionary;
        }

        /// <summary>
        /// Salva o mapeamento em arquivo no formato XML.
        /// </summary>
        /// <param name="path">
        /// O caminho com o nome do arquivo (.xml) a ser salvo, por exemplo "c:/file.xml".
        /// Pode-se usar também System.IO.Path.Combine(Content.Root + nomedoarquivo.xml) para salvar o arquivo
        /// na pasta Content.
        /// </param>
        public void Save(string path)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(path, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("asset");
                    writer.WriteStartElement("input");

                    WriteElement(writer, "dpadup", DPadUp);
                    WriteElement(writer, "dpaddown", DPadDown);
                    WriteElement(writer, "dpadright", DPadRight);
                    WriteElement(writer, "dpadleft", DPadLeft);

                    WriteElement(writer, "x", X);
                    WriteElement(writer, "y", Y);
                    WriteElement(writer, "a", A);
                    WriteElement(writer, "b", B);

                    WriteElement(writer, "lefttrigger", LeftTrigger);
                    WriteElement(writer, "righttrigger", RightTrigger);
                    WriteElement(writer, "leftshoulder", LeftShoulder);
                    WriteElement(writer, "rightshoulder", RightShoulder);

                    WriteElement(writer, "rightstick", RightStick);
                    WriteElement(writer, "rightthumbstickup", RightThumbStickUp);
                    WriteElement(writer, "rightthumbstickdown", RightThumbStickDown);
                    WriteElement(writer, "rightthumbstickright", RightThumbStickRight);
                    WriteElement(writer, "rightthumbstickleft", RightThumbStickLeft);

                    WriteElement(writer, "leftstick", LeftStick);
                    WriteElement(writer, "leftthumbstickup", LeftThumbStickUp);
                    WriteElement(writer, "leftthumbstickdown", LeftThumbStickDown);
                    WriteElement(writer, "leftthumbstickright", LeftThumbStickRight);
                    WriteElement(writer, "leftthumbstickleft", LeftThumbStickLeft);

                    WriteElement(writer, "start", Start);
                    WriteElement(writer, "back", Back);
                    WriteElement(writer, "bigbutton", BigButton);

                    writer.WriteEndElement(); //input
                    writer.WriteEndElement(); //asset
                    writer.WriteEndDocument();            
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        /// <summary>
        /// Carrega um arquivo XML com o mapeamento salvo pelo método Save().
        /// </summary>
        /// <param name="path">O caminho do arquivo (.xml).</param>
        public void Load(string path)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);

                DPadUp = ReadElement(document, "dpadup");
                DPadDown = ReadElement(document, "dpaddown");
                DPadRight = ReadElement(document, "dpadright");
                DPadLeft = ReadElement(document, "dpadleft");
                X = ReadElement(document, "x");
                Y = ReadElement(document, "y");
                A = ReadElement(document, "a");
                B = ReadElement(document, "b");
                LeftTrigger = ReadElement(document, "lefttrigger");
                RightTrigger = ReadElement(document, "righttrigger");
                LeftShoulder = ReadElement(document, "leftshoulder");
                RightShoulder = ReadElement(document, "rightshoulder");
                RightStick = ReadElement(document, "rightstick");
                RightThumbStickUp = ReadElement(document, "rightthumbstickup");
                RightThumbStickDown = ReadElement(document, "rightthumbstickdown");
                RightThumbStickRight = ReadElement(document, "rightthumbstickright");
                RightThumbStickLeft = ReadElement(document, "rightthumbstickleft");
                LeftStick = ReadElement(document, "leftstick");
                LeftThumbStickUp = ReadElement(document, "leftthumbstickup");
                LeftThumbStickDown = ReadElement(document, "leftthumbstickdown");
                LeftThumbStickRight = ReadElement(document, "leftthumbstickright");
                LeftThumbStickLeft = ReadElement(document, "leftthumbstickleft");
                Start = ReadElement(document, "start");
                Back = ReadElement(document, "back");
                BigButton = ReadElement(document, "bigbutton");                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Escreve um elemento no xml.
        private void WriteElement(XmlWriter writer, string element, Keys? key)
        {
            writer.WriteStartElement(element);
            writer.WriteAttributeString("key", key.ToString());
            writer.WriteValue(key.HasValue ? ((int)key).ToString() : "");
            writer.WriteEndElement();
        }

        //obtém um elemento xml convertido no formato Keys?.
        private Keys? ReadElement(XmlDocument document, string node)
        {
            XmlNode dpup = document.SelectSingleNode($"//{node}");

            if (dpup.InnerText != string.Empty)
            {
                int i = int.Parse(dpup.InnerText);
                return (Keys)i;
            }

            return null;
        }
    }
}