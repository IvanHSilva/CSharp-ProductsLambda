using ProductsLambda.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductsLambda {
    internal class Program {
        static void Main(string[] args) {

            string path = "E:\\DEV-Files\\C#\\";
            string file = $"{path}ProductsLinq.csv";

            List<Product> list = new List<Product>();

            try {
                using (StreamReader sr = new StreamReader(file)) {
                    while (!sr.EndOfStream) {
                        string[] fields = sr.ReadLine().Split(';');
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        list.Add(new Product(name, price));
                    }
                }
                //double avg = list.Average(p => p.Price);
                double avg = (from p in list select p.Price).Average();
                Console.WriteLine($"Preço médio dos produtos: {avg.ToString("F2", CultureInfo.InvariantCulture)}");
                Console.WriteLine();
                //IEnumerable<string> names = list.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);
                IEnumerable<string> names = from p in list where p.Price < avg orderby p.Name descending select p.Name;
                Console.WriteLine($"Produtos com preços abaixo da média: ");
                foreach (string n in names) {
                    Console.WriteLine(n);
                }
                Console.WriteLine();
            } catch (IOException e) {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }
    }
}
