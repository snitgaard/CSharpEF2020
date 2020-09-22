using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using PetShop.Core.ApplicationServices;
using PetShop.Core.Entity;

namespace PetShop.UI
{
    public class Printer : IPrinter
    {
        private IPetService _petService;

        public Printer(IPetService petService)
        {
            _petService = petService;
            InitData();

            void InitData()
            {
                var pet1 = new Pet()
                {
                    Name = "Johnny",
                    Type = "Leguan",
                    BirthDate = new DateTime(2018, 6, 10),
                    SoldDate = new DateTime(2019, 02, 06),
                    Color = "Green",
                    PreviousOwner = "Billy Joel",
                    Price = 1500
                };
                _petService.CreatePet(pet1);

                var pet2 = new Pet()
                {
                    Name = "Bravo",
                    Type = "Kongecobra",
                    BirthDate = new DateTime(2010, 05, 20),
                    SoldDate = new DateTime(2012, 08, 12),
                    Color = "Orange",
                    PreviousOwner = "Prins Henrik",
                    Price = 5000
                };
                _petService.CreatePet(pet2);

                var pet3 = new Pet()
                {
                    Name = "Abraham",
                    Type = "Tick",
                    BirthDate = new DateTime(2019, 08, 29),
                    SoldDate = new DateTime(2019, 08, 29),
                    Color = "Blue",
                    PreviousOwner = "Georg Jensen",
                    Price = 5
                };
                _petService.CreatePet(pet3);

                var pet4 = new Pet()
                {
                    Name = "Kasper",
                    Type = "Evolution dyret",
                    BirthDate = new DateTime(1809, 02, 12),
                    SoldDate = new DateTime(2002, 10, 16),
                    Color = "Blue",
                    PreviousOwner = "Leonardo DaVinci",
                    Price = 50000
                };
                _petService.CreatePet(pet4);
            }
        }

        public void StartUI()
        {
            string[] menuItems =
            {
                "List All Pets",
                "Add Pet",
                "Delete Pet",
                "Edit Pet",
                "Search pets by type",
                "Search pets by color",
                "Sort by price",
                "Get 5 cheapest pets",
                "Exit"
            };

            int selection = ShowMenu(menuItems);

            //name, type, birthdate, solddate, color, previousowner, price
            while (selection != 9)
            {
                switch (selection)
                {
                    case 1:
                        var pets = _petService.GetPets();
                        ListPets(pets);
                        break;
                    case 2:
                        var name = AskQuestion("Name: ");
                        var type = AskQuestion("Type: ");
                        var birthDate = AskQuestion("Birthdate: ");
                        var soldDate = AskQuestion("Sold date: ");
                        var color = AskQuestion("Color: ");
                        var previousOwner = AskQuestion("Previous owner: ");
                        var price = AskQuestion("Price: ");
                        var pet = _petService.NewPet(name, type, DateTime.Parse(birthDate), 
                            DateTime.Parse(soldDate), color, previousOwner, Double.Parse(price));
                        _petService.CreatePet(pet);
                        break;
                    case 3:
                        var idToDelete = PrintFindPetById();
                        _petService.DeletePet(idToDelete);
                        break;
                    case 4:
                        var idToEdit = PrintFindPetById();
                        var petToEdit = _petService.FindPetById(idToEdit);
                        Console.WriteLine("Updating " + petToEdit.Name);
                        var newName = AskQuestion("Name: ");
                        var newType = AskQuestion("Type: ");
                        var newBirthDate = AskQuestion("Birthdate: ");
                        var newSoldDate = AskQuestion("Sold date: ");
                        var newColor = AskQuestion("Color: ");
                        var newPreviousOwner = AskQuestion("Previous owner: ");
                        var newPrice = AskQuestion("Price: ");
                        _petService.UpdatePet(new Pet()
                        {
                            Id = idToEdit,
                            Name = newName,
                            Type = newType,
                            BirthDate = DateTime.Parse(newBirthDate),
                            SoldDate = DateTime.Parse(newSoldDate),
                            Color = newColor,
                            PreviousOwner = newPreviousOwner,
                            Price = Double.Parse(newPrice)
                        });
                        break;
                    case 5:
                        var typeToSearch = AskQuestion("Insert pet type: ");
                        var petTypeSearch = _petService.GetAllByType(typeToSearch);
                        ListPets(petTypeSearch);
                        break;
                    case 6:
                        var colorToSearch = AskQuestion("Insert pet color: ");
                        var petColorSearch = _petService.GetAllByColor(colorToSearch);
                        ListPets(petColorSearch);
                        break;
                    case 7:
                        var sortedPrice = _petService.GetAllByPrice();
                        ListPets(sortedPrice);
                        break;
                    case 8:
                        var fiveCheapest = _petService.GetFiveCheapestPets();
                        ListPets(fiveCheapest);
                        break;
                    default:
                        break;
                }

                selection = ShowMenu(menuItems);
            }

            Console.WriteLine("Exiting application. Bye bye!");
            Console.ReadLine();
        }

        private int PrintFindPetById()
        {
            Console.WriteLine("Insert Pet Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number");
            }

            return id;
        }

        private string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        private int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("Select the menu you want to open: ");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ": " + menuItems[i]);
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > 9)
            {
                Console.WriteLine("You need to select a number between 1 and 9");
            }
            return selection;
        }

        void ListPets(List<Pet> pets)
            {
                Console.WriteLine("\nList of Pets");
                foreach (var pet in pets)
                {
                    Console.WriteLine($"\nId: {pet.Id} " + $"\nName: {pet.Name} " +
                                      $"\nType: {pet.Type} " +
                                      $"\nBirthdate: {pet.BirthDate} " +
                                      $"\nSold date: {pet.SoldDate}" +
                                      $"\nColor: {pet.Color}" +
                                      $"\nPrevious Owner: {pet.PreviousOwner}" +
                                      $"\nPrice: {pet.Price}");
                }
                Console.WriteLine("\n");
            }

        }
    }
