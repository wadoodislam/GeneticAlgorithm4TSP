# GeneticAlgorithm4TSP
Implementation of Genetic Algorithm to approximately solve the Euclidian Traveling Salesman Problem.

## Installation and Setup Instructions

#### For Windows Users:

You will need `.NET v4` installed on your machine.  

To run the program follow these commands:
1. `...> git clone https://github.com/wadoodislam/GeneticAlgorithm4TSP.git`
2. `...> cd GeneticAlgorithm4TSP`
3. `...> C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild GeneticAlgorithm4TSP.csproj`
4. `...> copy *.txt bin\Debug`
5. `...> bin\Debug\GeneticAlgorithm4TSP`

![Windows output](/screenshots/Windows.png)

#### For Linux Users:
You will need `dotnet-sdk-2.2` installed on your machine. you can find instructions for this [here](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro)

To run the program follow these commands:
1. `~$ git clone https://github.com/wadoodislam/GeneticAlgorithm4TSP.git`
2. `~$ dotnet new console -o <name for new project>`
3. `~$ cd GeneticAlgorithm4TSP`
4. `~$ cp -r !(*.csproj|*.config) ../<name for new project>/`
5. `~$ cd ../<name for new project>/`
6. `~$ dotnet run`

![Linux output](/screenshots/Linux.png)


## Reflection
This was a week-long project built during **Computational Intelligence** course as a midterm project at the [National University of Computer and Emerging Sciences](http://nu.edu.pk/). Project goals included acquiring practical hands-on experience on **Genetic Algorithm** and compare results changing its various parameters.

This project helped me understand and acquire in-depth knowledge of genetic algorithm dynamics and taught me how can we translate a problem to chromosomes to apply evolutionary algorithms and how different parameters of the algorithm like mutation rate, crossover, and selection methods play vital roles in the accuracy and completion time of the algorithm.

The language used to implement this project in C# (c-sharp). I choose C# because it is a sophisticated object-oriented language that allowed me to implement the genetic algorithm without giving to much high-level abstraction while keeping the code clean and modular.

## License

* [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html)

## Contributing

Please fork this repository and contribute back using
[pull requests](https://github.com/wadoodislam/GeneticAlgorithm4TSP/pulls).

Any contributions, large or small, major features, bug fixes, additional
language translations, unit/integration tests are welcomed and appreciated
but will be thoroughly reviewed and discussed.

