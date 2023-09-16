
/*
 Name: Jerrell Russ 
 Email: jruss1.cnm.edu
 Title: Ideal Gas Calculator
 Obj: I this program we'll cover chapters 1.7-2
 */
using System.IO;
using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using System.Transactions;
using System.Xml.Linq;

namespace NewIdealGasGalc
{

    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayHeader();

            string doAnother = "y";


            do
            {

                //variable needed for to call get moleclar weight
                int count = 0;
                string[] gasNames = new string[86];
                double[] molecularWeights = new double[86];
                GetMolecularWeights(ref gasNames, ref molecularWeights, out count);


                DisplayGasNames(gasNames, count);

                int countGases = 0;
                countGases = count;


                //Variables for GetMolecularWeightsFromName
                string gasName;


                //get the user gas they want
                Console.Write($"\n\n What is the name of the gas you want/need (type as shown): ");
                gasName = Console.ReadLine();
                Console.WriteLine($" The element you chose is {gasName}");

                double getMoleWeightFromName = GetMolecularWeightFromName(gasName, gasNames, molecularWeights, countGases);
               


                Console.Write(" Enter the temperature in celcius? ");
                double celcius = double.Parse(Console.ReadLine());
                double temp = CelciusToKelvin(celcius);





                //Variables needed to call NumberOfMoles
                string stringMass;
                double mass;
                double molecularWeight;
                string stringMoleWeight;

                Console.Write(" What is the mass in grams? ");
                stringMass = Console.ReadLine();
                mass = double.Parse(stringMass);


                molecularWeight = GetMolecularWeightFromName( gasName, gasNames,  molecularWeights,  countGases);

                NumberOfMoles(mass, molecularWeight);



                //Variables Needed to call Pressure

                string tempVol;
                double vol;
                Console.Write(" What is the volume in meters cubed? ");
                tempVol = Console.ReadLine();
                vol = double.Parse(tempVol);

                double pressure = Pressure(mass, vol, temp, molecularWeight);


                DisplayPressure(pressure);

                Console.WriteLine(" Do you want another go around? Type 'y' or 'n' ");
                doAnother = Console.ReadLine();
            } while (doAnother == "y");

        }

        static void DisplayHeader()
        {
            Console.Write("Jerrell Russ, Title: Ideal Gas Caculator, OBJ: Be able to open a csv file and extract data from it \n\n\n");
        }



        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {

            count = 0;

            try
            {
                StreamReader sr = new StreamReader("MolecularWeightsGasesAndVapors.csv");
                
                string line;
                sr.ReadLine();//Reading in the header not used

                while ((line = sr.ReadLine()) != null)
                {
                    string[] fileSplit = line.Split(',');
                    gasNames[count] = fileSplit[0];
                    molecularWeights[count] = double.Parse(fileSplit[1]);

                    count++;

                }
                
            }
            catch (Exception exp)
            {
                Console.WriteLine("file could not be read");
                Console.WriteLine(exp.Message);

            }

        }




        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            for (int i = 0; i < countGases; i++)
            {
                Console.Write($"{gasNames[i],20} ");
                if (i % 3 == 0)
                {
                    Console.WriteLine();

                }
            }
        }





        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {
            
            double tempMoleWeights=0.0;
            bool tempGasName;    

            for (int i = 0; i < countGases; i++)
            {
                if (gasNames[i] == gasName)
                {
                    tempMoleWeights = molecularWeights[i];

                    i = countGases;
                }


            }
            
            return tempMoleWeights;
           
        }





        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
            double pressure;


            //r is a constant variable 
            const double r = 8.3145;
            double kelvin = temp + 273.15;


            //double n
            double n;
            n = mass / molecularWeight;


            pressure = n * r * kelvin / vol;

            return pressure;


        }




        static double NumberOfMoles(double mass, double molecularWeight)
        {
            mass = mass / molecularWeight;
            return mass;
        }




        static double CelciusToKelvin(double celcius)
        {
            double kelvin = celcius + 273.15;
            return kelvin;

        }




        private static void DisplayPressure(double pressure)
        {
            Console.WriteLine($"This pressures is {pressure}");


        }




    }
}