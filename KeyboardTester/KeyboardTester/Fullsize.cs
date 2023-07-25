/**
 * Keyboard Validator: Full-Size (100%) Format
 * 
 * Author: Dylan Belyk
**/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KeyboardTester
{
    public partial class Fullsize : Form
    {
        // Dictionary used to hold checkbox values to link to keys
        private Dictionary<Keys, CheckBox> keyDict = new Dictionary<Keys, CheckBox>();
        public Fullsize()
        {
            InitializeComponent();
        }

        private void Fullsize_Load(object sender, EventArgs e)
        {
            // Handles arrow keys focusing on the reset button bug for first input
            this.PreviewKeyDown += Fullsize_OnArrowKeyDown;
            // List of all keys on a standard Full-Size (100%) Keyboard
            List<Keys> keys = new List<Keys>() { Keys.Escape, Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10,
                Keys.F11, Keys.F12, Keys.PrintScreen, Keys.Scroll, Keys.Pause, Keys.Oemtilde, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6,
                Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.OemMinus, Keys.Oemplus, Keys.Back, Keys.Insert, Keys.Home, Keys.PageUp, Keys.Tab, Keys.Q,
                Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J,
                Keys.K, Keys.L, Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.OemOpenBrackets, Keys.OemCloseBrackets, Keys.OemPipe,
                Keys.Delete, Keys.End, Keys.PageDown, Keys.CapsLock, Keys.OemSemicolon, Keys.OemQuotes, Keys.Oemcomma, Keys.OemPeriod,
                Keys.OemQuestion, Keys.LWin, Keys.Space, Keys.RWin, Keys.Apps, Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.NumLock, Keys.Divide,
                Keys.Multiply, Keys.Subtract, Keys.Add, Keys.Decimal, Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4,
                Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9
            };
            // List of CheckBox names from form
            List<CheckBox> checkboxVariables = new List<CheckBox>() { Esc, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, PrintScreen, ScrollLock,
                Pause, Tilde, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Zero, Hyphen, EqualsK, Backspace, Insert, Home, PageUp, Tab, Q, W, E,
                R, T, Y, U, I, O, P, A, S, D, F, G, H, J, K, L, Z, X, C, V, B, N, M, OpenBracket, ClosedBracket, Backslash, Delete, End, PageDown, CapsLock,
                Semicolon, Apostrophe, Comma, Period, Question, Win, Spacebar, ROption, Fn, UArrow, DArrow, LArrow, RArrow, NumLock, NumDiv, NumMul,
                NumSub, NumAdd, NumDel, NumZero, NumOne, NumTwo, NumThree, NumFour, NumFive, NumSix, NumSeven, NumEight, NumNine
            };
            // Iterates through the lists to populate the keyDict
            for (int i = 0; i < checkboxVariables.Count; i++)
            {
                keyDict[keys[i]] = checkboxVariables[i];
            }
        }
        private void KeyDown_Check(object sender, KeyEventArgs e)
        {
            // Handles arrow keys focusing on the reset button bug for every input after the first
            this.PreviewKeyDown += Fullsize_OnArrowKeyDown;
            // Used for formatting string outputs to text box
            string keyString = e.KeyCode.ToString();
            // On KeyDown checks for matching key within the dictionary and changes associated checkbox to checked and blue
            if (keyDict.ContainsKey(e.KeyCode))
            {
                // Making the checkbox associated with the key code checked and blue
                CheckBox associatedCheckbox = keyDict[e.KeyCode];
                associatedCheckbox.Checked = true;
                associatedCheckbox.BackColor = Color.LightSteelBlue;
                // Removes the D from number outputs - D1, D2, D3, etc. but leaves anything else starting with "D"
                string[] excludeStrings = { "Down", "D", "Delete", "Decimal", "Divide" };
                if (keyString.StartsWith("D") && !excludeStrings.Any(k => k == keyString))
                {
                    keyString = keyString.Substring(1);
                }
                else if (keyString.Contains("Num") &&  keyString != "NumLock")
                {
                    char lastDig = keyString[keyString.Length - 1];
                    keyString = lastDig.ToString();
                }
                // Handles formatting for any outputs the start with "Oem"
                switch (keyString)
                {
                    case "Oemtilde":
                        keyString = "~ "; // Tilde
                        break;
                    case "OemOpenBrackets":
                        keyString = "[ "; // Open Bracket
                        break;
                    case "Oem6":
                        keyString = "] "; // Closed Bracket
                        break;
                    case "OemMinus":
                        keyString = "- "; // Subtraction
                        break;
                    case "Oemplus":
                        keyString = "+ "; // Addition
                        break;
                    case "Oem5":
                        keyString = "\\ "; // Backslash
                        break;
                }
                // Outputs to text box once keyString is formatted from previous statements
                OutputBox.Text += keyString + " ";
                // Make a key clicking effect by making a new timer, waiting 750 milliseconds, then changing the checkbox back to default settings
                if (associatedCheckbox.Checked)
                {
                    ClickEffect(associatedCheckbox);
                }
            }
            // Handles BOTH shift keys
            else if (e.Shift)
            {
                // Makes BOTH shift checkboxes checked and changes color
                LShift.Checked = RShift.Checked = true;
                LShift.BackColor = RShift.BackColor = Color.LightSteelBlue;
                // Displaying output in textbox
                OutputBox.Text += "Shift ";
                // When checked, waits 750 milliseconds then changes the checkbox back to default settings
                if (LShift.Checked || RShift.Checked)
                {
                    ClickEffect(LShift, RShift);
                }
            }
            // Handles BOTH control keys
            else if (e.Control)
            {
                // Makes BOTH control checkboxes checked and changes color
                LControl.Checked = RControl.Checked = true;
                LControl.BackColor = RControl.BackColor = Color.LightSteelBlue;
                // Displaying output in textbox
                OutputBox.Text += "Ctrl ";
                // When checked, waits 750 milliseconds then changes the checkbox back to default settings
                if (LControl.Checked || RControl.Checked)
                {
                    ClickEffect(LControl, RControl);
                }
            }
            else if (e.Alt)
            {
                // Makes BOTH alt checkboxes checked and changes color
                LAlt.Checked = RAlt.Checked = true;
                LAlt.BackColor = RAlt.BackColor = Color.LightSteelBlue;
                // Displaying output in textbox
                OutputBox.Text += "Alt ";
                // When checked, waits 750 milliseconds then changes the checkbox back to default settings
                if (LAlt.Checked || RAlt.Checked)
                {
                    ClickEffect(LAlt, RAlt);
                }
            }
            // Handles BOTH enter keys
            else if (e.KeyCode == Keys.Enter)
            {
                // Makes BOTH enter checkboxes checked and changes color
                EnterK.Checked = NumEnter.Checked = true;
                EnterK.BackColor = NumEnter.BackColor = Color.LightSteelBlue;
                // Displaying output in textbox
                OutputBox.Text += "Enter ";

                if (EnterK.Checked || NumEnter.Checked)
                {
                    ClickEffect(EnterK, NumEnter);
                }
            }
        }
        // Method to handle clicking effect which uses the same code multiple times in KeyDown_Check
        private void ClickEffect(params CheckBox[] checkboxes)
        {
            foreach (CheckBox checkbox in checkboxes)
            {
                // Make a key clicking effect by making a new timer, waiting 750 milliseconds then changing the checkbox back to default settings
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 750;
                timer.Tick += (s, evt) =>
                {
                    checkbox.Checked = false;
                    checkbox.BackColor = SystemColors.Control;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }
        // Output textbox reset button
        private void Reset_Click(object sender, EventArgs e)
        {
            // Clears text box
            OutputBox.Clear();
            // Removes focus from button so other keys work
            this.ActiveControl = null;
        }
        // Handles arrow keys focusing on the reset button bug
        private void Fullsize_OnArrowKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right }.Contains(e.KeyCode))
            {
                e.IsInputKey = true;
            }
        }
    }
}
