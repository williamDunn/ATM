//William Dunn

using System;
using System.Collections.Generic;

namespace cashMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize ATM & its contents

            Dictionary<string, int> cashMachine = new Dictionary<string, int>();

            cashMachine.Add("$100", 10);
            cashMachine.Add("$50", 10);
            cashMachine.Add("$20", 10);
            cashMachine.Add("$10", 10);
            cashMachine.Add("$5", 10);
            cashMachine.Add("$1", 10);

            //keeps track of the amount of bills available and their total value

            cashMachine.TryGetValue("$100", out int hundredBill);
            int hundredBillValue = hundredBill * 100;

            cashMachine.TryGetValue("$100", out int fiftyBill);
            int fiftyBillValue = fiftyBill * 50;

            cashMachine.TryGetValue("$100", out int twentyBill);
            int twentyBillValue = twentyBill * 20;

            cashMachine.TryGetValue("$100", out int tenBill);
            int tenBillValue = tenBill * 10;

            cashMachine.TryGetValue("$100", out int fiveBill);
            int fiveBillValue = fiveBill * 5;

            cashMachine.TryGetValue("$100", out int dollarBill);
            int dollarBillValue = dollarBill * 1;

            //total cash value of ATM contents

            int totalAmount = hundredBillValue + fiftyBillValue + twentyBillValue + tenBillValue + fiveBillValue + dollarBillValue;

                while (true)
                {
                    //Read in commands - if command requires more than one argument, parse by space

                    string caseSwitch = Console.ReadLine();
                    string[] extraArgs = caseSwitch.Split(' ');

                    ///////////////////////////////////////////////
                    //Begin checking in argument length to find specified command
                    //restock ATM - r, withdraw - w $amount, all ATM contents - i, specified ATM contents - i $amount, quit application - q

                    if (extraArgs.Length == 1)
                {
                    switch (caseSwitch.ToLower())
                    {
                        case "r":
                            cashMachine["$100"] = 10;
                            cashMachine["$50"] = 10;
                            cashMachine["$20"] = 10;
                            cashMachine["$10"] = 10;
                            cashMachine["$5"] = 10;
                            cashMachine["$1"] = 10;

                            totalAmount = 1860;

                            machineBalance(cashMachine);
                            break;

                        case "w":
                            Console.WriteLine("Please input the amount you'd like to withdraw. Format: w $amount");
                            break;

                        case "i":
                            foreach (var x in cashMachine) { Console.WriteLine(x.Key + " - " + x.Value); }
                            break;

                        case "q":
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Failure: Invalid Command");
                            break;
                    }
                }

                //withdraw command that checks the requested withdrawal amount(int withdrawalAmount) and if funds are available, dispenses
                //cash in the most efficient way possible.
                else if (extraArgs[0] == "w" && extraArgs.Length == 2)
                {
                    string inputAmount = extraArgs[1];
                    int withdrawalAmount = Int32.Parse(inputAmount.TrimStart('$'));
                    int withdrawn = withdrawalAmount;

                    if (withdrawalAmount > totalAmount)
                    {
                        Console.WriteLine("Failure insufficient funds");
                    }

                    else if (withdrawalAmount <= totalAmount)
                    {
                        do
                        {
                            if (withdrawn >= 100 && hundredBill > 0)
                            {
                                withdrawn = withdrawn - 100;
                                hundredBill = hundredBill - 1;
                                cashMachine["$100"] = hundredBill;
                                hundredBillValue = hundredBillValue - 100;
                                totalAmount = totalAmount - 100;
                            }

                            else if (withdrawn >= 50 && fiftyBill > 0)
                            {
                                withdrawn = withdrawn - 50;
                                fiftyBill = fiftyBill - 1;
                                cashMachine["$50"] = fiftyBill;
                                fiftyBillValue = fiftyBillValue - 50;
                                totalAmount = totalAmount - 50;
                            }

                            else if (withdrawn >= 20 && twentyBill > 0)
                            {
                                withdrawn = withdrawn - 20;
                                twentyBill = twentyBill - 1;
                                cashMachine["$20"] = twentyBill;
                                twentyBillValue = twentyBillValue - 20;
                                totalAmount = totalAmount - 20;
                            }

                            else if (withdrawn >= 10 && tenBill > 0)
                            {
                                withdrawn = withdrawn - 10;
                                tenBill = tenBill - 1;
                                cashMachine["$10"] = tenBill;
                                tenBillValue = tenBillValue - 10;
                                totalAmount = totalAmount - 10;
                            }

                            else if (withdrawn >= 5 && fiveBill > 0)
                            {
                                withdrawn = withdrawn - 5;
                                fiveBill = fiveBill - 1;
                                cashMachine["$5"] = fiveBill;
                                fiveBillValue = fiveBillValue - 5;
                                totalAmount = totalAmount - 5;
                            }

                            else if (withdrawn >= 1 && dollarBill > 0)
                            {
                                withdrawn = withdrawn - 1;
                                dollarBill = dollarBill - 1;
                                cashMachine["$1"] = dollarBill;
                                dollarBillValue = dollarBillValue - 1;
                                totalAmount = totalAmount - 1;
                            }
                        }
                        while (withdrawn > 0);

                        Console.WriteLine("Success: Dispensed $" + withdrawalAmount);
                        machineBalance(cashMachine);
                    }
                }

                //checks denominations inputted (must be $amount) and if valid will return the key and its values
                else if (extraArgs[0] == "i" && extraArgs.Length > 1 && extraArgs.Length < 8)
                {
                    string arg2, arg3, arg4, arg5, arg6, arg7;
                    arg3 = "";
                    arg4 = "";
                    arg5 = "";
                    arg6 = "";
                    arg7 = "";
                    arg2 = extraArgs[1].ToString();

                    if (extraArgs.Length >= 3) { arg3 = extraArgs[2].ToString(); }

                    if (extraArgs.Length >= 4) { arg4 = extraArgs[3].ToString(); }

                    if (extraArgs.Length >= 5) { arg5 = extraArgs[4].ToString(); }

                    if (extraArgs.Length >= 6) { arg6 = extraArgs[5].ToString(); }

                    if (extraArgs.Length == 7) { arg7 = extraArgs[6].ToString(); }

                    foreach (var x in cashMachine)
                    {
                        if (x.Key == arg2 | x.Key == arg3 | x.Key == arg4 | x.Key == arg5 | x.Key == arg6 | x.Key == arg7)
                        { Console.WriteLine(x.Key + " - " + x.Value); }
                    }
                }

                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }

        //method that returns current ATM balance
        static void machineBalance(Dictionary<string, int> atm)
        {
            Console.WriteLine("Machine Balance:");
            foreach (var x in atm) { Console.WriteLine(x.Key + " - " + x.Value); }
        }

    }
}
