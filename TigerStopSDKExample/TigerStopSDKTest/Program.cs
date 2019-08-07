using System;
using System.Collections.Generic;
using System.Threading;
using TigerStopAPI;

namespace TigerStopSDKExample
{
    class Program
    {
        static TigerStopAPI.TigerStop_IO io;

        static void Main(string[] args)
        {
            Console.WriteLine("Ready to connect....");

            Thread.Sleep(2000);

            Start:

            Console.Write("(1) Enter Comport and Baudrate\n(2) Search for Available Connections\n(3) Exit\nSELECT OPTION : ");
            string input = Console.ReadLine();

            try
            {
                switch (input)
                {
                    case "1":
                        string comport;
                        int baud;
                        Console.Write("Enter Comport : ");
                        comport = Console.ReadLine().ToUpper();
                        Console.Write("Enter Baudrate : ");
                        baud = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Connecting to " + comport + "....");

                        io = new TigerStopAPI.TigerStop_IO(comport, baud);

                        if (io.IsOpen)
                        {
                            Console.WriteLine("Successfully connected.");

                            goto MainLoop;
                        }
                        else
                        {
                            Console.WriteLine("Connection failed.");
                            goto Start;
                        }
                    case "2":
                        Console.WriteLine("Searching....");

                        List<KeyValuePair<string, int>> con = new List<KeyValuePair<string, int>>();
                        con = TigerStopAPI.TigerStop_IO.Connections();

                        if (con.Count > 0)
                        {
                            foreach (KeyValuePair<string, int> c in con)
                            {
                                Console.WriteLine("Comport : " + c.Key + " | Baudrate : " + c.Value);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No connections were found.");
                        }

                        goto Start;
                    case "3":
                        goto Exit;
                    default:
                        Console.WriteLine("Invalid Option.");
                        goto Start;
                }
            }
            catch
            {
                Console.WriteLine("Error Occurred.");
                goto Start;
            }

            MainLoop:

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("==================================================");
            Console.WriteLine("                    -READY-                       ");
            Console.WriteLine("==================================================");

            while (true)
            {
                if (InputHandler())
                {
                    break;
                }
            }
            
            Exit:

            Console.WriteLine("Exiting....");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }

        public static bool InputHandler()
        {
            bool exit = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter Command : ");
            string[] input = Console.ReadLine().Split(' ');
            Console.ForegroundColor = ConsoleColor.Red;

            switch (input[0])
            {
                case "Exit":
                case "EXIT":
                case "exit":
                case "x":
                case "X":
                    exit = true;
                    break;
                case "Move":
                case "MOVE":
                case "move":
                case "m":
                case "M":
                    if (io.MoveTo(input[1]))
                    {
                        Console.WriteLine("Move Successful");
                    }
                    else
                    {
                        Console.WriteLine("Move Unsuccessful");
                    }

                    exit = false;

                    break;
                case "Cycle":
                case "CYCLE":
                case "cycle":
                case "c":
                case "C":
                    if (io.CycleTool())
                    {
                        Console.WriteLine("Cycle Successful");
                    }
                    else
                    {
                        Console.WriteLine("Cycle Unsuccessful");
                    }

                    exit = false;

                    break;
                case "Help":
                case "HELP":
                case "help":
                case "h":
                case "H":
                    Console.WriteLine("==================================================\n" +
                                      "                     -HELP-                       \n" +
                                      "==================================================\n" +
                                      "Move : Moves the machine to the desired position.\n" +
                                      " - Move [position] | move [position] | MOVE [position] | M [position] | m [position] \n" +
                                      "\n" +
                                      "Cycle : Cycles the tool of the machine.\n" +
                                      " - Cycle | cycle | CYCLE | c | C \n" +
                                      "\n" +
                                      "Home : Homes the machine, returning to the home position.\n" +
                                      " - Home | home | HOME | hm | HM \n" +
                                      "\n" +
                                      "Sleep : Sets the drive to sleep.\n" +
                                      " - Sleep | sleep | SLEEP | sl | SL \n" +
                                      "\n" +
                                      "Wake Up : Wakes the drive from sleep.\n" +
                                      " - Wake | wake | WAKE | w | W \n" + 
                                      "\n" +
                                      "Analog : Returns the analog values tracked by the amp.\n" +
                                      " - Analog | analog | ANALOG | a | A \n" +
                                      " - Analog [1-5] | analog [1-5] | ANALOG [1-5] | a [1-5] | A [1-5] \n" +
                                      "\n" +
                                      "Log : Returns the log of commands and errors tracked be the amp.\n" +
                                      " - Log | log | LOG | l | L \n" +
                                      " - Log [index] | log [index] | LOG [index] | l [index] | L [index] \n" +
                                      "\n" +
                                      "Counters : Returns the counter values tracked by the amp.\n" +
                                      " - Count | count | COUNT | ct | CT \n" +
                                      " - Count [1-25] | count [1-25] | COUNT [1-25] | ct [1-25] | CT [1-25] \n" +
                                      "\n" +
                                      "Position : Returns the current position of the machine.\n" +
                                      " - Pos | pos | POS | p | P \n" +
                                      "\n" +
                                      "Status : Returns the current status of the amp.\n" +
                                      " - Status | status | STATUS | s | S \n" +
                                      "\n" +
                                      "Setting : Returns the value of the desired setting.\n" +
                                      " - Sett [index] | sett [index] | SETT [index] | st [index] ST [index] \n" +
                                      "\n" +
                                      "Update Setting : Changes a specified setting to a specified value.\n" +
                                      " - Updt [Index] [Value] | updt [Index] [Value] | UPDT [Index] [Value] | u [Index] [Value] | U [Index] [Value] \n" +
                                      "\n" +
                                      "IO Connection : Turns the specified IO connection on or off.\n" +
                                      " - Io [IO#] [on/off] | io [IO#] [on/off] | IO [IO#] [on/off] | i [IO#] [on/off] | I [IO#] [on/off] \n" +
                                      "\n" +
                                      "Exit : Exit the program.\n" +
                                      " - Exit | exit | EXIT | x | X \n");
                    exit = false;
                    break;
                case "Analog":
                case "ANALOG":
                case "analog":
                case "a":
                case "A":
                    if (input.Length > 1)
                    {
                        Console.WriteLine(io.GetAnalog(input[1]));
                    }
                    else
                    {
                        foreach (string s in io.GetAnalog())
                        {
                            Console.WriteLine(s);
                        }
                    }

                    exit = false;

                    break;
                case "Log":
                case "LOG":
                case "log":
                case "l":
                case "L":
                    if (input.Length > 1)
                    {
                        foreach (string s in io.GetLog(input[1]))
                        {
                            Console.WriteLine(s);
                        }
                    }
                    else
                    {
                        foreach (string s in io.GetLog())
                        {
                            Console.WriteLine(s);
                        }
                    }

                    exit = false;

                    break;
                case "Count":
                case "COUNT":
                case "count":
                case "ct":
                case "CT":
                    if (input.Length > 1)
                    {
                        Console.WriteLine(io.GetCounter(input[1]));
                    }
                    else
                    {
                        foreach (string s in io.GetCounter())
                        {
                            Console.WriteLine(s);
                        }
                    }

                    exit = false;

                    break;
                case "Pos":
                case "POS":
                case "pos":
                case "p":
                case "P":
                    Console.WriteLine(io.GetPosition());

                    exit = false;

                    break;
                case "Status":
                case "STATUS":
                case "status":
                case "s":
                case "S":

                    Console.WriteLine(io.GetStatus());

                    exit = false;

                    break;
                case "Sett":
                case "SETT":
                case "sett":
                case "st":
                case "ST":
                    // A quick check of the first character in the second argument will determine if a setting name was sent or a setting index.
                    // If a setting index, a digit, was found then GetSetting() can be called immediately.
                    if (char.IsDigit(input[1], 0))
                    {
                        Console.WriteLine(io.GetSetting(Convert.ToInt32(input[1])));
                    }
                    // Else, the full setting name, not a digit, was found and the setting name needs to be concatenated.
                    else if (!char.IsDigit(input[1], 0))
                    {
                        // 'newInput' is the same length as 'input' minus the command.
                        string[] newInput = new string[input.Length - 1];

                        Array.Copy(input, 1, newInput, 0, (input.Length - 1));

                        string setting = string.Join(" ", newInput);

                        Console.WriteLine(io.GetSetting(setting));
                    }
                    else
                    {
                        goto default;
                    }

                    exit = false;

                    break;
                case "Home":
                case "HOME":
                case "home":
                case "hm":
                case "HM":
                    io.HomeDevice();

                    Console.WriteLine("Home Complete");

                    exit = false;
                    
                    break;
                case "FEL":
                case "fel":
                case "minmax":
                case "min-max":
                case "MINMAX":
                case "MIN-MAX":
                case "mm":
                case "MM":
                    io.FindEndLimits();

                    Console.WriteLine("Min-max complete");

                    exit = false;

                    break;
                case "Sleep":
                case "SLEEP":
                case "sleep":
                case "sl":
                case "SL":
                    io.DriveSleep();

                    exit = false;

                    break;
                case "Wake":
                case "WAKE":
                case "wake":
                case "w":
                case "W":
                    io.DriveWake();

                    exit = false;

                    break;
                case "Updt":
                case "UPDT":
                case "updt":
                case "u":
                case "U":
                    // A quick check of the first character in the second argument will determine if a setting name was sent or a setting index.
                    // If a setting index, a digit, was found then UpdateSetting() can be called immediately.
                    if (char.IsDigit(input[1], 0))
                    {
                        if (io.UpdateSetting(Convert.ToInt32(input[1]), Convert.ToDouble(input[2])))
                        {
                            Console.WriteLine("Update Successful.");
                        }
                        else
                        {
                            Console.WriteLine("Update Unsuccessful.");
                        }

                        exit = false;
                    }
                    // Else, the full setting name, not a digit, was found and the setting name needs to be concatenated.
                    else
                    {
                        // 'newInput' is the same length as 'input' minus the command and the new setting value.
                        string[] newInput = new string[input.Length - 2];

                        Array.Copy(input, 1, newInput, 0, (input.Length - 2));

                        string setting = string.Join(" ", newInput);

                        if (io.UpdateSetting(setting, input[input.Length - 1]))
                        {
                            Console.WriteLine("Update Successful.");
                        }
                        else
                        {
                            Console.WriteLine("Update Unsuccessful.");
                        }

                        exit = false;
                    }
                    break;
                case "io":
                case "IO":
                case "Io":
                case "i":
                case "I":
                    if (input[2].ToLower() == "on")
                    {
                        if (!io.IO_Connection(Convert.ToInt32(input[1]), true))
                        {
                            goto default;
                        }
                    }
                    else if (input[2].ToLower() == "off")
                    {
                        if (!io.IO_Connection(Convert.ToInt32(input[1]), false))
                        {
                            goto default;
                        }
                    }
                    else
                    {
                        goto default;
                    }

                    exit = false;

                    break;
                default:
                    Console.WriteLine("Invalid Command.\nType 'help' or 'h' to view list of available commands.\n");
                    exit = false;
                    break;

            }

            return exit;
        }
    }
}
