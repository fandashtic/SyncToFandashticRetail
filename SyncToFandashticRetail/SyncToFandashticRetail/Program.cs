using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SyncToFandashticRetail
{
    class Program
    {
        static string newBranch = @"{ 'Test': 'test' }";
        static void Main(string[] args)
        {
            // Instanciating with base URL  
            FirebaseDB firebaseDB = new FirebaseDB();

            // Referring to Node with name "Teams"  
            FirebaseDB firebaseDBTeams = firebaseDB.Node("Users");
            FirebaseResponse getResponse = firebaseDBTeams.Get();

            List<User> usersList = new List<User>();

            if (getResponse.JSONContent == "" || getResponse.JSONContent == "null")
            {
                FirebaseResponse putNewBranchResponse = firebaseDBTeams.Put(newBranch);
            }
            else
            {
                usersList = JsonConvert.DeserializeObject<List<User>>(getResponse.JSONContent.ToString());
            }

            Dictionary<int, User> users = new Dictionary<int, User>();

            if (usersList != null && usersList.Count > 0)
            {
                usersList.ForEach(user =>
                {
                    if (user != null)
                    {
                        user.UserName = user.UserName + "-Test";
                        users.Add(user.UserId, user);
                    }
                });
            }
            else
            {
                users.Add(1, new User { UserId = 1, UserName= "Manickam1" });
                users.Add(2, new User { UserId = 2, UserName = "Ram" });
            }

            var json = JsonConvert.SerializeObject(users);

            FirebaseResponse putResponse = firebaseDBTeams.Put(json);

            //WriteLine("PUT Request");
            //FirebaseResponse putResponse = firebaseDBTeams.Put(data);
            //WriteLine("putResponse.Success");
            //WriteLine();

            //WriteLine("POST Request");
            //FirebaseResponse postResponse = firebaseDBTeams.Post(data);
            //WriteLine("postResponse.Success");
            //WriteLine();

            //WriteLine("PATCH Request");
            //FirebaseResponse patchResponse = firebaseDBTeams
            //    // Use of NodePath to refer path lnager than a single Node  
            //    .NodePath("Team-Awesome/Members/M1")
            //    .Patch("{\"Designation\":\"CRM Consultant\"}");
            //WriteLine("patchResponse.Success");
            //WriteLine();

            //WriteLine("DELETE Request");
            //FirebaseResponse deleteResponse = firebaseDBTeams.Delete();
            //WriteLine("deleteResponse.Success");
            //WriteLine();

            //WriteLine(firebaseDBTeams.ToString());
            ReadLine();
        }

        static void WriteLine(string str = null)
        {
            Console.WriteLine(str);
        }

        static void ReadLine()
        {
            Console.ReadKey();
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
    

