//Dealership Inventory Management System - Part 1.  Create a program that allows a user to add, remove, and view vehicles in an inventory management system of a car dealership. The program should allow the user to add vehicles to the inventory, remove vehicles from the inventory, and view the inventory.
//Create lists to store vehicle attributes 
using System.ComponentModel.Design;
using System.Data.Common;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;

namespace CarDealershipInventory;

// A structured type to set the vehicle details
class Vehicle
{
    public int Year { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }

    public Vehicle(int year, string make, string model)
    {
        Year = year;
        Make = make;
        Model = model;
    }
}

//1.3 Create a list to store the vehicle inventory at the dealership
class Program
{
    static List<Vehicle> inventory = new List<Vehicle>();

    //Constant declarations matching the menu configuration
    const int OPTION_VIEW = 1;
    const int OPTION_ADD = 2;
    const int OPTION_REMOVE = 3;
    const int OPTION_QUIT = 4;
    static void Main(string[] args)
    {
        //Starts the loop
        PrintMenu();
        int userSelection = GetUserInput();

        while (userSelection != OPTION_QUIT)
        {
            HandleUserInput(userSelection);

            PrintMenu();
            userSelection = GetUserInput();
        }

        Console.WriteLine("Thank you for visiting Chuck's Cars and Trucks!");
    }

    //2.1 Prints out the menu options as a numbered list with quit option at the end
    static void PrintMenu()
    {
        Console.WriteLine("\n--- Welcome to Chuck's Cars and Trucks ---");
        Console.WriteLine("\nPlease select from the following options (1-4): ");
        Console.WriteLine("\n===============================================");
        Console.WriteLine($"{OPTION_VIEW}.  View Inventory");
        Console.WriteLine($"{OPTION_ADD}.  Add Vehicle");
        Console.WriteLine($"{OPTION_REMOVE}  Remove Vehicle");
        Console.WriteLine($"{OPTION_QUIT}.  Quit");
        Console.WriteLine("\n===============================================");
    }

    //2.2 Prompts the user for a number selecting their choice and returns it as an int 
    static int GetUserInput()
    {
        int selection;
        while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > 4)
        {
            Console.WriteLine("Invalid.  Please enter a number between 1 and 4.");
        }
        return selection;
    }

    //2.3 Accepts the user's menu option selection and calls the correct function.  Break after each case statement to keep from falling through to other case statements.
    static void HandleUserInput (int selection)
    {
        switch (selection)
        {
            case OPTION_VIEW:
                ViewInventory();
                break;
            case OPTION_ADD:
                AddVehicle();
                break;
            case OPTION_REMOVE:
                RemoveVehicle();
                break;
            default:
                break;
        }
    }

    //2.4 Requests user input for Year, Make and Model and then adds the vehicle to the inventory
    static void AddVehicle()
    {
        Console.WriteLine("\n---Add New Vehicle---");
        //Prompts user to enter vehicle model year and verifies the user's input to be valid
        Console.WriteLine("Please enter the vehicle's model year: ");
        int year;
        //Begins loop to only allow valid years which vehicles were produced
        while (!int.TryParse(Console.ReadLine(), out year) && year > 1885 && year <= DateTime.Now.Year)
        {
            Console.WriteLine("Invalid year. Please try again. Enter a valid vehicle model year: ");
        }
    //Prompt user to enter both the make and model of the vehicle, waits for their input and reads their input
    Console.Write("Please enter the vehicle's make: ");
    string make = Console.ReadLine();

    Console.Write("Please enter the vehicle's model: ");
    string model = Console.ReadLine();

    //Adds the vehicle to the current inventory and displays it on screen after it has been added
    Vehicle newVehicle = new Vehicle(year, make, model);
    inventory.Add(newVehicle);

    Console.WriteLine($"Thank you! Your {newVehicle} has been added to the inventory.");
    }

    //2.5 Requests input from the user for the inventory number of which car to remove and removes the car from inventory
    static void RemoveVehicle()
    {
        Console.WriteLine("\n--- Remove Vehicle ---");
        if (inventory.Count == 0)
        {
            Console.WriteLine("Current inventory is empty. No vehicle(s) to remove.");
            return;
        }

        ViewInventory();
        Console.WriteLine("\nPlease enter the inventory number of the vehicle to remove: ");
        
        int targetIndex;
        if (int.TryParse(Console.ReadLine(), out targetIndex))
        {
            //Translates from a 1-based user input to a 0-based index 
            int systemIndex = targetIndex - 1;

            if (systemIndex >= 0 && systemIndex < inventory.Count)
            {
                Vehicle removed = inventory[systemIndex];
                inventory.RemoveAt(systemIndex);
                Console.WriteLine($"\nRemoved {removed.Year} {removed.Make} {removed.Model} from inventory.");
            }
            else
            {
                //Display to the user if they don't enter an inventory number that does not match current inventory
                Console.WriteLine("Error: no vehicle matches that inventory number.");
            }
        }
        else
        {
            //If user does not input an integer, prompt them to try again
            Console.WriteLine("Please enter a valid integer value for a vehicle to remove.");
        }
    }


    //2.6 Prints a human readable list of all vehicles in current inventory
    static void ViewInventory()
    {
        Console.WriteLine("\n--- Current Inventory ---");
        if (inventory.Count == 0)
        {
            Console.WriteLine("Current inventory is empty.  Please add vehicle(s).");
            return;
        }
        
        for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"{i+1}. {inventory[i].Year} | Make: {inventory[i].Make} | Model: {inventory[i].Model}");
            }
    }
}