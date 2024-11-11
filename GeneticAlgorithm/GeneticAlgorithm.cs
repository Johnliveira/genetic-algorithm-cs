namespace GeneticAlgorithm;

using System;
using System.Collections.Generic;

class GeneticAlgorithm
{

    public static int familySize = 50;
    
    static void Main()
    {
        List<string> initialPopulation = GenerateInitialPopulation(familySize);

        List<string> finalPopulation = initialPopulation;
        for (int i = 0; i < 100; i++)
        {
            finalPopulation = GenerateNewGeneration(initialPopulation, familySize);
        }
        
        Console.WriteLine("Geração Final:");
        foreach (string individual in finalPopulation)
        {
            Console.WriteLine(individual);
        }
    }

    static List<string> GenerateInitialPopulation(int size)
    {
        Random random = new Random();
        List<string> population = new List<string>();

        for (int i = 0; i < size; i++)
        {
            string individual = Convert.ToString(random.Next(0, 256), 2).PadLeft(8, '0');
            population.Add(individual);
        }

        return population;
    }

    static List<string> GenerateNewGeneration(List<string> currentPopulation, int newGenerationSize)
    {
        List<string> newGeneration = new List<string>();
        Random random = new Random();

        for (int i = 0; i < newGenerationSize; i++)
        {
            string newIndividual = CompetitionAndReproduction(currentPopulation);

            if (random.Next(100) < 1) // 1% de chance
            {
                newIndividual = Mutation(newIndividual);
            }

            newGeneration.Add(newIndividual);
        }

        return newGeneration;
    }

    static string CompetitionAndReproduction(List<string> population)
    {
        Random random = new Random();

        string individual1 = population[random.Next(population.Count)];
        string individual2 = population[random.Next(population.Count)];
        string winner1 = Fitter(individual1, individual2);

        string individual3 = population[random.Next(population.Count)];
        string individual4 = population[random.Next(population.Count)];
        string winner2 = Fitter(individual3, individual4);

        return Crossing(winner1, winner2);
    }

    static string Crossing(string father, string mother)
    {
        Random random = new Random();
        int cutoffPoint = random.Next(1, 7);
        return father.Substring(0, cutoffPoint) + mother.Substring(cutoffPoint);
    }

    static string Mutation(string individual)
    {
        Random random = new Random();

        int geneIndex = random.Next(8);
        
        char[] genes = individual.ToCharArray();
        genes[geneIndex] = genes[geneIndex] == '0' ? '1' : '0';

        return new string(genes);
    }

    static string Fitter(string individualA, string individualB)
    {
        int fitnessA = BinaryToInt(individualA);
        int fitnessB = BinaryToInt(individualB);
        return fitnessA >= fitnessB ? individualA : individualB;
    }

    static int BinaryToInt(string binary)
    {
        return Convert.ToInt32(binary, 2);
    }
}

