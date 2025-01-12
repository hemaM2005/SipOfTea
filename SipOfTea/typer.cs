using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SipOfTea
{
    public class Script
    {
        public void print(string text, int timer = 50)
        {
            foreach (char c in text) { Console.Write(c); Thread.Sleep(timer); }
        }
        public void printline(string text, int timer = 50)
        {
            foreach (char c in text) { Console.Write(c); Thread.Sleep(timer); }
            Console.WriteLine();
        }


        public void say(string path, int point, int speed = 40)
        {
            // getting text 

            path = path + ".txt";
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentDirectory, path);
            path = filePath;
            string text = File.ReadAllText(path);
            int ind1 = text.IndexOf("///" + point + "///") + 8;
            point++;
            int ind2 = text.IndexOf("///" + point + "///") - 1;
            if (text.IndexOf("///" + point + "///") == -1) { ind2 = text.Length; }

            // printing
            {
                string to_print = text.Substring(ind1, ind2 - ind1);
                Colors change = new Colors();
                bool in_diff_color = true;
                if (Console.ForegroundColor == ConsoleColor.White) { in_diff_color = false; }
                int i = 0;
                while (true)
                {
                    if (i >= to_print.Length) { break; }
                    if (Console.KeyAvailable == false)
                    {
                        if (to_print[i] == '#') { Thread.Sleep(40); i++; continue; }
                        else if (to_print[i] == '_')
                        {
                            if (Console.ForegroundColor == ConsoleColor.White) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                            else { Console.ForegroundColor = ConsoleColor.White; }
                            i++;
                            continue;
                        }
                        else if (to_print[i] == '$')
                        {
                            if (in_diff_color == false)
                            {
                                i++;
                                int dollar1 = i;
                                while (to_print[i] != '$') { i++; }
                                int dollar2 = i;
                                i++;
                                change.forecol(Convert.ToInt32(to_print.Substring(dollar1, dollar2 - dollar1)));
                                in_diff_color = true;
                                continue;
                            }
                            else { change.forecol(); 
                                i += 2; 
                                in_diff_color = false;
                                continue; }
                        }
                        else
                        {
                            Console.Write(to_print[i]);
                            Thread.Sleep(speed);
                            i++;
                            continue;
                        }
                    }
                    var pressed = Console.ReadKey(true).Key;
                    if (pressed == ConsoleKey.Enter)
                    {
                        bool in_diff_colour = true;
                        if (Console.ForegroundColor == ConsoleColor.White) { in_diff_colour = false; }
                        for (int j = 0; j < to_print.Substring(i).Length; j++)
                        {
                            char c = to_print.Substring(i)[j];
                            if (c == '#') { continue; }
                            if (c == '_')
                            {
                                if (Console.ForegroundColor == ConsoleColor.White) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                                else { Console.ForegroundColor = ConsoleColor.White; }
                                continue;
                            }
                            if (c == '$')
                            {
                                if (in_diff_colour == false)
                                {
                                    string test = to_print.Substring(i);
                                    j++;
                                    int dollar1 = j; //$3131$ d1=1, d2=5
                                    while (test[j] != '$') { j++; }
                                    int dollar2 = j;
                                    change.forecol(Convert.ToInt32(test.Substring(dollar1, dollar2 - dollar1)));
                                    in_diff_colour = true;
                                    continue;
                                }
                                else
                                {
                                    change.forecol();
                                    in_diff_colour = false;
                                    j ++;
                                    continue;
                                }
                            }
                            else
                            {
                                Console.Write(c);
                                continue;
                            }
                        }
                        break;
                    }
                    else { continue; }

                }
            }
            /* OLD CODE
            foreach (char c in text.Substring(ind1, ind2 - ind1))
            {
                if (c == '#') { Thread.Sleep(40); continue; }
                if (c == '_')
                {
                    if (Console.ForegroundColor == ConsoleColor.White) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                    else { Console.ForegroundColor = ConsoleColor.White; }
                }
                else
                {
                    Console.Write(c);
                    Thread.Sleep(40);
                }
            } */
        }

        public void ask(string path, int point, int speed = 40)
        {
            // getting text

            path = path + ".txt";
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentDirectory, path);
            path = filePath;
            string text = File.ReadAllText(path);
            int ind1 = text.IndexOf("///" + point + "///") + 8;
            int ind2 = text.IndexOf("///" + (point + 1) + "///") - 1;
            if (text.IndexOf("///" + (point + 1) + "///") == -1) { ind2 = (text.Length); } // old line: if (ind2 < 0) { ind2 += (text.Length - 6); }
            string oldtext = text;                             // do i need this
            text = text.Substring(ind1, ind2 - ind1);

            // seperating into options and answers
            List<string> options = new List<string>();
            List<int> indos = new List<int>();
            int no_of_options = 1;
            while (true)
            {
                if (text.IndexOf("\\" + no_of_options + "\\") == -1) { break; }
                int indo1 = text.IndexOf("\\" + no_of_options + "\\") + 4;
                int indo2 = text.IndexOf("\\" + (no_of_options + 1) + "\\") - 1;
                if (text.IndexOf("\\" + (no_of_options + 1) + "\\") == -1) { indo2 = text.IndexOf("\\1/") - 2; }
                options.Add(text.Substring(indo1, indo2 - indo1));
                indos.Add(indo1);
                no_of_options++;
            }
            no_of_options -= 1;

            List<string> answer = new List<string>();
            int j = 1;
            while (true)
            {
                if (text.IndexOf("\\" + j + "/") == -1) { break; }
                int inda1 = text.IndexOf("\\" + j + "/") + 4;
                int inda2 = text.IndexOf("\\" + (j + 1) + "/") - 1;
                if (text.IndexOf("\\" + (j + 1) + "/") == -1)
                {
                    inda2 = text.IndexOf("///" + (point + 1) + "///") - 2;
                    if (text.IndexOf("///" + (point + 1) + "///") == -1) { inda2 = text.Length; }
                }
                answer.Add(text.Substring(inda1, inda2 - inda1));
                j++;
            }

            // i am the arrow AH AH AHHHHHHHHHHH AHHHHHHHHHH HAHHHHH

            //// blink - blinks ONCE (put this in loop WITH thread.sleep)
            void blink()
            {
                char c = '>';
                (int x, int y) pos = Console.GetCursorPosition();
                Colors change = new Colors();
                if (Console.ForegroundColor == ConsoleColor.Black) { change.forecol(31); Console.Write(c); }
                else { change.forecol(0); Console.Write(c); }
                Console.SetCursorPosition(pos.x, pos.y);
            }

            //// using blink
            Console.WriteLine();
            List<(int x, int y)> nav = new List<(int x, int y)>(); //navigate
            if (point == 6)
            {
                Colors change = new Colors();
                List<int> shades = new List<int>() { 5, 10, 14 };
                for (int i = 0; i < 3; i++)
                {
                    nav.Add(Console.GetCursorPosition());
                    string s = options[i];
                    int change_shade = s.IndexOf("Add ") + 3;
                    Console.Write(s.Substring(0, change_shade));
                    change.forecol(shades[i]);
                    Console.WriteLine(s.Substring(change_shade));
                    change.forecol();
                }
            }
            else
            {
                foreach (string s in options)
                {
                    nav.Add(Console.GetCursorPosition());
                    Colors change = new Colors();
                    bool in_diff_col = true;
                    if (Console.ForegroundColor == ConsoleColor.White) { in_diff_col = false; }
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i] == '$')
                        {
                            if (in_diff_col == false)
                            {
                                i++;
                                int dollar1 = i;
                                while (s[i] != '$') { i++; }
                                int dollar2 = i + 1;
                                string newcol = s.Substring(dollar1, dollar2 - dollar1 - 1);
                                change.forecol(Convert.ToInt32(newcol));
                                in_diff_col = true;
                            }
                            else { change.forecol(); i += 2; in_diff_col = false; }
                        }
                        else
                        {
                            Console.Write(s[i]);
                        }
                    }
                    Console.WriteLine();
                }
            }

            //// first, we move cursor backwards
            Console.SetCursorPosition(nav[0].x, nav[0].y);

            //// now we check for key presed by user
            int location = 0;

            if (point == 1)
            {
                List<bool> escape = new List<bool>() { false, false, true };
                while (true)
                {
                    while (Console.KeyAvailable == false) { blink(); Thread.Sleep(500); }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write('>');

                    var pressed = Console.ReadKey(true).Key;
                    if (pressed == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if (pressed == ConsoleKey.UpArrow)
                    {
                        if (location == 0) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location--;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else if (pressed == ConsoleKey.DownArrow)
                    {
                        if (location == no_of_options - 1) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location++;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                }


                // printing response and ending
                Console.SetCursorPosition(0, nav[no_of_options - 1].y + 2);
                Thread.Sleep(1200);
                if (point == 13)
                {
                    if (location == 1)
                    {
                        for (int i = 0; i < answer[location].Length; i++)
                        {
                            if (answer[location][i] == '%')
                            {
                                i++;
                                (int x, int y) eraser = Console.GetCursorPosition();
                                while (answer[location][i] != '%') { Console.Write(answer[location][i]); Thread.Sleep(5); i++; }
                                i++;
                                Console.SetCursorPosition(eraser.x, eraser.y);
                                for (int no_of_spaces = 0; no_of_spaces < 20; no_of_spaces++) { Console.Write(" "); Thread.Sleep(5); }
                                Console.WriteLine();
                                continue;
                            }
                            else
                            {
                                if (answer[location][i] == '#') { Thread.Sleep(speed); continue; }
                                else { Console.Write(answer[location][i]); Thread.Sleep(speed); continue; }
                            }
                        }

                    }
                    else
                    {
                        foreach (char c in answer[location])
                        {
                            if (c == '#') { Thread.Sleep(40); }
                            else { Console.Write(c); Thread.Sleep(speed); }
                        }
                    }
                }
                else if (point == 1)
                {
                    if (location == 2)
                    {
                        string ending = "Oh.#.#.# is anyone there?###### Well, I guess there's noone there.#### Huh.####";
                        foreach (char c in ending) { if (c == '#') { Thread.Sleep(40); }
                            else { Console.Write(c); Thread.Sleep(40); } }
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                    else
                    {
                        foreach (char c in answer[location])
                        {
                            if (c == '#') { Thread.Sleep(40); }
                            else { Console.Write(c); Thread.Sleep(speed); }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < answer[location].Length; i++)
                    {
                        char c = answer[location][i];
                        bool in_diff_color = true;
                        Colors change = new Colors();
                        if (Console.ForegroundColor == ConsoleColor.White) { in_diff_color = false; }
                        if (c == '#') { Thread.Sleep(40); }
                        else if (c == '$')
                        {
                            if (in_diff_color == false)
                            {
                                i++;
                                int dollar1 = i;
                                while (answer[location][i] != '$') { i++; }
                                int dollar2 = i;
                                i++;
                                change.forecol(Convert.ToInt32(answer[location].Substring(dollar1, dollar2 - dollar1)));
                                in_diff_color = true;
                                continue;
                            }
                            else
                            {
                                change.forecol();
                                i += 2;
                                in_diff_color = false;
                                continue;
                            }
                        }
                        else { Console.Write(c); Thread.Sleep(speed); }
                    }
                }
            }
            else if (point == 5)
            {
                List<bool> rightchoice = new List<bool>() { false, true, false };
                while (true)
                {
                    while (Console.KeyAvailable == false) { blink(); Thread.Sleep(500); }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write('>');

                    var pressed = Console.ReadKey(true).Key;
                    if (pressed == ConsoleKey.Enter)
                    {
                        if (rightchoice[location] == true) { break; }
                        else
                        {
                            rightchoice[location] = true;
                            // string mkc = options[1] + "               ";
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                            foreach (char c in options[1]) { Console.Write(c); Thread.Sleep(2); }
                            /* this is the most disgusting piece of code i have ever touched in my life it might even be the most disgusting
                             * thing ive touched in my life there are very few things that have come close to this ive just spent an hour screaming 
                             * an unhealthy amount of obsceneties to my screen and i genuinely unironically from the bottom of my heart hold the
                             * belief that everyone should go kill themselves
                             */
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                            continue;
                        }
                    }
                    else if (pressed == ConsoleKey.UpArrow)
                    {
                        if (location == 0) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location--;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else if (pressed == ConsoleKey.DownArrow)
                    {
                        if (location == no_of_options - 1) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location++;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                }

                // printing response and ending
                Console.SetCursorPosition(0, nav[no_of_options - 1].y + 2);
                Thread.Sleep(1200);
                foreach (char c in answer[location])
                {
                    if (c == '#') { Thread.Sleep(40); }
                    else { Console.Write(c); Thread.Sleep(40); }
                }
                return;
            }
            else if (point == 7)
            {
                List<bool> rightchoice = new List<bool>() { true, false };
                while (true)
                {
                    while (Console.KeyAvailable == false) { blink(); Thread.Sleep(500); }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write('>');

                    var pressed = Console.ReadKey(true).Key;
                    if (pressed == ConsoleKey.Enter)
                    {
                        if (rightchoice[location] == true) { break; }
                        else
                        {
                            rightchoice[location] = true;
                            // string mkc = options[1] + "               ";
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                            foreach (char c in options[0]) { Console.Write(c); Thread.Sleep(2); }
                            /* this is the most disgusting piece of code i have ever touched in my life it might even be the most disgusting
                             * thing ive touched in my life there are very few things that have come close to this ive just spent an hour screaming 
                             * an unhealthy amount of obsceneties to my screen and i genuinely unironically from the bottom of my heart hold the
                             * belief that everyone should go kill themselves
                             */
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                            continue;
                        }
                    }
                    else if (pressed == ConsoleKey.UpArrow)
                    {
                        if (location == 0) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location--;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else if (pressed == ConsoleKey.DownArrow)
                    {
                        if (location == no_of_options - 1) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location++;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                }

                // printing response and ending
                Console.SetCursorPosition(0, nav[no_of_options - 1].y + 2);
                Thread.Sleep(1200);
                foreach (char c in answer[location])
                {
                    if (c == '#') { Thread.Sleep(40); }
                    else { Console.Write(c); Thread.Sleep(40); }
                }
                return;
            }
            else
            {
                while (true)
                {
                    while (Console.KeyAvailable == false) { blink(); Thread.Sleep(500); }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write('>');

                    var pressed = Console.ReadKey(true).Key;
                    if (pressed == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if (pressed == ConsoleKey.UpArrow)
                    {
                        if (location == 0) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location--;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else if (pressed == ConsoleKey.DownArrow)
                    {
                        if (location == no_of_options - 1) { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                        else
                        {
                            location++;
                            Console.SetCursorPosition(nav[location].x, nav[location].y);
                        }
                    }
                    else { Console.SetCursorPosition(nav[location].x, nav[location].y); continue; }
                }


                // printing response and ending
                Console.SetCursorPosition(0, nav[no_of_options - 1].y + 2);
                Thread.Sleep(1200);
                if (point == 13)
                {
                    if (location == 1)
                    {
                        for (int i = 0; i < answer[location].Length; i++)
                        {
                            if (answer[location][i] == '%')
                            {
                                i++;
                                (int x, int y) eraser = Console.GetCursorPosition();
                                while (answer[location][i] != '%') { Console.Write(answer[location][i]); Thread.Sleep(5); i++; }
                                i++;
                                Console.SetCursorPosition(eraser.x, eraser.y);
                                for (int no_of_spaces = 0; no_of_spaces < 20; no_of_spaces++) { Console.Write(" "); Thread.Sleep(5); }
                                Console.WriteLine();
                                continue;
                            }
                            else
                            {
                                if (answer[location][i] == '#') { Thread.Sleep(speed); continue; }
                                else { Console.Write(answer[location][i]); Thread.Sleep(speed); continue; }
                            }
                        }

                    }
                    else
                    {
                        foreach (char c in answer[location])
                        {
                            if (c == '#') { Thread.Sleep(40); }
                            else { Console.Write(c); Thread.Sleep(speed); }
                        }
                    }
                }
                else
                {
                    for(int i = 0; i < answer[location].Length; i++)
                    {
                        char c = answer[location][i];
                        bool in_diff_color = true;
                        Colors change = new Colors();
                        if (Console.ForegroundColor == ConsoleColor.White) { in_diff_color = false; }
                        if (c == '#') { Thread.Sleep(40); }
                        else if (c == '$')
                        {
                            if (in_diff_color ==  false)
                            {
                                i++;
                                int dollar1 = i;
                                while (answer[location][i] != '$') { i++; }
                                int dollar2 = i;
                                i++;
                                change.forecol(Convert.ToInt32(answer[location].Substring(dollar1, dollar2 - dollar1)));
                                in_diff_color = true;
                                continue;
                            }
                            else
                            {
                                change.forecol();
                                i += 2;
                                in_diff_color = false;
                                continue;
                            }
                        }
                        else { Console.Write(c); Thread.Sleep(speed); }
                    }
                }
            }

        }
    }


    public class Colors
    {

        public Dictionary<int, string> list = new Dictionary<int, string>()
            {
                {0, "Black"},
                {1, "DarkBlue"},
                {2, "DarkGreen"},
                {3, "DarkCyan"},
                {4, "DarkRed"},
                {5, "DarkMagenta"},
                {6, "DarkYellow"},
                {7, "Gray"},
                {8, "DarkGray"},
                {9, "Blue"},
                {10, "Green"},
                {11, "Cyan"},
                {12, "Red"},
                {13, "Magenta"},
                {14, "Yellow"},
                {15, "White"},
                {16, "Black"},
                {17, "DarkBlue"},
                {18, "DarkGreen"},
                {19, "DarkCyan"},
                {20, "DarkRed"},
                {21, "DarkMagenta"},
                {22, "DarkYellow"},
                {23, "Gray"},
                {24, "DarkGray"},
                {25, "Blue"},
                {26, "Green"},
                {27, "Cyan"},
                {28, "Red"},
                {29, "Magenta"},
                {30, "Yellow"},
                {31, "White"}
            };
        public void forecol(int code = 31)
        {
            if (code == 0) { Console.ForegroundColor = ConsoleColor.Black; }
            if (code == 1) { Console.ForegroundColor = ConsoleColor.DarkBlue; }
            if (code == 2) { Console.ForegroundColor = ConsoleColor.DarkGreen; }
            if (code == 3) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
            if (code == 4) { Console.ForegroundColor = ConsoleColor.DarkRed; }
            if (code == 5) { Console.ForegroundColor = ConsoleColor.DarkMagenta; }
            if (code == 6) { Console.ForegroundColor = ConsoleColor.DarkYellow; }
            if (code == 7) { Console.ForegroundColor = ConsoleColor.Gray; }
            if (code == 8) { Console.ForegroundColor = ConsoleColor.DarkGray; }
            if (code == 9) { Console.ForegroundColor = ConsoleColor.Blue; }
            if (code == 10) { Console.ForegroundColor = ConsoleColor.Green; }
            if (code == 11) { Console.ForegroundColor = ConsoleColor.Cyan; }
            if (code == 12) { Console.ForegroundColor = ConsoleColor.Red; }
            if (code == 13) { Console.ForegroundColor = ConsoleColor.Magenta; }
            if (code == 14) { Console.ForegroundColor = ConsoleColor.Yellow; }
            if (code == 15) { Console.ForegroundColor = ConsoleColor.White; }
            if (code == 16) { Console.ForegroundColor = ConsoleColor.Black; }
            if (code == 17) { Console.ForegroundColor = ConsoleColor.DarkBlue; }
            if (code == 18) { Console.ForegroundColor = ConsoleColor.DarkGreen; }
            if (code == 19) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
            if (code == 20) { Console.ForegroundColor = ConsoleColor.DarkRed; }
            if (code == 21) { Console.ForegroundColor = ConsoleColor.DarkMagenta; }
            if (code == 22) { Console.ForegroundColor = ConsoleColor.DarkYellow; }
            if (code == 23) { Console.ForegroundColor = ConsoleColor.Gray; }
            if (code == 24) { Console.ForegroundColor = ConsoleColor.DarkGray; }
            if (code == 25) { Console.ForegroundColor = ConsoleColor.Blue; }
            if (code == 26) { Console.ForegroundColor = ConsoleColor.Green; }
            if (code == 27) { Console.ForegroundColor = ConsoleColor.Cyan; }
            if (code == 28) { Console.ForegroundColor = ConsoleColor.Red; }
            if (code == 29) { Console.ForegroundColor = ConsoleColor.Magenta; }
            if (code == 30) { Console.ForegroundColor = ConsoleColor.Yellow; }
            if (code == 31) { Console.ForegroundColor = ConsoleColor.White; }
        }

        public void backcol(int code = 0)
        {
            if (code == 0) { Console.BackgroundColor = ConsoleColor.Black; }
            if (code == 1) { Console.BackgroundColor = ConsoleColor.DarkBlue; }
            if (code == 2) { Console.BackgroundColor = ConsoleColor.DarkGreen; }
            if (code == 3) { Console.BackgroundColor = ConsoleColor.DarkCyan; }
            if (code == 4) { Console.BackgroundColor = ConsoleColor.DarkRed; }
            if (code == 5) { Console.BackgroundColor = ConsoleColor.DarkMagenta; }
            if (code == 6) { Console.BackgroundColor = ConsoleColor.DarkYellow; }
            if (code == 7) { Console.BackgroundColor = ConsoleColor.Gray; }
            if (code == 8) { Console.BackgroundColor = ConsoleColor.DarkGray; }
            if (code == 9) { Console.BackgroundColor = ConsoleColor.Blue; }
            if (code == 10) { Console.BackgroundColor = ConsoleColor.Green; }
            if (code == 11) { Console.BackgroundColor = ConsoleColor.Cyan; }
            if (code == 12) { Console.BackgroundColor = ConsoleColor.Red; }
            if (code == 13) { Console.BackgroundColor = ConsoleColor.Magenta; }
            if (code == 14) { Console.BackgroundColor = ConsoleColor.Yellow; }
            if (code == 15) { Console.BackgroundColor = ConsoleColor.White; }
            if (code == 16) { Console.BackgroundColor = ConsoleColor.Black; }
            if (code == 17) { Console.BackgroundColor = ConsoleColor.DarkBlue; }
            if (code == 18) { Console.BackgroundColor = ConsoleColor.DarkGreen; }
            if (code == 19) { Console.BackgroundColor = ConsoleColor.DarkCyan; }
            if (code == 20) { Console.BackgroundColor = ConsoleColor.DarkRed; }
            if (code == 21) { Console.BackgroundColor = ConsoleColor.DarkMagenta; }
            if (code == 22) { Console.BackgroundColor = ConsoleColor.DarkYellow; }
            if (code == 23) { Console.BackgroundColor = ConsoleColor.Gray; }
            if (code == 24) { Console.BackgroundColor = ConsoleColor.DarkGray; }
            if (code == 25) { Console.BackgroundColor = ConsoleColor.Blue; }
            if (code == 26) { Console.BackgroundColor = ConsoleColor.Green; }
            if (code == 27) { Console.BackgroundColor = ConsoleColor.Cyan; }
            if (code == 28) { Console.BackgroundColor = ConsoleColor.Red; }
            if (code == 29) { Console.BackgroundColor = ConsoleColor.Magenta; }
            if (code == 30) { Console.BackgroundColor = ConsoleColor.Yellow; }
            if (code == 31) { Console.BackgroundColor = ConsoleColor.White; }
        }
    }
}