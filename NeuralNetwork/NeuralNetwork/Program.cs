﻿using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.Classes;

namespace NeuralNetwork
{
	internal class Program
	{
		#region -- Constants --
		private const int MaxEpoch = 2000;
		#endregion

		#region -- Variables --
		private static int _numInputParameters;
		private static int _numHiddenLayerNeurons;
		private static int _numOutputParameters;
		private static Network _network;
		#endregion

		#region -- Main --
		private static void Main()
		{
			Greet();
			SetNumInputParameters();
			SetNumNeuronsInHiddenLayer();
			SetNumOutputParameters();

			CreateNetwork();
			TrainNetwork();
			VerifyTraining();

			Console.ReadLine();
		}
		#endregion

		#region -- Network Training --
		private static void TrainNetwork()
		{
			Console.WriteLine("Now, we need some input data.");
			PrintUnderline(30);
			PrintNewLine(2);

			var dataSet = new List<double[]>();
			var expectedResults =new List<int[]>();

			for (var i = 0; i < 4; i++)
			{
				dataSet.Add(GetInputData(String.Format("Data Set {0}", i + 1)));
				expectedResults.Add(GetExpectedResult(i));
				PrintNewLine(2);
			}

			_network.Train(dataSet, expectedResults);
			PrintNewLine(2);
			Console.WriteLine("The network is trained!");
			PrintNewLine();
		}

		private static void VerifyTraining()
		{
			Console.WriteLine("Let's test it!");
			var inputs = GetInputData(String.Format("Give me {0} inputs: ", _numInputParameters));

			var results = _network.GetResult(inputs);

			foreach (var result in results)
			{
				Console.WriteLine("Output: {0}", result);
			}
		}
		#endregion

		#region -- Network Setup --
		private static void Greet()
		{
			Console.WriteLine("We're going to create an artificial Neural Network!");
			Console.WriteLine("The network will use back propagation to train itself.");
			PrintUnderline(75);
			PrintNewLine(2);
		}

		private static void SetNumInputParameters()
		{
			Console.WriteLine("How many input parameters will there be?");
			_numInputParameters = GetInput("Input Parameters: ", 2);
			PrintNewLine(2);
		}

		private static void SetNumNeuronsInHiddenLayer()
		{
			Console.WriteLine("How many neurons in the hidden layer?");
			_numHiddenLayerNeurons = GetInput("Neurons: ", 2);
			PrintNewLine(2);
		}

		private static void SetNumOutputParameters()
		{
			Console.WriteLine("How many output parameters will there be?");
			_numOutputParameters = GetInput("Output Parameters: ", 1);
			PrintNewLine(2);
		}

		private static double[] GetInputData(string message)
		{
			Console.WriteLine(message);
			var line = Console.ReadLine();

			while (line == null || line.Split(' ').Count() < _numInputParameters)
			{
				Console.WriteLine("{0} inputs are required.", _numInputParameters);
				PrintNewLine();
				Console.WriteLine(message);
				line = Console.ReadLine();
			}

			var values = new double[_numInputParameters];
			var lineNums = line.Split(' ');
			for(var i = 0; i < lineNums.Length; i++)
			{
				double num;
				if (Double.TryParse(lineNums[i], out num))
				{
					values[i] = num;
				}
				else
				{
					Console.WriteLine("You entered an invalid number.  Try again");
					PrintNewLine(2);
					return GetInputData(message);
				}
			}

			return values;
		}

		private static int[] GetExpectedResult(int inputDataNumber)
		{
			Console.WriteLine("Expected Result for Data Set {0}:", inputDataNumber + 1);
			var line = Console.ReadLine();

			while (line == null || line.Split(' ').Count() < _numOutputParameters)
			{
				Console.WriteLine("{0} outputs are required.", _numOutputParameters);
				PrintNewLine();
				Console.WriteLine("Expected Result for Data Set {0}:", inputDataNumber + 1);
				line = Console.ReadLine();
			}

			var values = new int[_numOutputParameters];
			var lineNums = line.Split(' ');
			for (var i = 0; i < lineNums.Length; i++)
			{
				int num;
				if (int.TryParse(lineNums[i], out num) && (num == 0 || num == 1))
				{
					values[i] = num;
				}
				else
				{
					Console.WriteLine("You must enter 1s and 0s!");
					PrintNewLine(2);
					return GetExpectedResult(inputDataNumber);
				}
			}

			return values;
		}

		private static void CreateNetwork()
		{
			Console.WriteLine("Creating Network...");
			_network = new Network(_numInputParameters, _numHiddenLayerNeurons, _numOutputParameters, MaxEpoch);
		}
		#endregion

		#region -- Console Helpers --
		private static int GetInput(string message, int min)
		{
			Console.Write(message);
			var num = GetNumberFromConsole();

			while (num < min)
			{
				Console.Write(message);
				num = GetNumberFromConsole();
			}

			return num;
		}

		private static int GetNumberFromConsole()
		{
			int num;
			return int.TryParse(Console.ReadLine(), out num) ? num : 0;
		}

		private static void PrintNewLine(int numNewLines = 1)
		{
			for(var i = 0; i < numNewLines; i++)
				Console.WriteLine();
		}

		private static void PrintUnderline(int numUnderlines)
		{
			for(var i = 0; i < numUnderlines; i++)
				Console.Write('-');
		}
		#endregion
	}
}