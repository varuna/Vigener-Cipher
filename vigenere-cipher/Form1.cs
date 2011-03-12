/*******************************************************************************
* This file is part of Vigener-cipher project.
* Maintained by : Varuna Lekamwasa <vrlekamwasam@gmail.com>
* Authored By : Varuna Lekamwasa <vrlekamwasam@gmail.com>
*
* Vigener-cipher project is free software: you can redistribute it and/or modify
* it under the terms of the GNU Lesser General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* Vigener-cipher prohect  is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU Lesser General Public License for more details.
*
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vigenere_cipher
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// initializes the GUI components
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click event of the encrypt button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string returnText = convertText(textBox1.Text, textBox2.Text, true);
            textBox3.Text = returnText;
        }

        /// <summary>
        /// Click event of the decrypt button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string returnText = convertText(textBox1.Text, textBox2.Text, false);
            textBox3.Text = returnText;
        }

        /// <summary>
        /// Remove the space charater from the input text
        /// </summary>
        /// <param name="text">input text</param>
        /// <param name="key">input key</param>
        /// <param name="expandedKey">key string is exapnded to fit to the length of the text</param>
        /// <param name="spaceLocs">space character locations of the original string</param>
        /// <returns>reduced text is returned</returns>
        private string getReducedText(string text, string key, out string expandedKey, out List<int> spaceLocs)
        {
            int countKey = key.Length;
            int countText = text.Length;
            string reducedText;

            for (int i = 0; i < countText; i++)
            {
                if (j == countKey)
                {
                    j = 0;
                }

                if (!text.ElementAt(i).Equals(' '))
                {
                    expandedKey += key.ElementAt(j++).ToString();
                    reducedText += text.ElementAt(i).ToString();
                }
                else
                {
                    spaceLocs.Add(i);
                }
            }
            return reducedText;

        }

        /// <summary>
        /// used to set the spaces of the returning text.
        /// </summary>
        /// <param name="cipherText">cipher text or the plain text which required to set the spaces accordingly</param>
        /// <param name="spaceLocs">List of spaces which was in the original string</param>
        /// <returns>the complete string is return to the controller</returns>
        private string setSpacesToText(string cipherText, List<int> spaceLocs)
        {
            string returnText = "";
            int returnTextIndex = 0;
            int j = 0;

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (returnTextIndex == spaceLocs.ElementAt(j))
                {
                    j++;
                    if (j == spaceLocs.Count)
                        j--;
                    returnTextIndex++;
                    returnText += " ";
                    returnText += cipherText.ElementAt(i).ToString();
                }
                else
                {
                    returnText += cipherText.ElementAt(i).ToString();
                }
                returnTextIndex++;
            }
            return returnText;
        }

        /// <summary>
        /// Convert the plain text to cipher or vice versa
        /// </summary>
        /// <param name="text">input text</param>
        /// <param name="key">input key</param>
        /// <param name="encrypt">encrypt the stirng if true</param>
        /// <returns>encrypted or decrypted string is returned</returns>
        private string convertText(string text, string key, bool encrypt)
        {
            int countText = text.Length;
            int countKey = key.Length;
            string expandedKey = "";
            string reducedText = "";
            string cipherText = "";

            List<int> spaceLocations = new List<int>();

            reducedText = getReducedText(text, key, out expandedKey, out spaceLocations);

            if (reducedText.Length == expandedKey.Length)
            {
                if (encrypt)
                {
                    for (int i = 0; i < reducedText.Length; i++)
                    {
                        cipherText += getCharacter((getLocation(reducedText.ElementAt(i)) + getLocation(expandedKey.ElementAt(i))) % 26).ToString();
                    }

                }
                else
                {
                    for (int i = 0; i < reducedText.Length; i++)
                    {
                        int value = getLocation(reducedText.ElementAt(i)) - getLocation(expandedKey.ElementAt(i));
                        if (value < 0)
                            value += 26; // otherwise when the value is negative reading the exact value is wrong if we just take the absolute value.
                        cipherText += (getCharacter(value % 26)).ToString();
                    }
                }
            }

            return setSpacesToText(cipherText, spaceLocations);
        }

        /// <summary>
        /// used to get the interger for the character specified
        /// </summary>
        /// <param name="cp">the character which needed in getting the location. Currently it only supports the alphebatical character set only</param>
        /// <returns>the assigned integer value for the character is returned, (in the range of 0 -25)</returns>
        private int getLocation(char cp)
        {
            switch (cp.ToString().ToUpper())
            {
                case "A": return 0;
                case "B": return 1;
                case "C": return 2;
                case "D": return 3;
                case "E": return 4;
                case "F": return 5;
                case "G": return 6;
                case "H": return 7;
                case "I": return 8;
                case "J": return 9;
                case "K": return 10;
                case "L": return 11;
                case "M": return 12;
                case "N": return 13;
                case "O": return 14;
                case "P": return 15;
                case "Q": return 16;
                case "R": return 17;
                case "S": return 18;
                case "T": return 19;
                case "U": return 20;
                case "V": return 21;
                case "W": return 22;
                case "X": return 23;
                case "Y": return 24;
                case "Z": return 25;
                default: return -1;
            }
        }

        /// <summary>
        /// used to get the character assigned to the integer specified
        /// </summary>
        /// <param name="val">integer value which represent a character(in the range of 0 - 25)</param>
        /// <returns>returns the character corresponding to the integer specified</returns>
        private char getCharacter(int val)
        {
            switch (val)
            {
                case 0: return 'A';
                case 1: return 'B';
                case 2: return 'C';
                case 3: return 'D';
                case 4: return 'E';
                case 5: return 'F';
                case 6: return 'G';
                case 7: return 'H';
                case 8: return 'I';
                case 9: return 'J';
                case 10: return 'K';
                case 11: return 'L';
                case 12: return 'M';
                case 13: return 'N';
                case 14: return 'O';
                case 15: return 'P';
                case 16: return 'Q';
                case 17: return 'R';
                case 18: return 'S';
                case 19: return 'T';
                case 20: return 'U';
                case 21: return 'V';
                case 22: return 'W';
                case 23: return 'X';
                case 24: return 'Y';
                case 25: return 'Z';
                default: return '?';
            }
        }

        
    }
}
