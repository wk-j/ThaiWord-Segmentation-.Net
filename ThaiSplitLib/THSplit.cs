using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace THSplit {
    public class Spliter {
        private string[] allWord;
        private Dictionary<char, List<string>> dictionary = new Dictionary<char, List<string>>();

        /// <summary>
        /// Assign Dictionary
        /// </summary>
        /// <param name="dict">string[]</param>
        public Spliter() {
            var assembly = Assembly.GetExecutingAssembly();
            var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("ThaiSplitLib.dictionary.txt"));
            string text = textStreamReader.ReadToEnd();
            allWord = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var word in allWord) {
                if (!dictionary.ContainsKey(word[0])) {
                    dictionary.Add(word[0], new List<string>());
                    //Console.WriteLine("added "+ word[0]);
                }
                dictionary[word[0]].Add(word);
            }
        }


        /// <summary>
        /// Assign Dictionary
        /// </summary>
        /// <param name="dict">string[]</param>
        public Spliter(string[] words) {
            allWord = words;
            foreach (var word in allWord) {
                if (!dictionary.ContainsKey(word[0])) {
                    dictionary.Add(word[0], new List<string>());
                }
                dictionary[word[0]].Add(word);
            }
        }

        public byte[] StringToAscii(string text) {
            string value = text;

            // Convert the string into a byte[].
            byte[] asciiBytes = Encoding.ASCII.GetBytes(value);
            return asciiBytes;
        }

        public string[] GetDictionary() => allWord;

        public List<string> SegmentByDictionary(string input) {
            // check space
            // eng type
            string[] inputSplitSpace = input.Split(' ');
            List<string> outputList = new List<string>();
            foreach (string item in inputSplitSpace) {
                // initial
                char[] inputChar = item.ToCharArray();
                string tmpString = "";
                for (int i = 0; i < inputChar.Length; i++) {
                    // eng langauge type
                    if (IsEngCharacter(inputChar[i])) {
                        tmpString += inputChar[i].ToString();
                        for (int j = i + 1; j < inputChar.Length; j++) {
                            if (IsEngCharacter(inputChar[j])) {
                                tmpString += inputChar[j];
                                i = j;
                            } else {
                                break;
                            }
                        }
                        outputList.Add(tmpString);
                        tmpString = "";
                    }
                    // thai langauge type
                    else if (IsVowelNeedConsonant(inputChar[i])) {
                        tmpString += inputChar[i].ToString();
                        for (int j = i + 1; j < inputChar.Length; j++) {
                            if (IsVowelNeedConsonant(inputChar[j])) {
                                tmpString += inputChar[j];
                                i = j;
                            } else {
                                break;
                            }

                        }
                        outputList.Add(tmpString);
                        tmpString = "";
                    } else if (IsToken(inputChar[i])) {
                        tmpString += inputChar[i].ToString();
                        for (int j = i + 1; j < inputChar.Length; j++) {
                            if (IsToken(inputChar[j])) {
                                tmpString += inputChar[j];
                                i = j;
                            } else {
                                break;
                            }

                        }
                        outputList.Add(tmpString);
                        tmpString = "";
                    } else if (IsConsonant(inputChar[i]) || isVowel(inputChar[i])) {
                        tmpString += inputChar[i].ToString();
                        string moretmp = tmpString;
                        bool isFound = false;
                        for (int j = i + 1; j < inputChar.Length; j++) {
                            moretmp += inputChar[j].ToString();
                            if (dictionary.ContainsKey(moretmp[0])) {
                                foreach (var word in dictionary[moretmp[0]]) {
                                    if (word == moretmp) {
                                        tmpString = moretmp;
                                        i = j;
                                        isFound = true;
                                        break;
                                    }
                                }
                            }
                            //int pos = Array.IndexOf(allWord, moretmp);
                            //// found in dictionary
                            //if (pos > -1)
                            //{
                            //    tmpString = moretmp;
                            //    i = j;
                            //    isFound = true;
                            //}

                        }
                        if (isFound) {
                            outputList.Add(tmpString);
                        }
                        tmpString = "";
                    } else {
                        outputList.Add(inputChar[i].ToString());
                    }
                }

            }
            return outputList;
        }

        public bool IsConsonant(char charNumber) => (charNumber >= 3585 && 3630 >= charNumber);

        public bool isVowel(char charNumber) => (charNumber >= 3632 && 3653 >= charNumber);

        public bool IsToken(char charNumber) => (charNumber >= 3656 && 3659 >= charNumber);

        public bool IsVowelNeedConsonant(char charNumber) {
            if (charNumber >= 3632 && 3641 >= charNumber)
                return true;

            if (charNumber == 3653)
                return true;

            return false;
        }


        public bool IsEngCharacter(char charNumber) {
            // large letter
            if (charNumber >= 41 && 90 >= charNumber) {
                return true;
            }
            // small letter
            else if (charNumber >= 61 && 122 >= charNumber) {
                return true;
            }
            return false;
        }
    }
}
