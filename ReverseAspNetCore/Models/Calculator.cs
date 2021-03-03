using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReverseAspNetCore.Models
{
    public class Calculator
    {
        private string numberInput;
        public string dec;
        public string bin;
        public string oct;
        public string hex;

        public Calculator(string NumberAsInt, string Type)
        {
            this.numberInput = NumberAsInt;

            if (Type == "decimal") {
                this.calcFromDecimal();
            }
            else if (Type == "octa")
            {
                this.calcFromOcta();
            }
            else if (Type == "hex")
            {
                this.calcFromHex();
            }
            else if (Type == "binary")
            {
                this.calcFromBinary();
            }
            else
            {
                //Exception?
            }
        }

        public void calcFromDecimal()
        {
            this.dec = this.numberInput;
            this.bin = this.calcBinFromDec();
            this.oct = this.calcBinToOct();
            this.hex = this.calcBinToHex();
        }
        public void calcFromBinary()
        {
            this.bin = this.numberInput;
            this.oct = this.calcBinToOct();
            this.dec = this.calcBinToDec();
            this.hex = this.calcBinToHex();
        }
        public void calcFromOcta()
        {
            this.oct = this.numberInput;
            this.bin = this.calcBinFromOct();
            this.dec = this.calcBinToDec();
            this.hex = this.calcBinToHex();
        }
        public void calcFromHex()
        {
            this.hex = this.numberInput;
            this.bin = this.calcBinFromHex();
            this.dec = this.calcBinToDec();
            this.oct = this.calcBinToOct();
        }


        public string calcBinFromDec()
        {
            try
            {
                decimal deci = Convert.ToDecimal(this.numberInput);
                string result = "";
                decimal tmp;

            
                for (int i = 1; i > 0; i++)
                {
                    tmp = deci / 2;
                    if (tmp != 0)
                    {
                        string tmpResult = tmp.ToString();
                        bool containsDec = tmpResult.Contains(",");
                        if (containsDec)
                        {
                            int convertToInt = Convert.ToInt32(Math.Floor(tmp));
                            result = result.Insert(result.Length, "1");
                            deci = Convert.ToDecimal(convertToInt);
                        }
                        else
                        {
                            result = result.Insert(result.Length, "0");
                            deci = tmp;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                result = this.Reverse(result);
            
                return result;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public string calcBinFromOct()
        {
            char[] cArray = this.oct.ToCharArray();
            string result = String.Empty;
            
            
            int tmp;

            for (int i = cArray.Length - 1; i > -1; i--)
            {
                int maxValue = 4;
                string tmpResult = String.Empty;

                tmp = Convert.ToInt32(cArray[i].ToString());

                for (int p = 0; p <= 2; p++)
                {
                    if (tmp >= maxValue)
                    {
                        tmpResult = tmpResult.Insert(tmpResult.Length, "1");
                        tmp = tmp - maxValue;
                    }
                    else
                    {
                        tmpResult = tmpResult.Insert(tmpResult.Length, "0");
                    }

                    maxValue = maxValue / 2;
                    if (maxValue == 0)
                    {
                        tmpResult = this.Reverse(tmpResult);
                        result = result.Insert(result.Length, tmpResult);
                        break;
                    }
                }
            };

            return result;
        }
        public string calcBinFromHex()
        {
            char[] cArray = this.hex.ToCharArray();
            string result = String.Empty;

            int tmp;

            for (int i = cArray.Length - 1; i > -1; i--)
            {
                int maxValue = 8;
                string tmpResult = String.Empty;

                tmp = Convert.ToInt32(this.translateHexToDec(cArray[i].ToString()));

                for (int p = 0; p <= 3; p++)
                {
                    if (tmp >= maxValue)
                    {
                        tmpResult = tmpResult.Insert(tmpResult.Length, "1");
                        tmp = tmp - maxValue;
                    }
                    else
                    {
                        tmpResult = tmpResult.Insert(tmpResult.Length, "0");
                    }

                    maxValue = maxValue / 2;
                    if (maxValue == 0)
                    {
                        tmpResult = this.Reverse(tmpResult);
                        result = result.Insert(result.Length, tmpResult);
                        break;
                    }
                }
            };

            result = this.Reverse(result);

            return result;
        }


        public string calcBinToDec()
        {
            char[] cArray = this.bin.ToCharArray();
            int result = 0;
            int increment = 1;

            for (int i = cArray.Length - 1; i > -1; i--)
            {
                if(cArray[i].ToString() == "1")
                {
                    result = result + increment;
                }
                increment = increment * 2;
            };

            return result.ToString();
        }
        public string calcBinToOct()
        {
            char[] cArray = this.bin.ToCharArray();
            string tmpResult = "0";
            string result = "";

            int increment = 1;
            int reset = 3;
            
            int length = cArray.Length - 1;
            int p = 0;

            for (int i = length; i > -1; i--)
            {
                if (length - reset == length - p)
                {
                    result += tmpResult;
                    tmpResult = "0";
                    increment = 1;
                    reset = reset + 3;
                }

                if (cArray[i].ToString() == "1")
                {
                    int tmpConvert = Convert.ToInt32(tmpResult) + increment;

                    tmpResult = tmpConvert.ToString();
                }
                increment = increment * 2;

                if(length == 0)
                {
                    result += tmpResult;
                }

                p++;
            };

            if(tmpResult != "0")
            {
                result += tmpResult;
            }

            result = this.Reverse(result);

            return result;
        }
        public string calcBinToHex()
        {
            char[] cArray = this.bin.ToCharArray();
            string tmpResult = "0";
            string result = "";

            int increment = 1;
            int reset = 4;

            int length = cArray.Length - 1;
            int p = 0;

            for (int i = length; i > -1; i--)
            {
                if (length - reset == length - p)
                {
                    result += this.translateDecToHex(Convert.ToInt32(tmpResult));
                    tmpResult = "0";
                    increment = 1;
                    reset = reset + 4;
                }

                if (cArray[i].ToString() == "1")
                {
                    int tmpConvert = Convert.ToInt32(tmpResult) + increment;

                    tmpResult = tmpConvert.ToString();
                }
                increment = increment * 2;

                if (length == 0)
                {
                    increment = 1;
                    result += this.translateDecToHex(Convert.ToInt32(tmpResult));
                }

                p++;
            };

            if (tmpResult != "0")
            {
                result += this.translateDecToHex(Convert.ToInt32(tmpResult));
            }

            result = this.Reverse(result);

            return result;
        }

        private string Reverse(string reverse)
        {
            char[] cArray = reverse.ToCharArray();
            string result = String.Empty;

            for (int i = cArray.Length - 1; i > -1; i--)
            {
                result += cArray[i];
            };

            return result;
        }

        private string translateHexToDec(string toTranslate)
        {
            
            switch (toTranslate)
            {
                case "A":
                    return "10";
                    break;
                case "B":
                    return "11";
                    break;
                case "C":
                    return "12";
                    break;
                case "D":
                    return "13";
                    break;
                case "E":
                    return "14";
                    break;
                case "F":
                    return "15";
                    break;
                case "a":
                    return "10";
                    break;
                case "b":
                    return "11";
                    break;
                case "c":
                    return "12";
                    break;
                case "d":
                    return "13";
                    break;
                case "e":
                    return "14";
                    break;
                case "f":
                    return "15";
                    break;
                case "0":
                    return "0";
                    break;
                case "1":
                    return "1";
                    break;
                case "2":
                    return "2";
                    break;
                case "3":
                    return "3";
                    break;
                case "4":
                    return "4";
                    break;
                case "5":
                    return "5";
                    break;
                case "6":
                    return "6";
                    break;
                case "7":
                    return "7";
                    break;
                case "8":
                    return "8";
                    break;
                case "9":
                    return "9";
                    break;
                default:
                    return "false";
                    break;
            }
            
        }
    
        private string translateDecToHex(int toTranslate)
        {
            if(toTranslate > 9)
            {
                switch (toTranslate)
                {
                    case 10:
                        return "A";
                        break;
                    case 11:
                        return "B";
                        break;
                    case 12:
                        return "C";
                        break;
                    case 13:
                        return "D";
                        break;
                    case 14:
                        return "E";
                        break;
                    case 15:
                        return "F";
                        break;
                    default:
                        return "false";
                        break;
                }
            }
            else
            {
                return toTranslate.ToString();
            }
        }

    }
}
