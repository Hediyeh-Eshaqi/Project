﻿using System;
using System.Collections.Generic;
using System.IO;

namespace project
{
    class drug{
        public string name;
        public int price;
        public Dictionary<string,string> drugsEffects = new Dictionary<string,string>();
        public Dictionary<string,char> alergies = new Dictionary<string,char>();
        public drug(string name, int price){
            this.name = name;
            this.price = price;
        }
        public void addEffect(string drugName , string Effect){
            drugsEffects.Add(drugName,Effect);
        }
        public void addAlergy(string allergy , char Effect){
            alergies.Add(allergy,Effect);
        }
    }

    class drugStore{
        public static long polynomialRollingHash(String str)
        {   
            // P and M
            int p = 31;
            int m = (int)(1e9 + 9);
            long power_of_p = 1;
            long hash_val = 0;
        
            // Loop to calculate the hash value
            // by iterating over the elements of String
            for(int i = 0; i < str.Length; i++)
            {
                hash_val = (hash_val + (str[i] -
                            'a' + 1) * power_of_p) % m;
                power_of_p = (power_of_p * p) % m;
            }
            return hash_val;
        }

        public static Dictionary<string,drug>[] hash_table = new Dictionary<string,drug>[2000];
        public drugStore(){
            for (int i = 0; i < 2000; i++)
            {
                hash_table[i]=new Dictionary<string,drug>();
            }
        }
        public void init(){
            for (int i = 0; i < 2000; i++)
            {
                hash_table[i] = new Dictionary<string,drug>();
            }
            string[] lines = System.IO.File.ReadAllLines("drugs.txt");
            foreach (string line in lines)
            {
                string[] splited = line.Split(':');
                long hash = polynomialRollingHash(splited[0])%2000;
                if (hash<0)
                {
                    hash*=-1;
                }
                hash_table[hash].Add(splited[0],new drug(splited[0],int.Parse(splited[1])));
            }
        }
        public void addNewDrug(string name ,  int price){
            
            //add the new drug to our objects

            
            long hash = polynomialRollingHash(name)%2000;
            if (hash<0)
            {
                hash*=-1;
            }

            if(hash_table[hash].ContainsKey(name)==true)
            {
                throw new Exception ("this drug is already exist!! you can not add it again!!");
            }
            else
            {
                hash_table[hash].Add(name,new drug(name,price));
                //add the new drug to our dataset

                File.AppendAllText("drugs.txt", name+" : "+price);
                //some random drugs for this drug effect
                    
                


                //some random alergy for this drug alergy
                
            }
            

            
            
        }

        public void readDrug(string name){
            long hash = polynomialRollingHash(name)%2000;
            if (hash<0)
            {
                hash*=-1;
            }
            if(hash_table[hash].ContainsKey(name)==true)
            {
                Console.WriteLine(hash_table[hash][name].name+" : "+hash_table[hash][name].price);
                Console.WriteLine("this drug effects are:");
                foreach (KeyValuePair<string, string> item in hash_table[hash][name].drugsEffects)
                {
                    Console.WriteLine(item.Key,item.Value);
                }
                Console.WriteLine("this drug alergies are:");
                foreach (KeyValuePair<string, char> item in hash_table[hash][name].alergies)
                {
                    Console.WriteLine(item.Key,item.Value);
                }
            }
            else
            {
                
                throw new Exception ("the drug is not in the drug store!!");
            }
            
        }
        
    }
    class Program
    {
        public static drugStore d = new drugStore();
        static void UserActions(int flag)
        {

            if (flag == 1)
            {
                drug test = new drug("test",1000);
                Console.WriteLine(test.name);
                test.addAlergy("alergy",'+');
                test.addEffect("drug","effect");
                Console.WriteLine(test.drugsEffects["drug"]);
                Console.WriteLine(test.alergies["alergy"]);
                
                d.init();
                
                long hash = drugStore.polynomialRollingHash("Drug_cwfordbcup"+" ")%2000;
                if (hash<0)
                {
                    hash*=-1;
                }
                Console.WriteLine(hash);
                Console.WriteLine(drugStore.hash_table[hash]["Drug_cwfordbcup"+" "].price);
                
            }
            else if (flag == 2)
            {
                d.addNewDrug("test",10000);
            }
            else if (flag == 3)
            {
                d.readDrug("test");
            }
            else if (flag == 4)
            {

            }
            else if (flag == 5)
            {

            }
            else if (flag == 6)
            {

            }
            else if (flag == 7)
            {

            }
            else
            {
                throw new Exception("Invalid input!! your input is not in the correct range!!");
            }
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                                                            Welcome Back!");
            Console.ForegroundColor = ConsoleColor.White;
            int flag = 0;
            while (flag != 8)
            {
                Console.WriteLine();
                Console.WriteLine("Enter your desired option:".PadLeft(27, ' '));
                Console.WriteLine("1.Reading data files".PadLeft(22, ' '));
                Console.WriteLine("2.Presence or absence of drug interactions in a prescription drug".PadLeft(67, ' '));
                Console.WriteLine("3.Evaluate the presence or absence of drug allergy in a prescription with referring disease".PadLeft(93, ' '));
                Console.WriteLine("4.Calculating the price of prescription drugs".PadLeft(47, ' '));
                Console.WriteLine("5.Rising prices of drugs".PadLeft(26, ' '));
                Console.WriteLine("6.Adding or removing from the data structures".PadLeft(47, ' '));
                Console.WriteLine("7.Search".PadLeft(10, ' '));
                Console.WriteLine("8.Exit".PadLeft(8, ' '));
                try
                {

                    flag = int.Parse(Console.ReadLine());
                    Console.Clear();
                }
                catch (System.Exception)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Invalid input!! your input is not a number!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                if (flag == 8) break;
                try
                {
                    UserActions(flag);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }



        }
    }
}


