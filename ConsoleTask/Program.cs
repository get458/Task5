// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ConsoleTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTask
{

    class Program
    {
        private static void Main(string[] args)
        {
            
            Console.WriteLine("Hello, World!");
            
            //Reg("Москва", "Пушкина", "5", "543", "Иванов",
              //  "Иван", "Иваныч", "mtgfd@gmail.com", new DateOnly(2005, 04, 20), "89527651231");
            
            //DelUser(6);
            //YoungerUser();
            //OlderUser();
            AverageAge();
            //SearchForSurname("Борисов");
        }

        public static bool checkCredentials(Usertable usertable, string mail)
        {
            using (LastTaskContext db = new LastTaskContext())
            {
                if (db.Usertables.Contains(usertable))
                {
                    Console.WriteLine("Пользователь с введенными данным существует!");
                    return false;
                }
                else
                {
                    if (!(usertable.Telephone.Length == 11) && !(usertable.Telephone.StartsWith('8')))
                    {
                        Console.WriteLine("Номер телефона должен содержать 11 цифр и начинаться с 8");
                        return false;
                        
                    }
                    else
                    {
                        var email = new EmailAddressAttribute();
                        if (email.IsValid(mail))
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат почты!");
                            return false;
                        }
                    }
                }
            }
        }

        public static void Reg(string city, string street, string building, string description,
            string name, string surname, string lastname, string mail, DateOnly birthday,
            string telephone)
        {
            using (LastTaskContext db = new LastTaskContext())
            {
                Address address = new Address()
                {
                    City = city,
                    Street = street,
                    Building = building,
                    Description = description,
                   
                };

                Usertable usertable = new Usertable()
                {
                    Name = name,
                    Surname = surname,
                    Lastname = lastname,
                    Mail = mail,
                    Birthday = birthday,
                    Telephone = telephone,
                    AddressId = 3
                };
                if (checkCredentials(usertable, mail))
                {
                    db.Usertables.Add(usertable);
                    db.Addresses.Add(address);
                    db.SaveChanges();
                    Console.WriteLine("Add!");
                }
            }
        }
        
        public static bool DelUser(int id)
        {
            using (LastTaskContext db = new LastTaskContext())
            {
                var userToDel = db.Usertables.Find(id);
                db.Usertables.Remove(userToDel);
                db.SaveChanges();
                Console.WriteLine("Delete!");
            
                return true;
            }
        }
        
        public static void YoungerUser()
        {
            using (LastTaskContext db = new LastTaskContext())
            {
                var younger = db.Usertables.OrderBy(e => e.Birthday).Reverse().ToList();
                var today = DateTime.Today;
                Console.WriteLine("Sort by younger to older");
                foreach (var user in younger)
                {
                    var age = today.Year - user.Birthday.Year;
                    Console.WriteLine($"{user.Surname} - {age}");
                }
            }
        } 
        
        public static void OlderUser()
        {
            using (LastTaskContext db = new LastTaskContext())
            {
                var younger = db.Usertables.OrderBy(e => e.Birthday).ToList();
                var today = DateTime.Today;
                Console.WriteLine("Sort by older to younger");
                foreach (var user in younger)
                {
                    var age = today.Year - user.Birthday.Year;
                    Console.WriteLine($"{user.Surname} - {age}");
                }
            }
        }

        public static void AverageAge()
        {
            using (LastTaskContext db = new LastTaskContext())
            {
                var users = db.Usertables.ToList();
                var today = DateTime.Today;
                int countAge = 0;
                int userCount = 0;
                var result = 0;
                
                foreach (var user in users)
                {
                    var age = today.Year - user.Birthday.Year;
                    countAge += age;
                    userCount++;
                }
                result = countAge / userCount;
                Console.WriteLine($"Average age is {result}");
            }
        }

        public static void SearchForSurname(string surname)
        {
            using (LastTaskContext db = new LastTaskContext())
            {
                var search = db.Usertables.Where(e => e.Surname == surname).Include(q => q.Address).ToList();
                foreach (var user in search)
                {
                    Console.WriteLine($"{user.Id} - {user.Surname} - {user.Name} - {user.Lastname} - {user.Birthday} - {user.Telephone} - {user.Mail} " +
                                      $"- {user.Address.City} - {user.Address.Street} - {user.Address.Building} - {user.Address.Description}");   
                }
            }
        }

    }

}