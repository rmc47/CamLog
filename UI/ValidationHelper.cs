using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Engine
{
    internal static class ValidationHelper
    {
        private static readonly Color c_WarningColor = Color.Yellow;
        private static readonly Color c_NormalColor = SystemColors.Window;

        public static bool ValidateLocatorTextbox(TextBox tb)
        {
            if (tb.TextLength != 6)
                return true;
            try
            {
                Locator l = new Locator(tb.Text);
            }
            catch { return true; }
            return false;
        }

        public static bool ValidateSerialTextbox(TextBox tb)
        {
            int result;
            return !int.TryParse(tb.Text, out result);
        }

        /// <summary>
        /// Returns true if the textbox FAILS to meet the minimum length
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        public static bool ValidateTextbox(TextBox textBox, int minLength)
        {
            return ValidateTextbox(textBox, delegate(TextBox tb)
            {
                return tb.TextLength < minLength;
            });
        }

        /// <summary>
        /// Returns true if the textbox FAILS to meet the criterion
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="failurePredicate"></param>
        /// <returns></returns>
        public static bool ValidateTextbox(TextBox textBox, Predicate<TextBox> failurePredicate)
        {
            if (failurePredicate(textBox))
            {
                textBox.BackColor = c_WarningColor;
                return true;
            }
            else
            {
                if (textBox.ReadOnly)
                    textBox.BackColor = SystemColors.Control;
                else
                    textBox.BackColor = c_NormalColor;
                return false;
            }
        }

    }
}
