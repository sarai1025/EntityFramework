using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a basic example of Entity Framework implementation for .Net Framework, based on database first approach. ");
            Console.WriteLine("This approach maps the entities trough edmx.");

            bool close = false;
            while (!close)
            {
                ExecuteOptions();

                Console.WriteLine("Do you want to close? Yes or No?");
                close = Console.ReadLine() == "Yes";
            }
        }

        private static void ExecuteOptions()
        {
            Console.WriteLine("\nPlease write the number of your desired option: \n 1. Read existing data. \n 2. Update existing pet´s age. \n 3. Delete existing user. \n 4. Create a new Record.");
            string optionSelected = Console.ReadLine();

            switch (optionSelected)
            {
                case "1":
                    ReadData();
                    break;
                case "2":
                    UpdatePetAge();
                    break;
                case "3":
                    DeleteUser();
                    break;
                case "4":
                    CreateAdopting();
                    break;
                default:
                    break;
            }
        }

        private static void ReadData()
        {
            var context = new EFDBEntities();
            var result = (from o in context.Owners
                          join p in context.Pets on o.kPet equals p.kPet
                          select new { o, p }).ToList();
            foreach (var item in result)
            {
                Console.WriteLine($"Owner: {item.o.Name}. Pet: {item.p.Name} - {item.p.Age} years old.");
            }

            if (result == null || !result.Any())
            {
                Console.WriteLine("There is no data.");
            }
        }

        private static void UpdatePetAge()
        {
            Console.WriteLine("Write the pet´s name you want to update:");
            var petNameToUpdate = Console.ReadLine();
            Console.WriteLine("Write the new pet´s age:");
            var newPetAge = Console.ReadLine();

            var context = new EFDBEntities();
            var pet = context.Pets.FirstOrDefault(o => o.Name == petNameToUpdate);

            if (pet == null)
            {
                Console.WriteLine("Record Not found.");
                return;
            }
            context.Pets.Attach(pet);
            pet.Age = int.Parse(newPetAge);
            context.SaveChanges();

            Console.WriteLine("Record successfully updated.");
        }

        private static void DeleteUser()
        {
            Console.WriteLine("Write the user´s name you want to delete.");
            var userNameToDelete = Console.ReadLine();
            var context = new EFDBEntities();
            var user = context.Owners.FirstOrDefault(o => o.Name == userNameToDelete);
            
            if (user == null)
            {
                Console.WriteLine("Record Not found.");
                return;
            }
            context.Owners.Remove(user);
            context.SaveChanges();

            Console.WriteLine("Record successfully deleted.");
        }

        private static void CreateAdopting()
        {
            Console.WriteLine("Please enter your name:");

            string userName = Console.ReadLine();

            Console.WriteLine("Now, write your pet´s name:");
            string petName = Console.ReadLine();

            int petAge;
            Console.WriteLine("Finally write your pet´s age:");
            if (!int.TryParse(Console.ReadLine(), out petAge))
            {
                Console.WriteLine("Please enter a valid number for your pet´s age:");
                int.TryParse(Console.ReadLine(), out petAge);
            }

            Pets pet = CreatePet(petName, petAge);
            CreateOwner(userName, pet);

            Console.WriteLine("Record successfully created");

        }
        private static Pets CreatePet(string petName, int petAge)
        {
            var context = new EFDBEntities();
            var pet = new Pets { Name = petName, Age = petAge };
            context.Pets.Add(pet);
            context.SaveChanges();
            return pet;
        }
        private static void CreateOwner(string userName, Pets pet)
        {
            var context = new EFDBEntities();
            Owners owner = new Owners()
            {
                Name = userName,
                kPet = pet.kPet
            };
            context.Owners.Add(owner);
            context.SaveChanges();
        }
    }
}
