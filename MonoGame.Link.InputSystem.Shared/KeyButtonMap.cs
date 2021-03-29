// Danilo Borges Santos, 2020.

using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Microsoft.Xna.Framework.InputSystem
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
                            Keys? rightstick, Keys? rightthumbstickup, Keys? rightthumbstickdown, Keys? rightthumbstickright, Keys? rightthumbstickleft,
                            Keys? leftstick, Keys? leftthumbstickup, Keys? leftthumbstickdown, Keys? leftthumbstickright, Keys? leftthumbstickleft,
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
        public void SetDPad(Keys? dpadup, Keys? dpaddown, Keys? dpadright, Keys? dpadleft)
        {
            DPadUp = dpadup;
            DPadDown = dpaddown;
            DPadRight = dpadright;
            DPadLeft = dpadleft;
        }

        /// <summary>
        /// Define o mapa dos botões X, Y, A e B.
        /// </summary>
        /// <param name="x">Botão X do GamePad.</param>
        /// <param name="y">Botão Y do GamePad.</param>
        /// <param name="a">Botão A do GamePad.</param>
        /// <param name="b">Botão B do GamePad.</param>
        public void SetAXBY(Keys? x, Keys? y, Keys? a, Keys? b)
        {
            X = x;
            Y = y;
            A = a;
            B = b;
        }

        /// <summary>
        /// Define o mapa dos gatilhos [RT] e [LT].
        /// </summary>
        /// <param name="lefttrigger">Gatilho esquerdo [LT].</param>
        /// <param name="righttrigger">Gatilho direito [RT].</param>
        public void SetTrigger(Keys? lefttrigger, Keys? righttrigger)
        {
            LeftTrigger = lefttrigger;
            RightTrigger = righttrigger;
        }

        /// <summary>
        /// Define o mapa dos botões de ombro [RB] e [LB]
        /// </summary>
        /// <param name="leftshouder">Botão de ombro esquerdo. [LB].</param>
        /// <param name="rightshoulder">Botão de ombro direito [RB].</param>
        public void SetShoulder(Keys? leftshouder, Keys? rightshoulder)
        {
            LeftShoulder = leftshouder;
            RightShoulder = rightshoulder;
        }

        /// <summary>
        /// Define o mapa do direcional analógico direito.
        /// </summary>
        /// <param name="rightstick">Analógico direito.</param>
        /// <param name="rightthumbstickup">Analógico direito para cima.</param>
        /// <param name="rightthumbstickdown">Analógico direito para baixo.</param>
        /// <param name="rightthumbstickright">Analógico direito para direita.</param>
        /// <param name="rightthumbstickleft">Analógico direito para esquerda.</param>
        public void SetRightStick(Keys? rightstick, Keys? rightthumbstickup, Keys? rightthumbstickdown, Keys? rightthumbstickright, Keys? rightthumbstickleft)
        {
            RightStick = rightstick;
            RightThumbStickUp = rightthumbstickup;
            RightThumbStickDown = rightthumbstickdown;
            RightThumbStickRight = rightthumbstickright;
            RightThumbStickLeft = rightthumbstickleft;
        }

        /// <summary>
        /// Define o mapa do direcional analógico esquerdo.
        /// </summary>
        /// <param name="leftstick">Analógico esquerdo.</param>
        /// <param name="leftthumbstickup">Analógico esquerdo para cima.</param>
        /// <param name="leftthumbstickdown">Analógico esquerdo para baixo.</param>
        /// <param name="leftthumbstickright">Analógico esquerdo para direita.</param>
        /// <param name="leftthumbstickleft">Analógico esquerdo para esquerda.</param>
        public void SetLeftStick(Keys? leftstick, Keys? leftthumbstickup, Keys? leftthumbstickdown, Keys? leftthumbstickright, Keys? leftthumbstickleft)
        {
            LeftStick = leftstick;
            LeftThumbStickUp = leftthumbstickup;
            LeftThumbStickDown = leftthumbstickdown;
            LeftThumbStickRight = leftthumbstickright;
            LeftThumbStickLeft = leftthumbstickleft;
        }

        /// <summary>
        /// Define o mapa dos botões START, BACK e BIGBUTTON.
        /// </summary>
        /// <param name="start">Botão Start.</param>
        /// <param name="back">Botão Back.</param>
        /// <param name="bigbutton">Botão BigButton.</param>
        public void SetStartBackBig(Keys? start, Keys? back, Keys? bigbutton)
        {
            Start = start;
            Back = back;
            BigButton = bigbutton;
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
        /// Salva o mapeamento em arquivo no formato XML. Retorna True caso bem sucedido.
        /// </summary>
        /// <param name="path">O caminho com o nome do arquivo (.xml) a ser salvo.</param>
        public bool Save(string path)
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

                    writer.WriteElementString("DPadUp", GetString(DPadUp));
                    writer.WriteElementString("DPadDown", GetString(DPadDown));
                    writer.WriteElementString("DPadRight", GetString(DPadRight));
                    writer.WriteElementString("DPadLeft", GetString(DPadLeft));

                    writer.WriteElementString("X", GetString(X));
                    writer.WriteElementString("Y", GetString(Y));
                    writer.WriteElementString("A", GetString(A));
                    writer.WriteElementString("B", GetString(B));

                    writer.WriteElementString("LeftTrigger", GetString(LeftTrigger));
                    writer.WriteElementString("RightTrigger", GetString(RightTrigger));
                    writer.WriteElementString("LeftShoulder", GetString(LeftShoulder));
                    writer.WriteElementString("RightShoulder", GetString(RightShoulder));

                    writer.WriteElementString("RightStick", GetString(RightStick));
                    writer.WriteElementString("RightThumbStickUp", GetString(RightThumbStickUp));
                    writer.WriteElementString("RightThumbStickDown", GetString(RightThumbStickDown));
                    writer.WriteElementString("RightThumbStickRight", GetString(RightThumbStickRight));
                    writer.WriteElementString("RightThumbStickLeft", GetString(RightThumbStickLeft));

                    writer.WriteElementString("LeftStick", GetString(LeftStick));
                    writer.WriteElementString("LeftThumbStickUp", GetString(LeftThumbStickUp));
                    writer.WriteElementString("LeftThumbStickDown", GetString(RightThumbStickDown));
                    writer.WriteElementString("LeftThumbStickRight", GetString(LeftThumbStickRight));
                    writer.WriteElementString("LeftThumbStickLeft", GetString(LeftThumbStickLeft));

                    writer.WriteElementString("Start", GetString(Start));
                    writer.WriteElementString("Back", GetString(Back));
                    writer.WriteElementString("BigButton", GetString(BigButton));

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();                    
                }
                
                return true;
            }
            catch
            {
                return false;
            }            
        }

        private string GetString(Keys? key)
        {
            string s = key.HasValue ? ((int)key).ToString() : "null";
            return s;
        }

        private Keys? GetKey(string s)
        {
            Keys? k = null;

            if (!s.Equals("null"))
            {
                int i = int.Parse(s);
                k = (Keys)i;
            }

            return k;
        }

        /// <summary>
        /// Carrega um arquivo XML com o mapeamento salvo pelo método Save().
        /// Retorna True caso bem sucedido.
        /// </summary>
        /// <param name="path">O caminho do arquivo (.xml).</param>
        public bool Load(string path)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);

                XmlNode dpup = document.SelectSingleNode("//DPadUp");
                XmlNode dpdw = document.SelectSingleNode("//DPadDown");
                XmlNode dpdr = document.SelectSingleNode("//DPadRight");
                XmlNode dpdl = document.SelectSingleNode("//DPadLeft");
                XmlNode x = document.SelectSingleNode("//X");
                XmlNode y = document.SelectSingleNode("//Y");
                XmlNode a = document.SelectSingleNode("//A");
                XmlNode b = document.SelectSingleNode("//B");
                XmlNode lt = document.SelectSingleNode("//LeftTrigger");
                XmlNode rt = document.SelectSingleNode("//RightTrigger");
                XmlNode ls = document.SelectSingleNode("//LeftShoulder");
                XmlNode rs = document.SelectSingleNode("//RightShoulder");
                XmlNode rts = document.SelectSingleNode("//RightStick");
                XmlNode rtsu = document.SelectSingleNode("//RightThumbStickUp");
                XmlNode rtsd = document.SelectSingleNode("//RightThumbStickDown");
                XmlNode rtsr = document.SelectSingleNode("//RightThumbStickRight");
                XmlNode rtsl = document.SelectSingleNode("//RightThumbStickLeft");
                XmlNode lts = document.SelectSingleNode("//LeftStick");
                XmlNode ltsu = document.SelectSingleNode("//LeftThumbStickUp");
                XmlNode ltsd = document.SelectSingleNode("//LeftThumbStickDown");
                XmlNode ltsr = document.SelectSingleNode("//LeftThumbStickRight");
                XmlNode ltsl = document.SelectSingleNode("//LeftThumbStickLeft");
                XmlNode st = document.SelectSingleNode("//Start");
                XmlNode bk = document.SelectSingleNode("//Back");
                XmlNode bb = document.SelectSingleNode("//BigButton");

                DPadUp = GetKey(dpup.InnerText);
                DPadDown = GetKey(dpdw.InnerText);
                DPadRight = GetKey(dpdr.InnerText);
                DPadLeft = GetKey(dpdl.InnerText);
                X = GetKey(x.InnerText);
                Y = GetKey(y.InnerText);
                A = GetKey(a.InnerText);
                B = GetKey(b.InnerText);
                LeftTrigger = GetKey(lt.InnerText);
                RightTrigger = GetKey(rt.InnerText);
                LeftShoulder = GetKey(ls.InnerText);
                RightShoulder = GetKey(rs.InnerText);
                RightStick = GetKey(rts.InnerText);
                RightThumbStickUp = GetKey(rtsu.InnerText);
                RightThumbStickDown = GetKey(rtsd.InnerText);
                RightThumbStickRight = GetKey(rtsr.InnerText);
                RightThumbStickLeft = GetKey(rtsl.InnerText);
                LeftStick = GetKey(lts.InnerText);
                LeftThumbStickUp = GetKey(ltsu.InnerText);
                LeftThumbStickDown = GetKey(ltsd.InnerText);
                LeftThumbStickRight = GetKey(ltsr.InnerText);
                LeftThumbStickLeft = GetKey(ltsl.InnerText);
                Start = GetKey(st.InnerText);
                Back = GetKey(bk.InnerText);
                BigButton = GetKey(bb.InnerText);

                return true;
            }
            catch
            {
                return false;
            }            
        }
    }
}